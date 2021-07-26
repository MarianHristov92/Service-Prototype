// Real-App v5	ComplexDynDomainExampleAssetDomain
// File:			ComplexDynDomainExampleAssetDomain.cs
// Folder:		/Users/dimtrirenke/Documents/Repo/x_ServicePrototyp/DataServicePrototype/ShowCases/Asset
// Author:		Dimitri Renke - dimitri.renke@wfp2.com
// Created:		02.11.2016 - 10:35
// Last Modified	02.11.2016 - 10:35
// Copyright:	
using System;
using DataServicePrototype.loyalityExample;
using LibDataService.DataModels.Container;

namespace DataServicePrototype.Asset
{
    public class DTUserStatusDataContainer: IDataContainer
    {
        RootObject Data;

        public DTUserStatusDataContainer () 
        { 
        }
        public DTUserStatusDataContainer ( RootObject dt_user_status )
        {
            Data = dt_user_status;

        }

        public RootObject getData () 
        {
            return Data;
        }

        public Type getDataType ()
        {
            return typeof ( RootObject );
        }

        public bool isNullValue ()
        {
            return Data == null ? true : false;
        }
    }
}
