// Real-App v5	EmptyClass
// File:			EmptyClass.cs
// Folder:		/Users/dimtrirenke/Documents/Repo/x_ServicePrototyp/DataServicePrototype/ShowCases/Cache
// Author:		Dimitri Renke - dimitri.renke@wfp2.com
// Created:		27.10.2016 - 09:48
// Last Modified	27.10.2016 - 09:48
// Copyright:	
using System;
using Data.Description.Domain;
using DataServicePrototype.Asset;
using LibDataService.DataModels.Container;
using LibDataService.DataModels.Description.Domain;

namespace DataServicePrototype.Cache
{
    public class IntExampleAssetDomain : DataCached.ExampleAssetDomain
    {
        public IntExampleAssetDomain ()
        {
        }

        /**
         * REGION Domain Discription
         */
        #region
        EndpointMethods EndpointMethod = EndpointMethods.exam_int;

        public override string getDomain ()
        {
            return base.getDomain () + RootDomain.Separator + EndpointMethod;
        }
        #endregion
    }
}
