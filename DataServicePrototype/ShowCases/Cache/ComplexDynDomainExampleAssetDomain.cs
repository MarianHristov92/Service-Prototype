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
    public class ComplexDynDomainExampleAssetDomain : DataCached.ExampleAssetDomain, IDomainConstructionField
    {
        public ComplexDynDomainExampleAssetDomain ()
        {
        }
        /**
         * REGION Domain Discription
         */
        #region
        EndpointMethods EndpointMethod = EndpointMethods.exam_dynConstm;


        public string getDomain ( IDataContainer parameters = null )
        {
            if ( parameters == null )
                return base.getDomain () + Separator + EndpointMethod;
            else
            {
                DTUserDataContainer datacontainer = ( parameters as DTUserDataContainer );
                String paybackNummer = datacontainer.getData ().PaybackNummber;
                return base.getDomain () + Separator + EndpointMethod + Separator + paybackNummer;
            }
        }
        #endregion

        /**
         * REGION Implementation ICacheData
         */
        #region
        public override double  getValidSpan ()
        {
            return 1000 * 60*60;
        }
        #endregion
    }
}
