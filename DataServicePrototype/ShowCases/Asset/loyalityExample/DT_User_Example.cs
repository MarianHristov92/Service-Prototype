// Real-App v5	DT_User_Example
// File:			DT_User_Example.cs
// Folder:		/Users/dimtrirenke/Documents/Repo/x_ServicePrototyp/DataServicePrototype/ShowCases/Asset/loyalityExample
// Author:		Dimitri Renke - dimitri.renke@wfp2.com
// Created:		02.11.2016 - 10:42
// Last Modified	02.11.2016 - 10:42
// Copyright:	
using System;
namespace DataServicePrototype.loyalityExample
{
    public class DT_User_Example
    {
        public String PaybackNummber { get; }
        public DT_User_Example (String paybackNummber)
        {
            PaybackNummber = paybackNummber;
        }
    }
}
