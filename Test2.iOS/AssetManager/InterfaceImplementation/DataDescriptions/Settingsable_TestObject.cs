// ///-----------------------------------------------------------------
// ///   Class:          TSettingsable_Object
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 11.11.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using LibDataService.DataModels.Description;
using LibDataService.SettingsManager;

namespace Test.iOS
{
    public class Settingsable_TestObject
    {
		IDataDescription test_object;
        public Settingsable_TestObject (IDataDescription t_obj)
        {
			test_object = t_obj;
        }

		public bool IImpl_ISettingsData () 
		{
			return ( test_object is ISettingsData );
		}
    }
}
