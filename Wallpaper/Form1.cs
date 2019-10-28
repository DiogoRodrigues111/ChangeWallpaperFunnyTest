using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using Lib = System.Runtime.InteropServices.DllImportAttribute;
using u32 = System.UInt32;
using BOOL = System.Boolean;
using INT = System.Int32;
using DWORD = System.Int64;
using OBJECT = System.Object;
using IN = System.Runtime.InteropServices.InAttribute;
using OUT = System.Runtime.InteropServices.OutAttribute;
using OPTIONAL = System.Runtime.InteropServices.OptionalAttribute;
using Microsoft.Win32;

namespace Wallpaper
{
    public partial class Form1 : Form
    {
        [Lib("User32.dll", EntryPoint = "SystemParametersInfo")]
        extern private static u32 SystemParametersInfo(
                                                        [IN] u32 uiAction,
                                                        [IN][OPTIONAL] INT uiParam,
                                                        [IN] StringBuilder Str,
                                                        [OUT] INT fWinIni);

        private const u32 SPI_SETDESKWALLPAPER = 0x0014;
        private const u32 SPI_GETDESKWALLPAPER = 0x0073;
        private const INT DEFINE = 0x02;

        public Form1()
        {
            InitializeComponent();
        }

        private u32 SetDesktopWallpaper([IN] String sMsg)
        {
            StringBuilder _StrBuild = new StringBuilder();
            _StrBuild.Append(sMsg);
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, _StrBuild, DEFINE);
            return 0;
        }

        private u32 GetDesktopWallpaper()
        {
            StringBuilder _StrBuild = new StringBuilder(300);
            SystemParametersInfo(SPI_GETDESKWALLPAPER, 0, _StrBuild, 0);
            return 0;
        }

        private static BOOL SetOptions()
        {
            String _Op = "";
            String _OpName = "";
            RegistryKey _CurrentUserRoot = Registry.CurrentUser;
            RegistryKey _CurrentUser = _CurrentUserRoot.OpenSubKey(_Op);

            _CurrentUser.SetValue(_Op, _OpName);

            _CurrentUserRoot.Close();
            _CurrentUser.Close();

            return true;
        }

        private void OnClickButton1([IN] OBJECT sender, [IN] EventArgs e)
        {
            OpenFileDialog OpenFileSystem = new OpenFileDialog();
            var hResult = OpenFileSystem.ShowDialog();

            if(hResult == DialogResult.OK)
            {
                foreach(String s in OpenFileSystem.FileNames)
                {
                    SetDesktopWallpaper(s);
                }
            }
        }
    }
}
