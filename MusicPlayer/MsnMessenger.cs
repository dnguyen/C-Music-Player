using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Music
{
    internal static class MsnMessenger
    {
        public static void UpdateMusic(Song track)
        {
            string title = null;
            string artist = null;
            const string album = "Radio Horizonte";

            
                title = track.Title;
                artist = track.Artist;

            var buffer = String.Format("\\0Music\\0{0}\\0{{0}} - {{1}}\\0{1}\\0{2}\\0{3}\\0\\0\0", "0", title, artist, album);

            COPYDATASTRUCT data;
            data.dwData = 0x0547;
            data.lpData = VarPtr(buffer);
            data.cbData = buffer.Length * 2;

            var hWnd = 0;
            do
            {
                hWnd = FindWindowEx(0, hWnd, "MsnMsgrUIManager", null);
                SendMessage(hWnd, WM_COPYDATA, 0, VarPtr(data));
            } while (hWnd != 0);
        }

        private static int VarPtr(object e)
        {
            var gcHandle = GCHandle.Alloc(e, GCHandleType.Pinned);
            var ptr = gcHandle.AddrOfPinnedObject().ToInt32();
            gcHandle.Free();
            return ptr;
        }

        private struct COPYDATASTRUCT
        {
            public int dwData;
            public int cbData;
            public int lpData;
        }

        private const int WM_COPYDATA = 0x04a;

        [DllImport("User32.dll")]
        private static extern int FindWindowEx(int hwndParent, int hwndChildAfter, string strClassName, string strWindowName);

        [DllImport("User32.dll")]
        private static extern Int32 SendMessage(int hWnd, int Msg, int wParam, int lParam);

    }
}
