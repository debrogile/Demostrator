using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Azelea.Demostrator
{
    public class CallbackTraceListener : CustomTraceListener
    {
        private Queue<string> _SavedLogs = new Queue<string>();
        public Action<string> Callback { get; set; }
        
        private static readonly CallbackTraceListener _Instance = new CallbackTraceListener();
        public static CallbackTraceListener Instance
        {
            get { return _Instance; }
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if (data is LogEntry && Formatter != null)
            {
                WriteLine(Formatter.Format(data as LogEntry));
            }
            else
            {
                WriteLine(data.ToString());
            }
        }

        public override void Write(string message)
        {
            throw new NotImplementedException();
        }

        public override void WriteLine(string message)
        {
            if (Callback != null)
            {
                Callback(message);
            }
            else
            {
                _SavedLogs.Enqueue(message);
            }
        }

        public void ReplaySavedLogs()
        {
            if (Callback != null)
            {
                while (_SavedLogs.Count > 0)
                {
                    var log = _SavedLogs.Dequeue();
                    Callback(log);
                }
            }
        }
    }
}
