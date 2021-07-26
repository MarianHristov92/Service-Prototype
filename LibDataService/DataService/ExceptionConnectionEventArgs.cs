// // ///-----------------------------------------------------------------
// // ///   Class:          ExceptionConnectionEventArgs.cs
// // ///   Description:    <Description>
// // ///   Author:         Dimitri Renke                     Date: 12.01.2018
// // ///   Company:        
// // ///   Notes:          <Notes>
// // ///   Revision History:
// // ///-----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Flurl.Http;
using LibDataService.Exception;

namespace LibDataService.Exception
{
    public class ExceptionConnectionEventArgs : ExceptionEventArgs
    {
        protected HttpCall _call;

        public ExceptionConnectionEventArgs(HttpCall call) : base(call.Exception)
        {
            _call = call;
            _exception = call.Exception;
            AdditionInfo = GenerateBaseHttpInfos(call, null);

        }

        public ExceptionConnectionEventArgs(System.Exception exception, HttpCall call) : base(exception)
        {
            _call = call;
            _exception = exception;
            AdditionInfo = GenerateBaseHttpInfos(call, null);
        }

        protected List<KeyValuePair<string, string>> GenerateBaseHttpInfos(HttpCall call, List<KeyValuePair<string, string>> additionalInfo)
        {
            if (additionalInfo == null)
            {
                additionalInfo = new List<KeyValuePair<string, string>>();
            }
            if (call == null) return additionalInfo;
            try
            {
                additionalInfo.Add(new KeyValuePair<string, string>("HTTP-Status", call.HttpStatus.ToString()));
                additionalInfo.Add(new KeyValuePair<string, string>("HTTP-Method", call.Request.Method.ToString()));
                additionalInfo.Add(new KeyValuePair<string, string>("RequestUri", call.Request.RequestUri.ToString()));
                //additionalInfo.Add(new KeyValuePair<string, string>("ErrorResponseBody", call.ErrorResponseBody));
                additionalInfo.Add(new KeyValuePair<string, string>("StartedUtc", call.StartedUtc.ToString()));
                additionalInfo.Add(new KeyValuePair<string, string>("StartedUtc", call.EndedUtc.ToString()));
                additionalInfo.Add(new KeyValuePair<string, string>("EndedUtc", call.EndedUtc.ToString()));
                additionalInfo.Add(new KeyValuePair<string, string>("Duration", call.Duration.ToString()));
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
            return additionalInfo;
        }
    }
}
