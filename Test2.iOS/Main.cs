// ///-----------------------------------------------------------------
// ///   Class:          Main
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 11.11.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using System.Linq;
using System.Collections.Generic;

using Foundation;
using UIKit;

namespace Test2.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main ( string[] args )
        {
            // if you want to use a different Application Delegate class from "UnitTestAppDelegate"
            // you can specify it here.
            UIApplication.Main ( args, null, "UnitTestAppDelegate" );
        }
    }
}
