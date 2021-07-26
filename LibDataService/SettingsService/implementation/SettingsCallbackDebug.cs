// ///-----------------------------------------------------------------
// ///   Class:          SettingsCallbackDebug
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 30.09.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using System.Diagnostics;
using LibDataService.DataModels.Callback;
using LibDataService.DataModels.Container;
using LibDataService.Exception;

namespace LibDataService.SettingsService
{
	/// <summary>
	/// This Class is basic implementation of ISettingsCallback for debuging.
	/// </summary>
	public class SettingsCallbackDebug<T> : IDataCallback<T>
	{

        public int ID { get; set; }
        public Type Source { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:LibDataService.SettingsManager.SettingsCallbackDebug`1"/> class.
		/// </summary>
		public SettingsCallbackDebug()
		{

		}

		/// <summary>
		/// This method will be called if Settings load operation was successful.
		/// Obtained data will be shown in console.
		/// </summary>
		/// <param name="obtainedData">Instance of generic type, contain loaded data</param>
		public void OnObtainData(T obtainedData)
		{
			Debug.WriteLine((DateTime.Now).ToLocalTime().ToString() + "  onObtainData " + obtainedData.ToString());
		}

		public void OnObtainData(IDataContainer data)
		{
			Debug.WriteLine((DateTime.Now).ToLocalTime().ToString() + "  onObtainData " + data.ToString());
		}

		public void OnObtainError()
		{
			Debug.WriteLine("SettingsCallbackDebug onObtainError ");
		}

		public void OnObtainError(IException exception)
		{
			Debug.WriteLine((DateTime.Now).ToLocalTime().ToString() + "  onObtainData " + exception.Reason);
		}

		/// <summary>
		/// This method will be called if Settings load operation was not successful.
		/// Obtained data will be shown in console.
		/// </summary>
		/// <param name="obtainedData">null or error description</param>
		public void OnObtainError(SettingsErrorTypes.SettingsErrorType erroType)
		{
			//switch ( erroType ) 
			//{
			//	case SettingsErrorTypes.SettingsErrorType.NotFound:
			//		break;
			//	case SettingsErrorTypes.SettingsErrorType.CastingError:
			//		break;
			//	case SettingsErrorTypes.SettingsErrorType.UnsupportedType:
			//		break;
			//}
			Debug.WriteLine("SettingsCallbackDebug onObtainError " + erroType.ToString());
		}
	}
}
