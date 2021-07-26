
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Description.Domain;


namespace Data.Description.Domain.Asset{
    /**
     * 
     */
    public class AssetDomain : RootDomain {

        /**
         * 
         */
        public AssetDomain() {
        }

		/**
         * 
         */
        public const DataTypes DataType = DataTypes.Assets;

        /**
         * 
         */
        public enum Endpoints {
            StoreOffers,
            Catalogue,
            Loyality,
            Support,
            Teaser,
            StoreInfo,
            Search,
            Recipe,
            Ownbrand,
            Example
        }
        public override string getDomain ()
        {
            return DataType.ToString ();
        }

    }
}