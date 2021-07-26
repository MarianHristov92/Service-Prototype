// // ///-----------------------------------------------------------------
// // ///   Class:          ExceptionEventArgs.cs
// // ///   Description:    <Description>
// // ///   Author:         Dimitri Renke                     Date: 10.01.2018
// // ///   Company:        
// // ///   Notes:          <Notes>
// // ///   Revision History:
// // ///-----------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace LibDataService.Exception
{
    public class ExceptionEventArgs : EventArgs
    {
        protected System.Exception _exception;

        public System.Exception Exception => _exception;
        public List<KeyValuePair<string,string>> AdditionInfo;

        public ExceptionEventArgs(System.Exception exception)
        {
            _exception = exception;
        }
    }
}
