using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace PizzaMaster.PowerShell.UI
{
    public partial class Zuordnen
    {
        public enum MapType : uint
        {
            MAPVK_VK_TO_VSC = 0x0,
            MAPVK_VSC_TO_VK = 0x1,
            MAPVK_VK_TO_CHAR = 0x2,
            MAPVK_VSC_TO_VK_EX = 0x3
        }

        private readonly ZuordnenViewModel viewModel;

        public Zuordnen(ZuordnenViewModel viewModel)
        {
            viewModel.Finished += this.OnFinished;

            this.DataContext = viewModel;
            this.viewModel = viewModel;
            this.InitializeComponent();
            this.PreviewKeyDown += this.OnPreviewKeyDown;
            this.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 0.9, new Duration(TimeSpan.FromSeconds(0.2))));
        }

        public static char GetCharFromKey(Key key)
        {
            char ch = ' ';

            int virtualKey = KeyInterop.VirtualKeyFromKey(key);
            byte[] keyboardState = new byte[256];
            GetKeyboardState(keyboardState);

            uint scanCode = MapVirtualKey((uint) virtualKey, MapType.MAPVK_VK_TO_VSC);
            StringBuilder stringBuilder = new StringBuilder(2);

            int result = ToUnicode((uint) virtualKey, scanCode, keyboardState, stringBuilder, stringBuilder.Capacity,
                                   0);
            switch (result)
            {
                case -1:
                    break;
                case 0:
                    break;
                case 1:
                {
                    ch = stringBuilder[0];
                    break;
                }
                default:
                {
                    ch = stringBuilder[0];
                    break;
                }
            }
            return ch;
        }

        [DllImport("user32.dll")]
        public static extern bool GetKeyboardState(byte[] lpKeyState);

        [DllImport("user32.dll")]
        public static extern uint MapVirtualKey(uint uCode, MapType uMapType);

        private void OnFinished(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            var c = GetCharFromKey(e.Key);
            if (char.IsLetter(c))
            {
                this.viewModel.SelectedArtikel?.AppendToFilter(c);
                return;
            }

            switch (e.Key)
            {
                case Key.Escape:
                    this.Close();
                    break;
                case Key.Down:
                    this.viewModel.SelectedArtikel?.BenutzerDown();
                    break;
                case Key.Up:
                    this.viewModel.SelectedArtikel?.BenutzerUp();
                    break;
                case Key.Back:
                    this.viewModel.SelectedArtikel?.DeleteLastFilterChar();
                    break;
                case Key.Enter:
                    this.viewModel.SelectNext();
                    break;
                default:
                    return;
            }

            e.Handled = true;
        }

        [DllImport("user32.dll")]
        public static extern int ToUnicode(
            uint wVirtKey,
            uint wScanCode,
            byte[] lpKeyState,
            [Out, MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 4)] StringBuilder pwszBuff,
            int cchBuff,
            uint wFlags);
    }
}