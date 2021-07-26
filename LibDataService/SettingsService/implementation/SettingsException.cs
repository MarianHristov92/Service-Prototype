// ///-----------------------------------------------------------------
// ///   Class:          SettingsException
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 18.11.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using LibDataService.Exception;

namespace LibDataService
{
	public class SettingsException : BaseException
	{
		public SettingsException()
		{
		}

		public enum SettingsExceptionType
		{
			DataNotFound = 1,
			UnsupportedType = 2,
			CastingError = 3,
			Unexpectedly = 4
		};

		/// <summary>
		/// Return Exception type
		///	DataNotFound = 1,
		///	UnsupportedType = 2,
		///	CastingError = 3,
		///	Unexpectedly = 4
		/// </summary>
		/// <value>The type of the cached.</value>
		public SettingsExceptionType SettingsType { get; }
		public static SettingsException ExceptionUnsupportedType()
		{
			SettingsException exeption = new SettingsException(SettingsExceptionType.UnsupportedType);
			exeption.Info.Reason = SettingsExceptionType.UnsupportedType.ToString();

			return exeption;
		}

		public static SettingsException ExceptionDataNotFound()
		{
			SettingsException exeption = new SettingsException(SettingsExceptionType.DataNotFound);
			exeption.Info.Reason = SettingsExceptionType.DataNotFound.ToString();
			return exeption;
		}

		public static SettingsException ExceptionUnexpectedlye(System.Exception ex)
		{
			SettingsException exeption = new SettingsException(ex, SettingsExceptionType.Unexpectedly);
			exeption.Info.Reason = SettingsExceptionType.Unexpectedly.ToString();
			return exeption;
		}

		public static SettingsException ExceptionCastingError()
		{
			SettingsException exeption = new SettingsException(SettingsExceptionType.CastingError);
			exeption.Info.Reason = SettingsExceptionType.CastingError.ToString();
			return exeption;
		}

		private SettingsException(IException exceptionbefor) : base(exceptionbefor)
		{
		}

		private SettingsException(System.Exception exceptionbefor, SettingsExceptionType settingExceptionType) : base(settingExceptionType.ToString(), exceptionbefor)
		{
			SettingsType = settingExceptionType;
		}

		private SettingsException(SettingsExceptionType settingExceptionType)
		{
			SettingsType = settingExceptionType;
		}
	}
}
