// ///-----------------------------------------------------------------
// ///   Class:          IConnectionAuth
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 05.12.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;

namespace LibDataService.ConnectionService
{

    /// <summary>
    /// Basic authentification store User and Password
    /// </summary>
    public class BasicAuthentification 
    {
		protected string _user;
		public string User { 
			get { return _user; } 
			set { _user = value; } 
		}

		protected string _password;
		public string Password { 
			get { return _password; } 
			set { _password = value; } 
		}

        public BasicAuthentification ( string user, string pass ) 
        {
            User = user;
            Password = pass;
        }
    }
}
