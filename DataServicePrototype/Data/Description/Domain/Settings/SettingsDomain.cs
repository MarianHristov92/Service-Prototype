
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Description.Domain.Settings{
    /**
     * 
     */
    public class SettingsDomain : RootDomain {

        /**
         * 
         */
        public SettingsDomain() {
        }
        /**
         * 
         */
        public const DataTypes DataType = DataTypes.Settings;

        /**
         * 
         */
        public enum Endpoints
        {
            Example,
        }
        public override String getDomain ()
        {
            return DataType.ToString();
        }

    }
}