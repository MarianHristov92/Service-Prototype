// Real-App v5	DTWS
// File:			DTWS.cs
// Folder:		/Users/dimtrirenke/Documents/Repo/x_ServicePrototyp/DataServicePrototype/ShowCases
// Author:		Dimitri Renke - dimitri.renke@wfp2.com
// Created:		08.11.2016 - 13:08
// Last Modified	08.11.2016 - 13:08
// Copyright:	
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Http;
using LibDataService;
using System.Collections.Generic;

namespace DataServicePrototype.ShowCases
{
    public class DTWS
    {
        public DTWS ()
        {
        }
        public async void deactivateAccount ()
        {

            try
            {
             using ( var client = new HttpClient () )
                {
                    var values = new Dictionary<string, string>
                    {
                       { "paybackcard", "1170128393" },
                       { "thing2", "activationChannel" }
                    };
                    client.DefaultRequestHeaders.Add ( "Authorization", "Basic " + "aW9zOkFZNnt0RF5jMm4 =" );

                    var content = new FormUrlEncodedContent ( values );
                    var response = await client.PostAsync ( "https://www.real.de/webservices/digitaleTreue/deactivateAccount", content );

                    var responseString = await response.Content.ReadAsStringAsync ();
                    Log.i ( "response string " + responseString );
                }
            }

            catch ( WebException exception )
            {
                string responseText;
                  using (var reader = new StreamReader ( exception.Response.GetResponseStream () )) {
                  responseText = reader.ReadToEnd ();
                    Log.i ( "exception " + responseText );
        
             }
           }
        }
        public async void getStatusAccount ()
        {

            try
            {
                using ( var client = new HttpClient () )
                {
                    var values = new Dictionary<string, string>
                    {
                       { "paybackcard", "1170128393" },
                       { "thing2", "activationChannel" }
                    };
                    client.DefaultRequestHeaders.Add ( "Authorization", "Basic " + "aW9zOkFZNnt0RF5jMm4 =" );

                    var content = new FormUrlEncodedContent ( values );
                    var response = await client.PostAsync ( "https://www.real.de/webservices/digitaleTreue/getAccountStatus", content );

                    var responseString = await response.Content.ReadAsStringAsync ();
                    Log.i ( "response string " + responseString );
                }
            }

            catch ( WebException exception )
            {
                string responseText;
                using ( var reader = new StreamReader ( exception.Response.GetResponseStream () ) )
                {
                    responseText = reader.ReadToEnd ();
                    Log.i ( "exception " + responseText );

                }
            }
        }
    }
}
