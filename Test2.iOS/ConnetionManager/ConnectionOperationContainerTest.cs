// ///-----------------------------------------------------------------
// ///   Class:          asd
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 19.12.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using NUnit.Framework;
using LibDataService.SettingsManager;
using Data.Description.DomainAssociator;
using LibDataService.DataModels.Container;
using LibDataService.Helper;
using DataServicePrototype.Asset;
using LibDataService.ConnectionManager;
using Flurl;
using Flurl.Http;
using LibDataService.DataModels.Callback;
using System.Collections.Generic;
using System.Net.Http;
using LibDataService;
using LibDataService.DataModels.Description;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace Test.iOS
{
	[TestFixture]
	public class ConnectionOperationContainerTest
	{
		TestConnectionDescription testDescription;
		String TestURl = "https://www.real.de";
		List<KeyValuePair<string, string>> payload;
		[SetUp]
		public void RunBeforeAnyTests ()
		{
			payload = new List<KeyValuePair<string, string>> ();
			KeyValuePair<string, string> itemA = new KeyValuePair<string, string>( "testname", "testvalue" );
			payload.Add ( itemA );
		}
		/// <summary>
		/// Test 
		/// </summary>
		[Test]
		public void ClientSetURL ()
		{
			
			testDescription = new TestConnectionDescription ( TestURl, RequestType.POST,payload );
			ConnectionOperationContainerTestable opContainer = new ConnectionOperationContainerTestable ();
			opContainer.DataDescription = testDescription;
			FlurlClient client = opContainer.getPrepairRequest ();
			Assert.AreEqual ( client.Url.ToString(), TestURl );
		}
		/// <summary>
		/// Test would payload set to Request
		/// </summary>
		[Test]
		public void ClientSetPayload ()
		{
			DataDelegateCallback<IDataContainer> delegateCallback = new DataDelegateCallback<IDataContainer> (
					( Action<IDataContainer> ) ( ( data ) => { Assert.Fail (); } ),
				( Action<IException> ) ( ( ex ) =>
				{
					if ( !( ex is ConnectionException ) )
						Assert.Fail ();
					else
					{
						HttpCall call = ( ex as ConnectionException ).HttpCall;
						HttpContent payloadPre = new FormUrlEncodedContent ( payload );
						HttpContent b = call.Request.Content;
						Assert.Pass ();
						return;
					//Assert.AreEqual ( payloadPre, call.Request.Content );
					}
				} ) );
				testDescription = new TestConnectionDescription ( TestURl, RequestType.POST, payload );
				ConnectionOperationContainerTestable opContainer = new ConnectionOperationContainerTestable ();
				opContainer.DataDescription = testDescription;
				opContainer.Callback = delegateCallback;	
				opContainer.performeRequest ();
		}
		[Test]
		public void ClientSetParametrizedURL ()
		{
			testDescription = new TestConnectionDescription ( TestURl, RequestType.POST, payload );
			ConnectionOperationContainerTestable opContainer = new ConnectionOperationContainerTestable ();
			opContainer.DataDescription = testDescription;
			testDescription.parametrizesURL = "http://sondernurl.de?blub=milch";
			FlurlClient client = opContainer.getPrepairRequest ();
			Assert.AreEqual ( client.Url.ToString (), testDescription.parametrizesURL );
		}
		[Test]
		public void ClientAutTypeNone ()
		{
			DataDelegateCallback<IDataContainer> delegateCallback = new DataDelegateCallback<IDataContainer> (
					( Action<IDataContainer> ) ( ( data ) => { Assert.Fail (); } ),
				( Action<IException> ) ( ( ex ) =>
				{
					if ( !( ex is ConnectionException ) )
						Assert.Fail ();
					else
					{
						HttpCall call = ( ex as ConnectionException ).HttpCall;
						HttpRequestHeaders ad = call.Request.Headers;
						Log.i(ad.Authorization.Parameter);
						Assert.Fail();
					}
				} ) );
			TestAutConnDes testAutDescription = new TestAutConnDes ( TestURl, RequestType.POST, payload );
			testAutDescription.type = AuthentificationType.None;
			ConnectionOperationContainerTestable opContainer = new ConnectionOperationContainerTestable ();
			opContainer.DataDescription = testAutDescription;
			opContainer.Callback = delegateCallback;
			opContainer.performeRequest ();
		}
	}
	class ConnectionOperationContainerTestable : ConnectionOperationContainer
	{
		public FlurlClient getPrepairRequest () {
			prepairRequest ();
			return ( FlurlClient ) Client;
		}
	}
	public class TestConnectionDescription : IConnectionDescription, IDataDescription
	{
		List<KeyValuePair<string, string>> testPayload { get; set; }
		List<KeyValuePair<string, string>> testHeaders { get; set; }
		List<KeyValuePair<string, string>> testCookes { get; set; }
		String testURL;
		RequestType testRequestType;
		public String parametrizesURL { get; set;}

		public TestConnectionDescription (String url,RequestType rType,List<KeyValuePair<string, string>> payload = null ) 
		{
			testPayload = payload;
			testURL = url;
			testRequestType = rType;
			
		}
		public void analyseResponse ( HttpCall request, IDataCallback<IDataContainer> callback )
		{
			if ( callback != null )
				callback.onObtainError ( new ConnectionException ( request ) );
		}

		public void analyseResponse ( HttpResponseMessage response, IDataCallback<IDataContainer> callback )
		{
			throw new NotImplementedException ();
		}

		public string getBaseUrl ()
		{
			return testURL;
		}

		public string getHeaderParameter ( IDataContainer data )
		{
			throw new NotImplementedException ();
		}

		public string getParameterizedURL ( IDataContainer data )
		{
			return parametrizesURL;
		}

		public List<KeyValuePair<string, string>> getPayload ( IDataContainer data )
		{
			return testPayload;
		}

		public RequestType getReqeustType ()
		{
			return testRequestType;
		}

		public bool hasHeaderParameter ()
		{
			return false;
		}

		public bool hasParameterizedURL ()
		{
			return parametrizesURL == null ? false :true;
		}

		public bool hasPayload ()
		{
			return true;
		}
	}
	public class TestAutConnDes : TestConnectionDescription, IConnectionAuth
	{
		public String Token { get; set; }
		public BasicAuthentification basicAut { get; set; }
		public AuthentificationType type {get;set;}

		public TestAutConnDes ( String url, RequestType rType, List<KeyValuePair<string, string>> payload = null ):base(url,rType,payload)
		{
		}
		public AuthentificationType getAuthentificationType ()
		{
			return type;
		}

		public BasicAuthentification getBasicAuthentification ( IDataContainer data )
		{
			return basicAut;
		}

		public string getBearerToken ( IDataContainer data )
		{
			return Token;
		}

		public bool hasAuthentification ()
		{
			return type == AuthentificationType.None ? false : true;
		}
	}
}
