// // ///-----------------------------------------------------------------
// // ///   Class:          ConnectionContentException.cs
// // ///   Description:    <Description>
// // ///   Author:         Dimitri Renke                     Date: 11.01.2018
// // ///   Company:        
// // ///   Notes:          <Notes>
// // ///   Revision History:
// // ///-----------------------------------------------------------------
using System;
using Flurl.Http;
using System.Net;
using System.Diagnostics;
using LibDataService.Exception;

namespace LibDataService.ConnectionService
{
    public class ConnectionContentException : BaseException
    {
        private string _contentError;
        public string ContentError => _contentError;

    }
}