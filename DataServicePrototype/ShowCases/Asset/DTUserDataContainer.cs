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
    public class DTUserDataContainer: IDataContainer
    {
        DT_User_Example Data;
        
        public DTUserDataContainer (String paybackNummer)
        {
            Data = new DT_User_Example ( paybackNummer );
            
        }
        public DTUserDataContainer ( DT_User_Example dt_user )
        {
            Data = dt_user;

        }

        public DT_User_Example getData () 
        {
            return Data;
        }

        public Type getDataType ()
        {
            return typeof ( DT_User_Example );
        }

        public bool isNullValue ()
        {
            return Data == null ? true : false;
        }
    }
}
