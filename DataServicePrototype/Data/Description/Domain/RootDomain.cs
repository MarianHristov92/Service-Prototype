
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibDataService.DataModels.Description;
using LibDataService.DataModels.Description.Domain;

namespace Data.Description.Domain{
    /**
     * 
     */
    public abstract class RootDomain : IDomainConstruction, IDataDescription {

        
		public static String Separator = ".";

        public virtual string getDomain ()
        {
            throw new NotImplementedException ();
        }

        public enum DataTypes
		{
			User = 1,
			Assets = 2,
			Settings = 3
		}
		/**
		 * 
		 */

    }
}