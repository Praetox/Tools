/*  SMSuite -- StepMania MIGS calculator and stats display
 *  Copyright (C) 2009  Praetox (http://praetox.com/)
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
using System.Diagnostics;
using System.Runtime.InteropServices;

public class SM {
    Memory mem;
    SMConstants smc;
    SMConstants smcb;
    public SM(Process proc, Mode mode) {
        if (mode == Mode.VS) {
            smc = new SMConstants(Mode.P1);
            smcb = new SMConstants(Mode.P2);
        } else {
            smc = new SMConstants(mode);
        }
        mem = new Memory(proc);
        this.mode = mode;
    }
    public enum Mode { None, P1, P2, VS };
    public Mode mode;

    public string App_Mode {
        get {
            if (mode == Mode.P1) return "P1";
            if (mode == Mode.P2) return "P2";
            if (mode == Mode.VS) return "VS";
            return "WTF";
        }
        set { }
    }
    public int Dance_MIGS {
        get {
            return
                Count_Marv * +3 +
                Count_Perf * +2 +
                Count_Gret * +1 +
                Count_Good * +0 +
                Count_Booo * -4 +
                Count_Miss * -8 +
                Count_Okay * +6;
        }
        set {
            throw new Exception("WHY THE HELL WOULD YOU " +
                "ASSIGN TO THE MIGS SCORE YOU FLAMING FAGGOT");
        }
    }
    public int Dance_nPts {
        get {
            if (mode == Mode.VS) return
                mem.RInt(smc.Dance_nPts, 4) -
                mem.RInt(smcb.Dance_nPts, 4);
            else return mem.RInt(smc.Dance_nPts, 4);
        }
        set { mem.WInt(smc.Dance_nPts, value); }
    }
    public int Count_Cmbo {
        get {
            if (mode == Mode.VS) return
                mem.RInt(smc.Count_Cmbo, 4) -
                mem.RInt(smcb.Count_Cmbo, 4);
            else return mem.RInt(smc.Count_Cmbo, 4);
        }
        set { mem.WInt(smc.Count_Cmbo, value); }
    }
    public int Count_Marv {
        get {
            if (mode == Mode.VS) return
                mem.RInt(smc.Count_Marv, 4) -
                mem.RInt(smcb.Count_Marv, 4);
            return mem.RInt(smc.Count_Marv, 4);
        }
        set { mem.WInt(smc.Count_Marv, value); }
    }
    public int Count_Perf {
        get { 
            if (mode == Mode.VS) return
                mem.RInt(smc.Count_Perf, 4) -
                mem.RInt(smcb.Count_Perf, 4);
            else return mem.RInt(smc.Count_Perf, 4);
        }
        set { mem.WInt(smc.Count_Perf, value); }
    }
    public int Count_Gret {
        get {
            if (mode == Mode.VS) return
                mem.RInt(smc.Count_Gret, 4) -
                mem.RInt(smcb.Count_Gret, 4);
            else return mem.RInt(smc.Count_Gret, 4);
        }
        set { mem.WInt(smc.Count_Gret, value); }
    }
    public int Count_Good {
        get {
            if (mode == Mode.VS) return
                mem.RInt(smc.Count_Good, 4) -
                mem.RInt(smcb.Count_Good, 4);
            else return mem.RInt(smc.Count_Good, 4);
        }
        set { mem.WInt(smc.Count_Good, value); }
    }
    public int Count_Booo {
        get {
            if (mode == Mode.VS) return
                mem.RInt(smc.Count_Booo, 4) -
                mem.RInt(smcb.Count_Booo, 4);
            else return mem.RInt(smc.Count_Booo, 4);
        }
        set { mem.WInt(smc.Count_Booo, value); }
    }
    public int Count_Miss {
        get {
            if (mode == Mode.VS) return
                mem.RInt(smc.Count_Miss, 4) -
                mem.RInt(smcb.Count_Miss, 4);
            else return mem.RInt(smc.Count_Miss, 4);
        }
        set { mem.WInt(smc.Count_Miss, value); }
    }
    public int Count_Okay {
        get {
            if (mode == Mode.VS) return
                mem.RInt(smc.Count_Okay, 4) -
                mem.RInt(smcb.Count_Okay, 4);
            else return mem.RInt(smc.Count_Okay, 4);
        }
        set { mem.WInt(smc.Count_Okay, value); }
    }
}
public class SMConstants {
    SM.Mode mode;
    public SMConstants(SM.Mode mode) {
        this.mode = mode;
    }
    public int Dance_nPts {
        get { return (mode == SM.Mode.P1) ? 0x75d4a4 : 0x75d4a8; }
        set { } //Dance points
    }
    public int Count_Cmbo {
        get { return (mode == SM.Mode.P1) ? 0x75d494 : 0x75d498; }
        set { } //Max combo
    }
    public int Count_Marv {
        get { return (mode == SM.Mode.P1) ? 0x75d450 : 0x75d470; }
        set { } //Marvelous
    }
    public int Count_Perf {
        get { return (mode == SM.Mode.P1) ? 0x75d44c : 0x75d46c; }
        set { } //Perfect
    }
    public int Count_Gret {
        get { return (mode == SM.Mode.P1) ? 0x75d448 : 0x75d468; }
        set { } //Great
    }
    public int Count_Good {
        get { return (mode == SM.Mode.P1) ? 0x75d444 : 0x75d464; }
        set { } //Good
    }
    public int Count_Booo {
        get { return (mode == SM.Mode.P1) ? 0x75d440 : 0x75d460; }
        set { } //Boo
    }
    public int Count_Miss {
        get { return (mode == SM.Mode.P1) ? 0x75d43c : 0x75d45c; }
        set { } //Miss
    }
    public int Count_Okay {
        get { return (mode == SM.Mode.P1) ? 0x75d47c : 0x75d488; }
        set { } //OK
    }
}
public class Memory {
    #region API

    [DllImport("kernel32")]
    public static extern IntPtr OpenProcess(UInt32 dwDesiredAccess, Int32 bInheritHandle, UInt32 dwProcessId);
    [DllImport("kernel32")]
    public static extern long ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, [Out()] byte[] lpBuffer, int nSize, int lpNumberOfBytesWritten);
    [DllImport("kernel32")]
    public static extern long WriteProcessMemory(IntPtr hProcess, int lpBaseAddress, byte[] lpBuffer, int nSize, int lpNumberOfBytesWritten);
    [DllImport("kernel32")]
    public static extern Int32 CloseHandle(IntPtr hObject);

    public const uint PROC_OPER = 0x000008;
    public const uint PROC_READ = 0x000010;
    public const uint PROC_WRIT = 0x000020;
    public const uint PROC_FULL = 0x1F0FFF;
    private Process m_ReadProcess = null;
    private IntPtr m_hProcess = IntPtr.Zero;

    #endregion
    public Memory(Process proc) {
        m_ReadProcess = proc;
        m_hProcess = OpenProcess(PROC_FULL,
            1, (uint)m_ReadProcess.Id);
    }
    public void Dispose() {
        int iRetValue;
        iRetValue = CloseHandle(m_hProcess);
        if (iRetValue == 0) throw new
            Exception("CloseHandle failed");
    }
    #region Read memory
    public byte[] RBytes(int addr, int len) {
        byte[] buf = new byte[len];
        ReadProcessMemory(m_hProcess,
            addr, buf, len, 0);
        return buf;
    }
    public int RInt(int addr, int len) {
        byte[] bytes = RBytes(addr, len);
        return BitConverter.ToInt32(bytes, 0);
    }
    #endregion
    #region Write memory
    public void WBytes(int addr, byte[] var) {
        WriteProcessMemory(m_hProcess, addr,
            var, var.Length, var.Length);
    }
    public void WInt(int addr, int num) {
        WBytes(addr, BitConverter.GetBytes(num));
    }
    #endregion
}