// Real-App v5	UserManager
// File:		UserManager.cs
// Folder:		/Users/dimtrirenke/Documents/Repo/x_ServicePrototyp/DataServicePrototype/DataService/UserManager
// Author:		Dimitri Renke - dimitri.renke@wfp2.com
// Created:		07.10.2016 - 10:53
// Last Modified	07.10.2016 - 10:53
// Copyright:	
using System;
using LibDataService.DataManager;
using LibDataService.DataModels.Callback;
using LibDataService.DataModels.Container;
using LibDataService.DataModels.Description;

namespace DataServicePrototype.DataService
{
    public class UserManager : IDataManager
    {
        public UserManager ()
        {
        }

        public void getData ( IDataDescription dataDescriptionInstance, IDataCallback<IDataContainer> callback )
        {
            throw new NotImplementedException ();
        }

        public void getData ( IDataDescription dataDescriptionInstance, IDataContainer data, IDataCallback<IDataContainer> callback )
        {
            throw new NotImplementedException ();
        }

        public void setData ( IDataDescription dataDescriptionInstance, IDataContainer data )
        {
            throw new NotImplementedException ();
        }

        public void setData ( IDataDescription dataDescriptionInstance, IDataContainer data, IDataCallback<IDataContainer> callback )
        {
            throw new NotImplementedException ();
        }
    }
}
