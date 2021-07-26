// ///-----------------------------------------------------------------
// ///   Class:          SettingsShowCase
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 05.10.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using System.Diagnostics;
using Data.Description.DomainAssociator;
using LibDataService;
using LibDataService.DataModels.Callback;
using LibDataService.DataModels.Container;
using LibDataService.SettingsManager;


namespace DataServicePrototype
{
    public class SettingsShowCase
    {
        SettingsDataString myStringSetting = new SettingsDataString ();
        SettingsDataInt myIntSetting = new SettingsDataInt ();

        SettingsManager mySettingManager = new SettingsManager ();

        public SettingsShowCase ()
        {
        }
        public void saveSettings ( int toSaveInt = 25, String toSaveString = "EinStringDerGespreichrt werden soll" )
        {
            //Basic Datentypen
            mySettingManager.setData ( DomainAssociator.Setting.Example.BasicIntData, new SettingsDataInt(25) );
            mySettingManager.setData ( DomainAssociator.Setting.Example.BasicBoolData, new SettingsDataBool ( false ) );
            mySettingManager.setData ( DomainAssociator.Setting.Example.BasicFloatData, new SettingsDataFloat ( 2.2f) );
            mySettingManager.setData ( DomainAssociator.Setting.Example.BasicStringDataType, new SettingsDataString(toSaveString));

            //Eigene Datentypen
            ExmaplaSettingComplexType myComplexSetting = new ExmaplaSettingComplexType ();
            myComplexSetting.FirstParam = toSaveString;
            myComplexSetting.SecoundParam = toSaveInt;
            mySettingManager.setData ( DomainAssociator.Setting.Example.ComplexTypeData, myComplexSetting );
        }
        public void loadSettings ()
        {
            //Basic Datentypen
            mySettingManager.getData ( DomainAssociator.Setting.Example.BasicIntData, new SettingsCallbackDebug<IDataContainer> () );
            mySettingManager.getData ( DomainAssociator.Setting.Example.BasicIntData, new SettingsCallbackDebug<IDataContainer> () );
            mySettingManager.getData ( DomainAssociator.Setting.Example.BasicStringDataType, new SettingsCallbackDebug<IDataContainer> () );
            mySettingManager.getData ( DomainAssociator.Setting.Example.BasicFloatData, new SettingsCallbackDebug<IDataContainer> () );


            //ExmaplaSettingComplexType myComplexSetting = new ExmaplaSettingComplexType ();
            //Eigene Datentypen
            //mySettingManager.getData ( DomainAssociator.Setting.Example.ComplexTypeData, myComplexSetting, new SettingsCallbackDebug<ExmaplaSettingComplexType> () );
            mySettingManager.getData ( DomainAssociator.Setting.Example.ComplexTypeData, new SettingsCallbackDebug<IDataContainer> () );

        }
        public class SettingsCallbackD : IDataCallback<int>
        {
            public Type getDataType ()
            {
                throw new NotImplementedException ();
            }

            public void onObtainData ( int obtainedData )
            {
                Debug.WriteLine ( "myStringSetting.getData onObtainData " + obtainedData.ToString () );

            }

            public void onObtainError ( IException exception )
            {
                Debug.WriteLine ( "myStringSetting.getData onObtainError.Reason " + exception.getReason () );
                Debug.WriteLine ( "myStringSetting.getData onObtainError.Reason " + exception.getMessage () );
            }
        }
    }
}
