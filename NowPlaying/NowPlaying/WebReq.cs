using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace NowPlaying {
    public class HttpData {
        public WebHeaderCollection Headers;
        public string Html;
    }
    class WebReq {
        public Uri URI;
        private Socket socket;
        private Random rnd = new Random();
        public ReqState State = ReqState.Ready;
        public WebHeaderCollection Headers = new WebHeaderCollection();
        private WebHeaderCollection outHeaders = new WebHeaderCollection();
        private string Filename = "";
        private byte[] Postdata = new byte[0];
        private int ConMode = 1;
        private string ConQHdr = "GET";
        private string ConPHdr = "application/x-www-form-urlencoded";
        private string MPBound = "";
        private bool ReturnStr = false;
        public bool isReady = true;
        public string sResponseCode = "";
        public string sResponse = "";
        public MemoryStream msResponse;
        public int cSize = 1024;
        private int iTimeout = -1;
        private long cLength;
        public double Progress;
        
        public enum ReqState { Ready, Connecting, Requesting, Downloading, Completed, Failed };
        public void Request(Uri Url, WebHeaderCollection cHeaders, byte[] baPostdata, string sMPBound, int iPostType,
            string sFilename, bool bReturnStr) {
            sResponseCode = ""; msResponse = new MemoryStream();
            URI = Url; Filename = sFilename; ReturnStr = bReturnStr;
            isReady = false; State = ReqState.Connecting;
            Headers = new WebHeaderCollection();
            outHeaders.Add("Host: " + URI.Host);
            outHeaders.Add("Connection: " + "Close");
            outHeaders.Add("User-Agent: ShiNPScli/1.0.0 (Windows NT 5.1; U; en)");

            for (int a = 0; a < cHeaders.Count; a++) {
                outHeaders.Add(cHeaders.GetKey(a) + ": " + cHeaders.Get(a));
            }
            ConMode = iPostType;
            if (ConMode != 1) {
                ConQHdr = "POST"; Postdata = baPostdata;
                outHeaders.Add("Content-Length: " + Postdata.Length);
                if (ConMode == 3) {
                    MPBound = sMPBound;
                    ConPHdr = "multipart/form-data; boundary=" + MPBound;
                }
                outHeaders.Add("Content-Type: " + ConPHdr);
            }

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerAsync();
        }
        private void bw_DoWork(object sender, DoWorkEventArgs e) {
            try {
                // ~~~ Create socket, set timeout ~~~ \\
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint RHost = new IPEndPoint(Dns.GetHostEntry(URI.Host).AddressList[0], URI.Port);
                if (iTimeout != -1) {
                    socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, iTimeout);
                    socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, iTimeout);
                    BackgroundWorker bwTimeout = new BackgroundWorker();
                    bwTimeout.DoWork += new DoWorkEventHandler(bwTimeout_DoWork);
                    bwTimeout.RunWorkerAsync();
                }

                // ~~~ Create request, send request ~~~ \\
                State = ReqState.Connecting;
                try {
                    socket.Connect(RHost);
                }
                catch {
                    throw new Exception("#02-0002");
                }
                State = ReqState.Requesting;
                string ReqStr = ConQHdr + " " + URI.PathAndQuery + " HTTP/1.1\r\n" + outHeaders;
                //Response = ReqStr; isReady = true; return;
                byte[] bPck = System.Text.Encoding.ASCII.GetBytes(ReqStr);
                socket.Send(bPck);
                if (ConMode != 1) {
                    if (ConMode == 9001) {
                        string sGoodPack = "HTTP/1.1 100 Continue" + "\r\n\r\n";
                        string sTmpResp = ""; byte[] bTmpResp = new byte[1]; int iTmpBytes;
                        while ((iTmpBytes = socket.Receive(bTmpResp, 0, 1, SocketFlags.None)) > 0) {
                            sTmpResp += Encoding.ASCII.GetString(bTmpResp, 0, iTmpBytes);
                            if (sTmpResp.StartsWith(sGoodPack)) break;
                            if (sTmpResp.Length >= sGoodPack.Length && !sTmpResp.StartsWith(sGoodPack))
                                throw new Exception("#02-0006");
                        }
                    }
                    socket.Send(Postdata);
                }
                if (!ParseHeader()) {
                    if (sResponseCode != "") throw new Exception("#02-0004_" + sResponseCode);
                    throw new Exception("#02-0003");
                }

                // ~~~ Download file ~~~ \\
                byte[] RecvBuffer = new byte[cSize]; int nBytes, nTotalBytes = 0;
                while ((nBytes = socket.Receive(RecvBuffer, 0, cSize, SocketFlags.None)) > 0) {
                    nTotalBytes += nBytes; State = ReqState.Downloading;
                    msResponse.Write(RecvBuffer, 0, nBytes);
                }
                socket.Close();

                // ~~~ Return text ~~~ \\
                if (ReturnStr) {
                    //TODO: Parse encoding from headers
                    msResponse.Seek(0, SeekOrigin.Begin);
                    System.IO.StreamReader utf = new StreamReader(msResponse, Encoding.UTF8);
                    sResponse = utf.ReadToEnd(); //Close and Dispose raeps msResponse
                }
                if (Filename != "") {
                    System.IO.FileStream fs = new FileStream(Filename, FileMode.Create);
                    msResponse.WriteTo(fs); fs.Flush(); fs.Close(); fs.Dispose();
                }
                msResponse.Seek(0, SeekOrigin.Begin);
                State = ReqState.Completed;
                Progress = 100; isReady = true;
            }
            catch (Exception ex) {
                wrExThrow(ex.Message, ex.StackTrace);
            }
        }
        private bool ParseHeader() {
            try {
                byte[] bytes = new byte[10]; string Header = "";
                while (socket.Receive(bytes, 0, 1, SocketFlags.None) > 0) {
                    Header += Encoding.ASCII.GetString(bytes, 0, 1);
                    if (bytes[0] == '\n' && Header.EndsWith("\r\n\r\n"))
                        break;
                }
                MatchCollection matches = new Regex("[^\r\n]+").Matches(Header.TrimEnd('\r', '\n'));
                for (int n = 1; n < matches.Count; n++) {
                    string[] strItem = matches[n].Value.Split(new char[] { ':' }, 2);
                    if (strItem.Length > 0)
                        Headers[strItem[0].Trim()] = strItem[1].Trim();
                }
                if (matches.Count > 0) {
                    if (matches[0].Value.Contains(" 404 ")) sResponseCode = "404";
                }
                if (Headers["Content-Length"] != null) cLength = int.Parse(Headers["Content-Length"]);
                if (sResponseCode != "") return false;
                return true;
            }
            catch {
                //wrExThrow("Header parse error");
                return false;
            }
        }
        public void SetTimeout(int Timeout) {
            iTimeout = Timeout;
        }
        private void bwTimeout_DoWork(object sender, DoWorkEventArgs e) {
            System.Threading.Thread.Sleep(iTimeout);
            if (State != ReqState.Connecting) return;
            wrExThrow("#02-0005");
        }
        public string RandomChars(int Count) {
            string ret = "";
            for (int a = 0; a < Count; a++) {
                int ThisRnd = rnd.Next(1, 63);
                if (ThisRnd >= 1 && ThisRnd <= 26)
                    ThisRnd += 64;
                else if (ThisRnd >= 27 && ThisRnd <= 52)
                    ThisRnd += 97 - 27;
                else if (ThisRnd >= 53 && ThisRnd <= 62)
                    ThisRnd += 48 - 53;
                ret += (char)ThisRnd;
            }
            return ret;
        }
        public long Tick() {
            return System.DateTime.Now.Ticks / 10000;
        }
        private void wrExThrow(string str) { }
        private void wrExThrow(string str, string stb) { }
    }
}
