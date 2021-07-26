// Real-App v5	IUser
// File:			IUser.cs
// Folder:		/Users/dimtrirenke/Documents/Repo/x_ServicePrototyp/DataServicePrototype/DataService/UserManager
// Author:		Dimitri Renke - dimitri.renke@wfp2.com
// Created:		07.10.2016 - 10:54
// Last Modified	07.10.2016 - 10:54
// Copyright:	
using System;
namespace DataServicePrototype.DataService
{
    public interface IUser
    {
        bool isDirty ();
        bool isKnown ();
        void initianlize ();
    }
}
