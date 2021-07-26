
using System;
using LibDataService.DataModels.Description;
using LibDataService.DataModels.Description.Domain;
using NUnit.Framework;

namespace Test.iOS
{
    public class Domain_TestObject

    {
		IDataDescription test_object;

		public Domain_TestObject ( IDataDescription t_ob ) 
		{
			test_object = t_ob;
		}

        public bool IImpl_IDomainConstruction ()
        {
			return (test_object is IDomainConstruction);
        }

		public bool IImpl_IDomainConstructionField () 
		{
			return ( test_object is IDomainConstructionField );
		}
    }
}
