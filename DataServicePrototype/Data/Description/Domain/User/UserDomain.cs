
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Description.Domain.User{
    /**
     * 
     */
    public class UserDomain : RootDomain {

        /**
         * 
         */
        public UserDomain() {
        }

        /**
         * 
         */
        public enum Endpoints {
            Loyality,
            Payback,
            Rosso,
            Teaser,
            Shoppinglist
        }

    }
}