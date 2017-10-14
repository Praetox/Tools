using System;
using System.Runtime.InteropServices;

namespace ZC
{	
  
  public enum IconSize
  {
    Small,
    Large
  }

  /// <summary>
  /// 
  /// Extracts the icon associated with any file on your system.
  /// Author: WidgetMan http://softwidgets.com
  /// 
  /// </summary>
  /// <remarks>
  /// 
  /// Class requires the IconSize enumeration that is implemented in this
  /// same file. For best results, draw an icon from within a control's Paint
  /// event via the e.Graphics.DrawIcon method.
  /// 
  /// </remarks>  
	public class IconExtractor
	{

    private const uint SHGFI_ICON = 0x100;
    private const uint SHGFI_LARGEICON = 0x0;
    private const uint SHGFI_SMALLICON = 0x1;

    [StructLayout(LayoutKind.Sequential)]
    private struct SHFILEINFO 
    {
      public IntPtr hIcon;
      public IntPtr iIcon;
      public uint dwAttributes;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
      public string szDisplayName;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
      public string szTypeName;
    };

    [DllImport("shell32.dll")]
    private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

		public IconExtractor()
		{
		}

    public System.Drawing.Icon Extract(string File, IconSize Size)
    {
      IntPtr hIcon; 
      SHFILEINFO shinfo = new SHFILEINFO();

      if (Size == IconSize.Large)
      {
        hIcon = SHGetFileInfo(File, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_LARGEICON);
      }
      else
      {
        hIcon = SHGetFileInfo(File, 0, ref shinfo,(uint)Marshal.SizeOf(shinfo), SHGFI_ICON | SHGFI_SMALLICON);
      }      
      
      return System.Drawing.Icon.FromHandle(shinfo.hIcon);
    }

    public System.Drawing.Icon Extract(string File)
    {
      return this.Extract(File, IconSize.Small);
    }
	}
}
