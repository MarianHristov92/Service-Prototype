// ///-----------------------------------------------------------------
// ///   Class:          IConnectionEncryption
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 05.12.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
namespace LibDataService.ConnectionService
{
	public interface IConnectionEncryption
	{
		bool IsEncryptContent();
		string EncryptPayload(string toEncrypt);
		bool IsDecryptContent();
		string DecryptPayload(string toDecrypt);
	}
}
