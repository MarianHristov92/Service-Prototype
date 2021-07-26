
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CachManager{
    /**
     * 
     */
    public interface ICachManager {

        /**
         * @param IDataDescription 
         * @param IDataContainer 
         * @param ICachCallback
         */
        public void getData(void IDataDescription, void IDataContainer, void ICachCallback);

        /**
         * @param IDataDescription 
         * @param IDataContainer
         */
        public void setData(void IDataDescription, void IDataContainer);

    }
}