// ///-----------------------------------------------------------------
// ///   Class:          IException
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 04.11.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace LibDataService.Exception
{
	public interface IException
    {
		String Reason { get; }
		String Message { get; }
		String ReasonCode { get; }
		ExceptionType Criticality { get; }
		IException OriginException { get; }
		List<IException> ExceptionList { get; }
		JObject ToJson ();

	}
}
