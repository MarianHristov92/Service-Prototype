// Real-App v5	BaseTypeFloat,
// File:			BaseTypeFloat,.cs
// Folder:		/Users/dimtrirenke/Documents/Repo/x_ServicePrototyp/DataServicePrototype/Data/Description/Domain/Settings/Example
// Author:		Dimitri Renke - dimitri.renke@wfp2.com
// Created:		06.10.2016 - 11:05
// Last Modified	06.10.2016 - 11:05
// Copyright:	
using System;
using Data.Description.Domain;
using Data.Description.Domain.Settings;
namespace Data.Description.Domain.Settings
{
    public class BaseTypeStructExampleSettingsDomain : ExampleSettingsDomain
    {
        public const EndpointMethods EndpointMethod = EndpointMethods.BaseTypeStruct;
        public BaseTypeStructExampleSettingsDomain ()
        {
        }
        public override string getDomain ()
        {
            return base.getDomain () + RootDomain.Separator + EndpointMethod;
        }
    }
}
