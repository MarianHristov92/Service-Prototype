// ///-----------------------------------------------------------------
// ///   Class:          EmptyClass
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 30.09.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using LibDataService.DataModels.Container;
using LibDataService.SettingsManager;

namespace DataServicePrototype
{
    public class ExmaplaSettingComplexType : IDataContainer
    {
        public String FirstParam { get; set; }
        public int SecoundParam { get; set; }

        public ExmaplaSettingComplexType ()
        {
        }
        public override string ToString ()
        {
            return string.Format ( "[ExmaplaSettingComplexType: FirstParam={0}, SecoundParam={1}]", FirstParam, SecoundParam );
        }

        public Type getDataType ()
        {
            return typeof(ExmaplaSettingComplexType);
        }

        public void setDataType ( Type dataType )
        {
            throw new NotImplementedException ();
        }

        public bool isNullValue ()
        {
            if ( FirstParam == null )
                return true;
                    return false;
        }
    }
}
