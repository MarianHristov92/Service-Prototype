// ///-----------------------------------------------------------------
// ///   Class:          IConnectionAuth
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 05.12.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using LibDataService.DataModels.Container;

namespace LibDataService.ConnectionService
{
    /// <summary>
    /// Listing of intergrated Authentification Types 
    /// </summary>
    public enum AuthentificationType 
    {
        BasicAuthentification,
        BearerTokenAuthentification,
        None
    }
}
