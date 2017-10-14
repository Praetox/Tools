using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ZC
{
    public partial class frmBackground : Form
    {
        public frmBackground()
        {
            InitializeComponent();
        }

        public void SetWallpaper()
        {
            Microsoft.Win32.RegistryKey key =
                Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", true);
            string WPPath = key.GetValue("Wallpaper").ToString();
            string WPStyle = key.GetValue("WallpaperStyle").ToString();
            string WPTile = key.GetValue("TileWallpaper").ToString();
            BGImage.BackgroundImage = (new Bitmap(WPPath)) as Image;
            if ((WPStyle == "2") && (WPTile == "0")) BGImage.BackgroundImageLayout = ImageLayout.Stretch;
            if ((WPStyle == "0") && (WPTile == "0")) BGImage.BackgroundImageLayout = ImageLayout.Center;
            if ((WPStyle == "0") && (WPTile == "1")) BGImage.BackgroundImageLayout = ImageLayout.Tile;
            key.Close();
        }
        public void SetColor()
        {
            Microsoft.Win32.RegistryKey key =
                Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Control Panel\\Colors", true);
            string Colors = key.GetValue("Background").ToString();
            BGImage.BackColor = Color.FromArgb(
                Convert.ToInt32(Colors.Split(' ')[0]),
                Convert.ToInt32(Colors.Split(' ')[1]),
                Convert.ToInt32(Colors.Split(' ')[2]));
            key.Close();
        }

        private void frmBackground_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0); this.Size = Screen.PrimaryScreen.Bounds.Size;
            BGImage.Dock = DockStyle.Fill; SetWallpaper(); SetColor();
            SetWallpaper(); SetColor();
        }
    }
}