using System;
using System.IO;
using System.Threading;

namespace FirstToolWin.Utilities
{
    public class Logger
    {
#if DEBUG
        static Progress pr;
#endif
        public static void Log(object obj, IMEventArgs args)
        {
#if DEBUG
            if (pr == null)
            {
                pr = new Progress();
                pr.Show();
            }
            pr._UpdateConsole(obj, args);

            
#endif

            // write to file
            if (!System.IO.Directory.Exists("C:\\TEMP\\"))
                System.IO.Directory.CreateDirectory("C:\\TEMP\\");

            if (!File.Exists("C:\\TEMP\\log_FirstToolWin.txt"))
                File.Create("C:\\TEMP\\log_FirstToolWin.txt");

            System.IO.File.AppendAllText("C:\\TEMP\\log_FirstToolWin.txt", args.Message);
        }

    }

    public class IMEventArgs : EventArgs
    {
        readonly string _msg;

        public IMEventArgs()
            : this("")
        { }
        public IMEventArgs(string msg)
        {
            this._msg = msg;
        }
        static public implicit operator IMEventArgs(string s)
        {
            return new IMEventArgs(s);
        }

        public string Message
        {
            get
            {
                return _msg;
            }
        }
    }
}