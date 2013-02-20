using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Pogs.WinUI.Utilities
{
    internal static class LogUtility
    {
        internal static void Initialize(string path, string username)
        {
            if (!Directory.Exists(path))
                throw new ArgumentException("Could not find trace log directory, please contact your administrator.");

            TextWriterTraceListener objTL = new TextWriterTraceListener(path + username + " " + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + " trace.log");

            //Remove all existing listeners.
            System.Diagnostics.Trace.Listeners.Clear();

            //Add the newly created listener to the collection.
            System.Diagnostics.Trace.Listeners.Add(objTL);
            System.Diagnostics.Trace.AutoFlush = true;
            Log(username + " started session.");
        }

        public static void Log(string s)
        {
            Console.WriteLine(s);
            Debug.WriteLine(DateTime.Now.ToString() + ": " + s);
        }
    }
}