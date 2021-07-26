// Real-App v5	BaseTypeBooleanExampleSettingsDomain
// File:			BaseTypeBooleanExampleSettingsDomain.cs
// Folder:		/Users/dimtrirenke/Documents/Repo/x_ServicePrototyp/DataServicePrototype/Data/Description/Domain/Settings/Example
// Author:		Dimitri Renke - dimitri.renke@wfp2.com
// Created:		06.10.2016 - 11:03
// Last Modified	06.10.2016 - 11:03
// Copyright:	
using System;
using Data.Description.Domain;
using Data.Description.Domain.Settings;
using LibDataService.DataModels.Callback;
using LibDataService.DataModels.Container;
using LibDataService.SettingsManager;

namespace Data.Description.Domain.Settings
{
    public class BaseTypeBooleanExampleSettingsDomain : ExampleSettingsDomain, ISettingsData
    {
        public const EndpointMethods EndpointMethod = EndpointMethods.BaseTypeBool;
        public BaseTypeBooleanExampleSettingsDomain ()
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
            bool readedValue = manager.getData<bool> ( settingKey );
            SettingsDataBool cont = new SettingsDataBool ( readedValue );
            settingCallback.onObtainData ( cont );

        }
        public void setData ( string settingKey, ISettingsManager manager, IDataContainer data )
        {
            bool toWriteValue = ( data as SettingsDataBool ).Data;
            manager.setData<bool> ( settingKey, toWriteValue );
        }
        #endregion
    }
}
