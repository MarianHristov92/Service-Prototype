// ///-----------------------------------------------------------------
// ///   Class:          IConnectionData
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 05.12.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System.Collections.Generic;
using System.Net.Http;
using Flurl.Http;
using LibDataService.DataModels.Callback;
using LibDataService.DataModels.Container;

namespace LibDataService.ConnectionService
{
	/// <summary>
	/// This interface represent required and optional 
	/// parameter for requesting Data.
	/// </summary>
	public interface IConnectionDescription<TResult, TParameter> : IConnectionDescription
		where TResult : class, IDataContainer
		where TParameter : class, IDataContainer
	{
		string GetParametrizedUrl(TParameter data);

		List<KeyValuePair<string, string>> GetPayload(TParameter data);
	}

	public interface IConnectionDescription {
		RequestType RequestType { get; }

		string GetParametrizedUrl(IDataContainer data);

		List<KeyValuePair<string, string>> GetPayload(IDataContainer data);

		void AnalyseResponse(HttpResponseMessage response, IDataCallback callback);
		void AnalyseResponse(HttpCall request, IDataCallback callback);

		string BaseUrl { get; }

		bool HasParametrizedUrl { get; }

		bool HasPayload { get; }
	}

}
