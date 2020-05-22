using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Configuration.Assemblies;

using System.Diagnostics;
using System.IO;
using System.Xml;

namespace SQSAdmin_WpfCustomControlLibrary
{
    public static class CommonVariables
    {
        private static string _sEnvironment = "LIVE";
        private static int _userid;
        private static string _usercode;
        private static string _wcfEndpoint;
        private static string _windowtitleinfo;

        public static string Environment
        {
            get { return _sEnvironment; }
            set
            {
                _sEnvironment = value;

            }
        }
        
        public static int UserID
        {
            get { return _userid; }
            set
            {
                _userid = value;

            }
        }

        public static string UserCode
        {
            get { return _usercode; }
            set
            {
                _usercode = value;

            }
        }

        public static string WcfEndpoint
        {
            get { return _wcfEndpoint; }
            set
            {
                _wcfEndpoint = value;
            }
        }

        public static string WindowTitleInfo
        {
            get { return _windowtitleinfo; }
            set
            {
                _windowtitleinfo = value;

            }
        }
    }
}
