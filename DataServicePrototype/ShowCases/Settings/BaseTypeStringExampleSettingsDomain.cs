// ///-----------------------------------------------------------------
// ///   Class:          BaseTypeStringExampleSettingsDomain
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 30.09.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using LibDataService.DataModels.Callback;
using LibDataService.DataModels.Container;
using LibDataService.SettingsManager;

namespace Data.Description.Domain.Settings
{
    public class BaseTypeStringExampleSettingsDomain : ExampleSettingsDomain, ISettingsData
    {
        public const EndpointMethods EndpointMethod = EndpointMethods.BaseTypeString;

        public BaseTypeStringExampleSettingsDomain ()
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
            String readedValue = manager.getData<String> ( settingKey );
            SettingsDataString cont = new SettingsDataString ( readedValue );
            settingCallback.onObtainData ( cont );

        }
        public void setData ( string settingKey, ISettingsManager manager, IDataContainer data )
        {
            String toWriteValue = ( data as SettingsDataString ).Data;
            manager.setData<String> ( settingKey, toWriteValue );
        }
        #endregion

    }
}
