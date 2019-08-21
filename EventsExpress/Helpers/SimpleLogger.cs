using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.Helpers
{
    public class SimpleLogger
    {
        public static void Log(Exception ex)
        {
            Serilog.Log.Debug("ERROR --- " + DateTime.Now.ToString() + " : " + ex.Message);
            Serilog.Log.Debug("ERROR --- " + DateTime.Now.ToString() + " : " + ex.StackTrace);
            if (ex.InnerException != null)
            {
                Serilog.Log.Debug("ERROR INNER --- " + DateTime.Now.ToString() + " : " + ex.InnerException.Message);
                Serilog.Log.Debug("ERROR INNER --- " + DateTime.Now.ToString() + " : " + ex.InnerException.StackTrace);
            }
        }

        public static void Log(string msg)
        {
            Serilog.Log.Information("INFO --- " + DateTime.Now.ToString() + " : " + msg);
        }
    }
}
