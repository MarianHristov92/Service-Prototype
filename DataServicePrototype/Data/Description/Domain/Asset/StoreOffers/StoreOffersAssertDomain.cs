
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Description.Domain.Asset.StoreOffers{
    /**
     * 
     */
    public class StoreOffersAssertDomain : AssetDomain {

        /**
         * 
         */
        public StoreOffersAssertDomain() {
        }

		public const Endpoints Endpoint = Endpoints.StoreOffers;

        /**
         * 
         */
        public enum EndpointMethods {
            Categories,
            Category,
            Product
        }

    }
}