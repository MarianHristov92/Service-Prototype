// Real-App v5	DataCached
// File:			DataCached.cs
// Folder:		/Users/dimtrirenke/Documents/Repo/x_ServicePrototyp/DataServicePrototype/ShowCases/Cache
// Author:		Dimitri Renke - dimitri.renke@wfp2.com
// Created:		27.10.2016 - 09:35
// Last Modified	27.10.2016 - 09:35
// Copyright:	
using System;
using Data.Description.Domain;
using Data.Description.Domain.Asset;
using LibDataService.CacheManager;

namespace DataServicePrototype.Cache
{
    public class DataCached
    {
        public class ExampleAssetDomain : AssetDomain, ICacheData
        {
            /**
             * REGION Domain Discription
             */
            #region
            public const Endpoints Endpoint = Endpoints.Example;

            public enum EndpointMethods
            {
                exam_string,
                exam_int,
                exam_json,
                exam_dynConstm
            }
            public override string getDomain ()
            {
                return base.getDomain () + RootDomain.Separator + Endpoint;
            }
            #endregion

            /**
             * REGION Implementation ICacheData
             */ 
            #region
            public virtual double getValidSpan ()
            {
                return 1000*15;
            }
            #endregion
        }
    }
}
