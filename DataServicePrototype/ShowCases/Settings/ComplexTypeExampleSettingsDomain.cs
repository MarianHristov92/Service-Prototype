// ///-----------------------------------------------------------------
// ///   Class:          asd
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 30.09.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using DataServicePrototype;
using LibDataService.DataModels.Callback;
using LibDataService.DataModels.Container;
using LibDataService.SettingsManager;

namespace Data.Description.Domain.Settings
{
    public class ComplexTypeExampleSettingsDomain : ExampleSettingsDomain , ISettingsData
    {
        public ComplexTypeExampleSettingsDomain ()
        {
        }
        /**
         * REGION Implementation of ExampleSettingsDomain
         */
        #region
        public const EndpointMethods EndpointMethod = EndpointMethods.ComplexType;

        public override string getDomain ()
        {
            return base.getDomain () + RootDomain.Separator + EndpointMethod;
        }
        #endregion

        /**
         * REGION Implementation of ISettingsComplexData interface
         */ 
        #region
        private const String SettingsSeparator = ".-.";
        public void getData ( string settingKey, ISettingsManager manager, IDataCallback<IDataContainer> settingCallback )
        {
            String loadedData = manager.getData<String> ( settingKey );
            ExmaplaSettingComplexType loaedInstance = FromString ( loadedData );
            settingCallback.onObtainData ( loaedInstance );
        }

        public void setData ( string settingKey, ISettingsManager manager, IDataContainer data )
        {
            manager.setData ( settingKey, ToString(data as ExmaplaSettingComplexType) );
        }

        private string ToString (ExmaplaSettingComplexType data)
        {
            return data.FirstParam + SettingsSeparator + data.SecoundParam;
        }

        private ExmaplaSettingComplexType FromString ( String parameterString )
        {
            ExmaplaSettingComplexType returnObject = new ExmaplaSettingComplexType();
            if ( parameterString == null || parameterString.Length == 0 )
                return null;
            int endOfFirstString = parameterString.IndexOf ( SettingsSeparator );

            String secoundAsString;
            if ( endOfFirstString != -1 )
            {
                String firstParam = parameterString.Substring ( 0, parameterString.IndexOf ( SettingsSeparator ) );
                secoundAsString = parameterString.Substring ( parameterString.IndexOf ( SettingsSeparator ) + SettingsSeparator.Length );
                int secoundParam = int.Parse ( secoundAsString );
                returnObject.FirstParam=firstParam;
                returnObject.SecoundParam = secoundParam;
            }
            return returnObject;
        }
        #endregion
    }
}

