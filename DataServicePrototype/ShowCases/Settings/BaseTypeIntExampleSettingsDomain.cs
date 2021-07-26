// Real-App v5	BaseTypeInt16ExampleSettingsDomain
// File:			BaseTypeInt16ExampleSettingsDomain.cs
// Folder:		/Users/dimtrirenke/Documents/Repo/x_ServicePrototyp/DataServicePrototype/Data/Description/Domain/Settings/Example
// Author:		Dimitri Renke - dimitri.renke@wfp2.com
// Created:		06.10.2016 - 11:12
// Last Modified	06.10.2016 - 11:12
// Copyright:	
using System;
using Data.Description.Domain;
using Data.Description.Domain.Settings;
using LibDataService.DataModels.Callback;
using LibDataService.DataModels.Container;
using LibDataService.SettingsManager;

namespace Data.Description.Domain.Settings
{
    public class BaseTypeIntExampleSettingsDomain: ExampleSettingsDomain, ISettingsData    {
        public const EndpointMethods EndpointMethod = EndpointMethods.BaseTypeInt;
        public BaseTypeIntExampleSettingsDomain ()
        {
        }
        public override string getDomain ()
        {
            return base.getDomain () + RootDomain.Separator + EndpointMethod;
        }

        /**
         * REGION Implementation of ISettingsData 
         */
        #region
        public void getData ( string settingKey, ISettingsManager manager, IDataCallback<IDataContainer> settingCallback )
        {
            int readedValue = manager.getData<int> ( settingKey );
            SettingsDataInt cont = new SettingsDataInt( readedValue );
            settingCallback.onObtainData ( cont );

        }
        public void setData ( string settingKey, ISettingsManager manager, IDataContainer data )
        {
            int toWriteValue = ( data as SettingsDataInt ).Data;
            manager.setData<int> ( settingKey, toWriteValue );
        }
        #endregion
    }
}
