// ExceptionLogger.cs
// muc
// 21.12.2016 wfp:2 GmbH & Co. KG
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using LibDataService.Exception;

namespace LibDataService
{
	public sealed class ExceptionLogger
	{

		#region Attributes

		/// <summary>
		/// Static logger instance.
		/// </summary>
		static readonly Lazy<ExceptionLogger> _instance = new Lazy<ExceptionLogger>(() => new ExceptionLogger());

		#endregion

		#region Properties

		/// <summary>
		/// Returns the logger instance.
		/// </summary>
		/// <value>The logger instance.</value>
		public static ExceptionLogger Instance => _instance.Value;

		/// <summary>
		/// All logged exceptions in an observable collection
		/// </summary>
		static ObservableCollection<string> _warningExceptions = new ObservableCollection<string>();

		/// <summary>
		/// Get the list with Exceptions marked with criticality "Warning"
		/// </summary>
		/// <value>Returns exception list.</value>
		public static ObservableCollection<string> WarningExceptions => _warningExceptions;

		static ObservableCollection<string> _noFatalExceptions = new ObservableCollection<string>();

		/// <summary>
		/// Get the list with Exceptions marked with criticality "NoFatal"
		/// </summary>
		/// <value>Returns exception list.</value>
		public static ObservableCollection<string> NoFatalExceptions => _noFatalExceptions;

		static ObservableCollection<string> _fatalExceptions = new ObservableCollection<string>();

		/// <summary>
		/// Get the list with Exceptions marked with criticality "Fatal"
		/// </summary>
		/// <value>Returns exception list.</value>
		public static ObservableCollection<string> FatalExceptions => _fatalExceptions;

		static ObservableCollection<string> _unexpectedlyExceptions = new ObservableCollection<string>();

		/// <summary>
		/// Get the list with Exceptions marked with criticality "Unexpectedly"
		/// </summary>
		/// <value>Returns exception list.</value>
		public static ObservableCollection<string> UnexpectedlyExceptions => _unexpectedlyExceptions;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="T:LibDataService.ExceptionLogger"/> class.
		/// </summary>
		private ExceptionLogger() { }

		#endregion

		#region Public Methods

		/// <summary>
		/// Clears the exception lists content.
		/// </summary>
		public static void ClearExceptionLists()
		{
			_warningExceptions.Clear();
			_noFatalExceptions.Clear();
			_fatalExceptions.Clear();
			_unexpectedlyExceptions.Clear();
		}

		/// <summary>
		/// Logs the exception as formatted string.
		/// </summary>
		/// <returns>The exception as formatted string.</returns>
		/// <param name="e">E.</param>
		public static string CreateFormattedString(BaseException baseException)
		{
			string formattedException = FormatException(baseException);
			return formattedException;
		}

		/// <summary>
		/// Logs the exception to console.
		/// </summary>
		/// <param name="e">E.</param>
		public static void LogExceptionToConsole(BaseException baseException)
		{
			var formattedException = FormatException(baseException);
			Debug.WriteLine(formattedException);
		}

		#endregion


		#region Private Methods

		/// <summary>
		/// Formats the exception.
		/// </summary>
		/// <returns>The exception.</returns>
		/// <param name="e">E.</param>
		private static string FormatException(BaseException baseException)
		{
			StringBuilder mStringBuilder = new StringBuilder();
			CreateExceptionString(mStringBuilder, baseException, String.Empty);
			return mStringBuilder.ToString();
		}

		/// <summary>
		/// Creates the exception string.
		/// </summary>
		/// <param name="sb">Sb.</param>
		/// <param name="e">E.</param>
		/// <param name="indent">Indent.</param>
		/// TODO: Seperate the logic of this method.
		private static void CreateExceptionString(StringBuilder mStringBuilder, BaseException baseException, string indent)
		{
			//Set empty string to indent, if null
			if (indent == null) {
				indent = String.Empty;
			} else if (indent.Length > 0) {
				mStringBuilder.AppendFormat("{0}Inner ", indent);
			}

			mStringBuilder.AppendFormat("Exception:\n{0}Type: {1}", indent, baseException.GetType().FullName);
			mStringBuilder.AppendFormat("\n{0}Criticality: {1}", indent, baseException.Info.Criticality);
			mStringBuilder.AppendFormat("\n{0}Message: {1}", indent, baseException.Message);
			mStringBuilder.AppendFormat("\n{0}Source: {1}", indent, baseException.Source);
			mStringBuilder.AppendFormat("\n{0}Stacktrace: {1}", indent, baseException.StackTrace);

			//if innerException exists, then also add it to String Builder Stack
			if (baseException.InnerException != null) {
				mStringBuilder.Append("\n");
				//Add exception to ObservableCollection
				CreateExceptionString(mStringBuilder, (BaseException)baseException.InnerException, indent + "  ");
			}
			//Put exception log into correct observable collection
			switch (baseException.Criticality) {
				case ExceptionType.Warning:
					_warningExceptions.Add(mStringBuilder.ToString());
					break;
				case ExceptionType.NoFatal:
					_noFatalExceptions.Add(mStringBuilder.ToString());
					break;
				case ExceptionType.Fatal:
					_fatalExceptions.Add(mStringBuilder.ToString());
					break;
				case ExceptionType.Unexpectedly:
					_unexpectedlyExceptions.Add(mStringBuilder.ToString());
					break;
				default:
					break;
			}
		}

		#endregion
	}
}
