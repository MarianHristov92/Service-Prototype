// ///-----------------------------------------------------------------
// ///   Class:          DevelopmentModeDectector
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 04.11.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
namespace LibDataService.Helper
{
    public static class CompilingModeDectector
    {
		/// <summary>
		/// transfer compile conditional "#if DEBUG" to code. 
		/// </summary>
		/// <returns><c>true</c>, if debug compiling was ised, <c>false</c> otherwise.</returns>
		public static bool IsDebugCompiling() 
		{ 
			Boolean isDebugMode = false;
			#if DEBUG
			isDebugMode = true;
			#endif
			return isDebugMode;
		}
    }
}
