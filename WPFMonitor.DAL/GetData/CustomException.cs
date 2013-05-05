using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WPFMonitor.DAL.GetData
{
    public class CustomException
    {
        public string Message { get; set; }
        public CustomException InnerException;

        public CustomException(Exception ex)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;
            this.Message = ex.Message + ex.StackTrace;
        }
        public Exception ToException()
        {
            Exception e;
            CustomException ce = this;
            if (ce.InnerException != null)
            {
                Exception inner = ce.InnerException.ToException();
                e = new Exception(ce.Message, inner);
            }
            else
                e = new Exception(ce.Message);
            return e;
        }
    }
}

