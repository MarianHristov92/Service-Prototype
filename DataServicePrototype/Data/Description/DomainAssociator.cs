// ///-----------------------------------------------------------------
// ///   Class:          DomainDot
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 04.10.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using Data.Description.Domain.Settings;
using DataServicePrototype.Cache;
using LibDataService.DataModels.Description;

namespace Data.Description.DomainAssociator
{
    public class DomainAssociator
    {
        public DomainAssociator ()
        {
        }

        public static class Asset
        {
            public static class Example
            {
                public static IDataDescription StringExampleAssetDomain
                {
                    get
                    {
                        return new StringExampleAssetDomain ();
                    }
                }
                public static IDataDescription IntExampleAssetDomain
                {
                    get
                    {
                        return new IntExampleAssetDomain ();
                    }
                }
                public static IDataDescription JSONExampleAssetDomain
                {
                    get
                    {
                        return new JSONExampleAssetDomain ();
                    }
                }
                public static IDataDescription ComplexDynDomainExampleAssetDomain
                {
                    get
                    {
                        return new ComplexDynDomainExampleAssetDomain ();
                    }
                }
                
            }
        }

        public static class Setting
        {
            public static class Example
            {
                public static IDataDescription BasicStringDataType
                {
                    get
                    {
                        return new BaseTypeStringExampleSettingsDomain( );
                    }
                }
                public static IDataDescription BasicIntData
                {
                    get
                    {
                        return new BaseTypeIntExampleSettingsDomain( );
                    }
                }
                public static IDataDescription ComplexTypeData
                {
                    get
                    {
                        return new ComplexTypeExampleSettingsDomain();
                    }
                }

                public static IDataDescription MarktIDBasicStringData
                {
                    get
                    {
                        return new BaseTypeStringExampleSettingsDomain ();
                    }
                }

                public static IDataDescription BasicFloatData
                {
                    get
                    {
                        return new BaseTypeFloatExampleSettingsDomain ();
                    }
                }
                public static IDataDescription BasicStructData
                {
                    get
                    {
                        return new BaseTypeStructExampleSettingsDomain ();
                    }
                }

                public static IDataDescription BasicBoolData
                {
                    get
                    {
                        return new BaseTypeBooleanExampleSettingsDomain ();
                    }
                }
            }
        }

        public static class User
        {
        }
    }
}

