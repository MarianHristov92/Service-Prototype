// // ///-----------------------------------------------------------------
// // ///   Class:          ConnectionHelper.cs
// // ///   Description:    <Description>
// // ///   Author:         ban                     Date: 11/18/2019
// // ///   Company:        
// // ///   Notes:          <Notes>
// // ///   Revision History:
// // ///-----------------------------------------------------------------
using System;
using Xamarin.Essentials;

namespace LibDataService.Helper
{
    public static class ConnectionHelper
    {
        private static bool _hasInternet = true;
        private static int _defaultTimeout = 10;

        public static bool HasInternet
        {
            get
            {
                return _hasInternet;
            }
            set
            {
                if (value == _hasInternet) return;
                _hasInternet = value;
            }
        }

        public static int DefaultTimeout
        {
            get { return _defaultTimeout; }
        }

        public static void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess == NetworkAccess.Internet)
                HasInternet = true;
            else
                HasInternet = false;
        }
    }
}
