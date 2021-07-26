// ///-----------------------------------------------------------------
// ///   Class:          AssetException
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 09.11.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using LibDataService.Exception;

namespace LibDataService.AssetService
{
	public class AssetException : BaseException
	{
		public AssetException(IException ex) : base(ex) { }
		public AssetException(String message, System.Exception ex) : base(message, ex) { }
	}
}
