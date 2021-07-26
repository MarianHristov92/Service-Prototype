// Real-App v5	BaseTypeFloatExampleSettingsDomain
// File:			BaseTypeFloatExampleSettingsDomain.cs
// Folder:		/Users/dimtrirenke/Documents/Repo/x_ServicePrototyp/DataServicePrototype/Data/Description/Domain/Settings/Example
// Author:		Dimitri Renke - dimitri.renke@wfp2.com
// Created:		06.10.2016 - 11:06
// Last Modified	06.10.2016 - 11:06
// Copyright:	
using System;
using Data.Description.Domain;
using Data.Description.Domain.Settings;
using LibDataService.DataModels.Callback;
using LibDataService.DataModels.Container;
using LibDataService.SettingsManager;

namespace Data.Description.Domain.Settings
{
    public class BaseTypeFloatExampleSettingsDomain : ExampleSettingsDomain,ISettingsData
    {
        public const EndpointMethods EndpointMethod = EndpointMethods.BaseTypeFloat;
        public BaseTypeFloatExampleSettingsDomain ()
        {
        }



        public float getDefaultValue ()
        {
            float  defaultValue=25.5f;
            return defaultValue;
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
            float readedValue = manager.getData<float> ( settingKey );
            SettingsDataFloat cont = new SettingsDataFloat ( readedValue );
            settingCallback.onObtainData ( cont );

        }
        public void setData ( string settingKey, ISettingsManager manager, IDataContainer data )
        {
            float toWriteValue = ( data as SettingsDataFloat ).Data;
            manager.setData<float> ( settingKey,toWriteValue );
        }
        #endregion
    }

}
