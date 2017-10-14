/*  pUnPod - Listen to (and extract and rename) music from iPods.
 *  Copyright (C) 2006-2009 Praetox (http://praetox.com/)
 *  
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *  
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *  
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pUnPod
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        int t2aDelay = 0;
        byte[] bdBase = new byte[] { 0x6d, 0x68, 0x73, 0x64, 0x60, 0x00 };
        byte[] bdSong = new byte[] { 0x6d, 0x68, 0x69, 0x74, 0xb4, 0x01 };
        byte[] bdInfo = new byte[] { 0x6d, 0x68, 0x6f, 0x64, 0x18, 0x00 };
        SongInf[] DB = new SongInf[0];
        string Path_App = "";
        string Path_Pod = "";
        MP3 Media = new MP3();
        int iPlaying = -1;

        private bool cmp(int i1, byte[] b1, byte[] b2)
        {
            if (b1 == null || b2 == null) return false;
            if (b1.Length < b2.Length + i1) return false;
            for (int a = 0; a < b2.Length; a++)
            {
                if (b1[a + i1] != b2[a]) return false;
            }
            return true;
        }
        private bool cmp(byte[] b1, byte[] b2)
        {
            return cmp(0, b1, b2);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            frmLoad fl = new frmLoad();
            fl.ShowDialog();
            Path_Pod = fl.podPath + ":\\";
            Path_App = Application.StartupPath;
            if (!Path_App.EndsWith("\\")) Path_App += "\\";

            this.Show(); Application.DoEvents();
            lbLoading.BringToFront(); Application.DoEvents();

            System.IO.File.Copy(
                Path_Pod + "iPod_Control\\iTunes\\iTunesDB",
                Path_App + "iTunesDB", true);
            System.IO.FileStream file = new System.IO.FileStream(
                Path_App + "iTunesDB", System.IO.FileMode.Open);
            byte[] bDB = new byte[file.Length];
            file.Read(bDB, 0, (int)file.Length);
            int iSong = 0; int iOfsSong = 0;
            while (true)
            {
                if (cmp(iOfsSong, bDB, bdBase)) break;
                iOfsSong++;
            }
            iOfsSong++;
            while (true)
            {
                if (cmp(iOfsSong, bDB, bdSong))
                {
                    int iOfsInfo = iOfsSong; iSong++;
                    //if (iSong.ToString().EndsWith("00"))
                    //    lbLoading.Text = "Reading song " + iSong; Application.DoEvents();
                    SongInf sInfSong = new SongInf();
                    while (true)
                    {
                        if (cmp(iOfsInfo, bDB, bdInfo))
                        {
                            string sData = "";
                            int iOfsData1 = iOfsInfo + 40;
                            int iOfsData2 = (bDB[iOfsInfo + 8] + iOfsInfo) - 1;
                            while (iOfsData1 < iOfsData2)
                            {
                                //Fixed: Support for Kanji (also known as moonrunes)
                                sData += Convert.ToChar((bDB[iOfsData1]*1) + (bDB[iOfsData1+1]*256));
                                iOfsData1 += 2;
                            }
                            if (bDB[iOfsInfo + 12] == 0x01) sInfSong.n01_Title = sData;
                            if (bDB[iOfsInfo + 12] == 0x02) sInfSong.n02_Path = sData;
                            if (bDB[iOfsInfo + 12] == 0x03) sInfSong.n03_Album = sData;
                            if (bDB[iOfsInfo + 12] == 0x04) sInfSong.n04_Artist = sData;
                            if (bDB[iOfsInfo + 12] == 0x05) sInfSong.n05_Genre = sData;
                            if (bDB[iOfsInfo + 12] == 0x06) sInfSong.n06_Format = sData;
                            if (bDB[iOfsInfo + 12] == 0x07) sInfSong.n07_ = sData;
                            if (bDB[iOfsInfo + 12] == 0x08) sInfSong.n08_Comment = sData;
                            if (bDB[iOfsInfo + 12] == 0x09) sInfSong.n09_ = sData;
                            if (bDB[iOfsInfo + 12] == 0x0A) sInfSong.n0A_ = sData;
                            if (bDB[iOfsInfo + 12] == 0x0B) sInfSong.n0B_ = sData;
                            if (bDB[iOfsInfo + 12] == 0x0c) sInfSong.n0C_Composer = sData;
                            if (bDB[iOfsInfo + 12] == 0x0d) sInfSong.n0D_Grouping = sData;
                            if (bDB[iOfsInfo + 12] == 0x0E) sInfSong.n0E_ = sData;
                            if (bDB[iOfsInfo + 12] == 0x0F) sInfSong.n0F_ = sData;
                            if (bDB[iOfsInfo + 12] == 0x10) sInfSong.n10_ = sData;
                            if (bDB[iOfsInfo + 12] == 0x11) sInfSong.n11_ = sData;
                            if (bDB[iOfsInfo + 12] == 0x12) sInfSong.n12_ = sData;
                            if (bDB[iOfsInfo + 12] == 0x13) sInfSong.n13_ = sData;
                            if (bDB[iOfsInfo + 12] == 0x14) sInfSong.n14_ = sData;
                            if (bDB[iOfsInfo + 12] == 0x15) sInfSong.n15_ = sData;
                            if (bDB[iOfsInfo + 12] == 0x16) sInfSong.n16_AlbumArtist = sData;
                            iOfsInfo = iOfsData1;
                        }
                        else iOfsInfo += 1;
                        iOfsSong = iOfsInfo;
                        if (cmp(iOfsSong, bDB, bdSong) ||
                            cmp(iOfsSong, bDB, bdBase))
                        {
                            if (true) //(sInfSong.n02_Path.Contains("Music:F0"))
                            {
                                if (sInfSong.n04_Artist == "")
                                {
                                    if (sInfSong.n01_Title.Contains(" - "))
                                    {
                                        string tmp = sInfSong.n01_Title;
                                        sInfSong.n01_Title = tmp.Substring(tmp.IndexOf(" - ") + " - ".Length);
                                        sInfSong.n04_Artist = tmp.Substring(0, tmp.IndexOf(" - "));
                                    }
                                }

                                SongInf[] tmpSongInf = DB;
                                DB = new SongInf[DB.Length + 1];
                                for (int a = 0; a < DB.Length - 1; a++)
                                {
                                    DB[a] = tmpSongInf[a];
                                }
                                DB[DB.Length - 1] = sInfSong;
                            }
                            break;
                        }
                    }
                }
                else iOfsSong += 1;
                if (cmp(iOfsSong, bDB, bdBase)) break;
            }
            this.Show(); Application.DoEvents();
            MessageBox.Show("Parsed " + DB.Length + " songs from iTunes database.");
            DG.RowCount = DB.Length;
            for (int a = 0; a < DB.Length; a++)
            {
                DG[0, a].Value = (a+1).ToString().PadLeft(6, '0');
                DG[1, a].Value = DB[a].n01_Title;
                DG[2, a].Value = DB[a].n04_Artist;
                DG[3, a].Value = DB[a].n03_Album;
                DG[4, a].Value =
                    DB[a].n02_Path.Split(':')[3] + " - " +
                    DB[a].n02_Path.Split(':')[4]; ;
            }
            lbLoading.SendToBack();

            nIco.Icon = this.Icon;
            nIco.Text = "pUnPod";
            nIco.Visible = true;
        }

        #region GUI aids
        private void lbVol1_MouseDown(object sender, MouseEventArgs e)
        {
            int X = e.X;
            if (e.Button != MouseButtons.Left) return;
            if (X > lbVol2.Width - 4) X = lbVol2.Width - 4;
            if (X < 0) X = 0;
            lbVol1.Width = X;
        }
        private void lbVol1_MouseMove(object sender, MouseEventArgs e)
        {
            int X = e.X;
            if (e.Button != MouseButtons.Left) return;
            if (X > lbVol2.Width - 4) X = lbVol2.Width - 4;
            if (X < 0) X = 0;
            lbVol1.Width = X;
        }
        private void lbVol2_MouseDown(object sender, MouseEventArgs e)
        {
            int X = e.X-2;
            if (e.Button != MouseButtons.Left) return;
            if (X > lbVol2.Width-4) X=lbVol2.Width-4;
            if (X < 0) X=0;
            lbVol1.Width = X;
        }
        private void lbVol2_MouseMove(object sender, MouseEventArgs e)
        {
            int X = e.X-2;
            if (e.Button != MouseButtons.Left) return;
            if (X > lbVol2.Width-4) X=lbVol2.Width-4;
            if (X < 0) X=0;
            lbVol1.Width = X;
        }

        private void lbPrg1_MouseDown(object sender, MouseEventArgs e)
        {
            int X = e.X;
            if (e.Button != MouseButtons.Left) return;
            if (X > lbPrg2.Width - 4) X = lbPrg2.Width - 4;
            if (X < 0) X = 0;
            lbPrg1.Width = X;
            mpJumpTo(X);
        }
        private void lbPrg1_MouseMove(object sender, MouseEventArgs e)
        {
            int X = e.X;
            if (e.Button != MouseButtons.Left) return;
            if (X > lbPrg2.Width - 4) X = lbPrg2.Width - 4;
            if (X < 0) X = 0;
            lbPrg1.Width = X;
            mpJumpTo(X);
        }
        private void lbPrg2_MouseDown(object sender, MouseEventArgs e)
        {
            int X = e.X - 2;
            if (e.Button != MouseButtons.Left) return;
            if (X > lbPrg2.Width - 4) X = lbPrg2.Width - 4;
            if (X < 0) X = 0;
            lbPrg1.Width = X;
            mpJumpTo(X);
        }
        private void lbPrg2_MouseMove(object sender, MouseEventArgs e)
        {
            int X = e.X - 2;
            if (e.Button != MouseButtons.Left) return;
            if (X > lbPrg2.Width - 4) X = lbPrg2.Width - 4;
            if (X < 0) X = 0;
            lbPrg1.Width = X;
            mpJumpTo(X);
        }
        #endregion

        private void cmdPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.RootFolder = Environment.SpecialFolder.MyComputer;
            fbd.ShowDialog(); string sPath = fbd.SelectedPath;
            if (!sPath.EndsWith("\\")) sPath += "\\";
            if (fbd.SelectedPath != "") txtPath.Text = sPath;
        }

        private void DG_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            iPlaying = e.RowIndex;
            LoadCurrentMedia();
        }

        private void LoadCurrentMedia()
        {
            lbLoading.BringToFront(); Application.DoEvents();
            string sPodPath = Path_Pod + "iPod_Control\\Music\\" +
                DG[4, iPlaying].Value.ToString().Replace(" - ", "\\");

            Media.Close();
            System.IO.File.Copy(sPodPath,
                Path_App + "media.mp3", true);
            Media.Open(Path_App + "media.mp3");
            Media.Play();

            lbLoading.SendToBack(); Application.DoEvents();
            int iDB = Convert.ToInt32(DG[0, iPlaying].Value.ToString())-1;
            nIco.ShowBalloonTip(1000, "[pUP] Now playing",
                "Title: " + DB[iDB].n01_Title + "\r\n" +
                "Artist: " + DB[iDB].n04_Artist + "\r\n" +
                "Album: " + DB[iDB].n03_Album, ToolTipIcon.Info);
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Kill();
        }

        private void tProgress_Tick(object sender, EventArgs e)
        {
            double dPos = (double)(Media.Pos());
            double dLen = (double)(Media.Len());
            if (dLen > 0)
            {
                lbPrg1.Width = (int)(((double)(lbPrg2.Width - 4) / dLen) * dPos);
            }
        }
        private void mpJumpTo(Int32 iWidth)
        {
            double dPos = (double)(Media.Pos());
            double dLen = (double)(Media.Len());
            if (dLen > 0)
            {
                Int32 ms = (Int32)((dLen / (double)(lbPrg2.Width - 4)) * (double)(lbPrg1.Width));
                Media.iPos = ms; Media.Play();
            }
        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            Media.Open("media.mp3");
        }
        
        private void cmdPlay_Click(object sender, EventArgs e)
        {
            if (!Media.isPlaying()) Media.Play(); else Media.Pause();
        }

        private void cmdStop_Click(object sender, EventArgs e)
        {
            Media.Stop();
        }

        private void cmdUnload_Click(object sender, EventArgs e)
        {
            Media.Close();
        }

        private void cmdPrev_Click(object sender, EventArgs e)
        {
            iPlaying--;
            Media.Close();
            Media.Open(Path_Pod + DG[4, iPlaying].ToString().Replace(" - ", "\\"));
            Media.Play();
        }

        private void cmdNext_Click(object sender, EventArgs e)
        {
            iPlaying++;
            
            Media.Play();
        }

        private void cmdCopy_Click(object sender, EventArgs e)
        {
            lbLoading.BringToFront();
            lbLoading.Text = "Preparing dump..."; Application.DoEvents();

            int iTotal = 0;
            bool[] bCopy = new bool[DB.Length];
            for (int a = 0; a < DG.RowCount; a++)
            {
                if (DG[0, a].Selected)
                {
                    int i = Convert.ToInt32(DG[0, a].Value.ToString()) - 1;
                    bCopy[i] = true; iTotal++;
                }
            }

            lbLoading.TextAlign = ContentAlignment.TopLeft;
            lbLoading.Text = "Dumping " + iTotal + " songs..."; Application.DoEvents();
            for (int a = 0; a < DB.Length; a++)
            {
                if (bCopy[a])
                {
                    DB[a].n01_Title = DB[a].n01_Title.Trim(new char[] { ' ', '\t', '\r', '\n' });
                    DB[a].n03_Album = DB[a].n03_Album.Trim(new char[] { ' ', '\t', '\r', '\n' });
                    DB[a].n04_Artist = DB[a].n04_Artist.Trim(new char[] { ' ', '\t', '\r', '\n' });

                    lbLoading.Text = "Song [" + a + "] of [" + iTotal + "]" +
                        "\r\n\r\n" +
                        "  [TITLE] " + DB[a].n01_Title + "\r\n" +
                        "[ARTIST] " + DB[a].n04_Artist + "\r\n" +
                        "[ALBUM] " + DB[a].n03_Album + "\r\n\r\n";
                    Application.DoEvents();

                    string sFName = "";
                    string sPath = txtPath.Text;
                    string sFType = "." + DB[a].n02_Path.Split('.')[1];

                    string sFPath = DB[a].n03_Album
                        .Replace("\\", "[bs]").Replace("/", "[fs]").Replace(":", "[co]")
                        .Replace("\"", "[qt]").Replace("*", "[as]").Replace("?", "[qm]")
                        .Replace("<", "[lt]").Replace(">", "[gt]").Replace("|", "[pi]");
                    
                    if (DB[a].n03_Album != "") sPath += sFPath + "\\";
                    else sPath += "no!album" + "\\";
                    if (DB[a].n04_Artist != "") sFName += DB[a].n04_Artist + " - ";
                    else sFName += "no!artist" + " - ";
                    if (DB[a].n01_Title != "") sFName += DB[a].n01_Title;
                    else sFName += "no!title";

                    sFName = sFName
                        .Replace("\\", "[bs]").Replace("/", "[fs]").Replace("?", "[qm]")
                        .Replace("<", "[lt]").Replace(">", "[gt]").Replace("|", "[pi]")
                        .Replace("*", "[as]").Replace("\"", "''").Replace(":", "[co]");

                    bool bAddNum = false; int iNum = 2;
                    if (System.IO.File.Exists(sPath + sFName + sFType)) bAddNum = true;
                    while (System.IO.File.Exists(sPath + sFName + " (" + iNum + ")" + sFType))
                        iNum++;

                    lbLoading.Text += (sPath + sFName + sFType)
                        .Replace("\\", " \\ ").Replace("/", " / ");
                    Application.DoEvents();
                    if (!System.IO.Directory.Exists(sPath))
                        System.IO.Directory.CreateDirectory(sPath);

                    if (bAddNum) sFName += " (" + iNum + ")";
                    System.IO.File.Copy(Path_Pod.Substring(0, 2) +
                        DB[a].n02_Path.Replace(':', '\\'), sPath + sFName + sFType);

                }
            }

            lbLoading.TextAlign = ContentAlignment.MiddleCenter;
            lbLoading.Text = "Done."; Application.DoEvents();
            System.Threading.Thread.Sleep(1000);
            DG.BringToFront();
        }
    }

    public class SongInf
    {
        public string n01_Title = "";
        public string n02_Path = "";
        public string n03_Album = "";
        public string n04_Artist = "";
        public string n05_Genre = "";
        public string n06_Format = "";
        public string n07_ = "";
        public string n08_Comment = "";
        public string n09_ = "";
        public string n0A_ = "";
        public string n0B_ = "";
        public string n0C_Composer = "";
        public string n0D_Grouping = "";
        public string n0E_ = "";
        public string n0F_ = "";
        public string n10_ = "";
        public string n11_ = "";
        public string n12_ = "";
        public string n13_ = "";
        public string n14_ = "";
        public string n15_ = "";
        public string n16_AlbumArtist = "";
        public string n17_ = "";
        public string n18_ = "";
        public string n19_ = "";
        public string n1A_ = "";
        public string n1B_ = "";
        public string n1C_ = "";
        public string n1D_ = "";
        public string n1E_ = "";
        public string n1F_ = "";
    }

    public class MP3
    {
        private bool isOpen;
        public int iPos = 0;

        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern long mciSendString(
            string strCommand,          // The command to execute
            StringBuilder strReturn,    // The returned string (if any)
            int iReturnLength,          // The bitcount of the returned string
            IntPtr hwndCallback);       // Callback value of spec. handle
        public MP3()
        {
        }

        public void Open(string sFileName)
        {
            MCI("open \"" + sFileName + "\" type mpegvideo alias Praetox");
            MCI("set Praetox time format ms");
            iPos = 0; isOpen = true;
        }

        public void Play()
        {
            if (isOpen)
            {
                MCI("play Praetox from " + iPos);
            }
        }

        public void Pause()
        {
            if (isOpen)
            {
                MCI("stop Praetox");
                iPos = Convert.ToInt32(MCI("status Praetox position"));
            }
        }

        public void Stop()
        {
            if (isOpen)
            {
                MCI("stop Praetox");
                iPos = 0;
            }
        }

        public void Close()
        {
            MCI("close Praetox");
            iPos = 0; isOpen = false;
        }

        public bool isPlaying()
        {
            return (MCI("status Praetox mode") == "playing");
        }
        public Int32 Pos()
        {
            if (isPlaying()) return Convert.ToInt32(MCI("status Praetox position"));
            return 0;
        }
        public Int32 Len()
        {
            if (isPlaying()) return Convert.ToInt32(MCI("status Praetox length"));
            return 0;
        }

        public string MCI(string qStr)
        {
            StringBuilder ret = new StringBuilder(255);
            mciSendString(qStr, ret, 255, (IntPtr)0);
            return ret.ToString();
        }
    }
}
