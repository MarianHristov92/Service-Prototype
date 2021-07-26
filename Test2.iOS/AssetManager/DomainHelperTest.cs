
using System;
using NUnit.Framework;
using LibDataService.SettingsManager;
using Data.Description.DomainAssociator;
using LibDataService.DataModels.Container;
using LibDataService.Helper;
using DataServicePrototype.Asset;

namespace Test.iOS
{
    [TestFixture]
    public class DomainHelperTest
    {
		[SetUp]
		public void RunBeforeAnyTests ()
		{
			//assetMan = new AssetManager ();
		}
        [Test]
        public void GetDomainAsKeyWithOutDyn()
        {
			String awaitingKey = "Assets.Example";
			String key=DomainHelper.getDomainAsKey ( DomainAssociator.Asset.Example.ComplexDynDomainExampleAssetDomain);
			Assert.AreEqual(awaitingKey,key);
        }

		[Test]
		public void GetDomainAsKeyWithDyn ()
		{
			String awaitingKey = "Assets.Example.exam_dynConstm.1170128393";
			String key = DomainHelper.getDomainAsKey( DomainAssociator.Asset.Example.ComplexDynDomainExampleAssetDomain, new DTUserDataContainer ( "1170128393" ));
			Assert.AreEqual ( awaitingKey, key );
		}
    }
}
