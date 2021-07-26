// ///-----------------------------------------------------------------
// ///   Class:          ExampleSettingsDomain
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 30.09.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;


namespace Data.Description.Domain.Settings
{
    public class ExampleSettingsDomain : SettingsDomain
    {
        public const Endpoints Endpoint = Endpoints.Example;
        public ExampleSettingsDomain ()
        { }
           

        public enum EndpointMethods
        {
            BaseTypeString,
            BaseTypeInt,
            BaseTypeBool,
            BaseTypeFloat,
            BaseTypeStruct,
            ComplexType
        }
        public override String getDomain ()
        {
            return base.getDomain () + RootDomain.Separator + Endpoint;
        }
    }
}
