// TestsExceptionLogger.cs
// muc
// 22.12.2016 wfp:2 GmbH & Co. KG
using System;
using NUnit.Framework;
using NUnit.Framework.Internal;
using LibDataService;

namespace Test.iOS
{
	[TestFixture]
    public class TestsExceptionLogger
    {
        public TestsExceptionLogger ()
        {
			ExceptionLogger.ClearExceptionLists ();
        }


		[Test]
		public void Throw_Warning_Exception () { 
		    try
            {
                var warningException= new BaseException ( "You have been warned!" );
				warningException.Info.Criticality = BaseException.ExceptionType.Warning;
				throw warningException;
			}
			catch ( BaseException e )
			{
				ExceptionLogger.LogExceptionToConsole ( e );
				Assert.IsTrue ( !String.IsNullOrEmpty ( ExceptionLogger.LogExceptionAsFormattedString ( e ) ) );
			}
		}

		[Test]
		public void Validate_ExceptionList_Warning_Count () {
			Assert.IsTrue ( ExceptionLogger.GetException_Warnings.Count > 0 );
		}

		[Test]
		public void Throw_NoFatal_Exception ()
		{
			try
			{
				var noFatalException = new BaseException ( "No Fatal exception thrown!" );
				noFatalException.Info.Criticality = BaseException.ExceptionType.NoFatal;
				throw noFatalException;
			}
			catch ( BaseException e )
			{
				ExceptionLogger.LogExceptionToConsole ( e );
				Assert.IsTrue ( !String.IsNullOrEmpty ( ExceptionLogger.LogExceptionAsFormattedString ( e ) ) );
			}
	
		}

		[Test]
		public void Validate_ExceptionList_NoFatal_Count (){
			Assert.IsTrue ( ExceptionLogger.GetException_NoFatal.Count > 0 );
		}

		[Test]
		public void Throw_Fatal_Exception ()
		{
			try
			{
				var fatalException = new BaseException ( "Fatal exception thrown!" );
				fatalException.Info.Criticality = BaseException.ExceptionType.Fatal;
				throw fatalException;
			}
			catch ( BaseException e )
			{
				ExceptionLogger.LogExceptionToConsole ( e );
				Assert.IsTrue ( !String.IsNullOrEmpty( ExceptionLogger.LogExceptionAsFormattedString(e) ) );
			}

		}

		[Test]
		public void Validate_ExceptionList_Fatal_Count (){
			Assert.IsTrue ( ExceptionLogger.GetException_Fatal.Count > 0);
		}

		[Test]
		public void Throw_Unexpectedly_Exception ()
		{
			try
			{
				var unexpectedlyException = new BaseException ( "You have been warned!" );
				unexpectedlyException.Info.Criticality = BaseException.ExceptionType.Unexpectedly;
				throw unexpectedlyException;
			}
			catch ( BaseException e )
			{
				ExceptionLogger.LogExceptionToConsole ( e );
				Assert.IsTrue ( !String.IsNullOrEmpty ( ExceptionLogger.LogExceptionAsFormattedString ( e ) ) );
			}

		}

		[Test]
		public void Validate_ExceptionList_Unexpectedly_Count (){
			Assert.IsTrue ( ExceptionLogger.GetException_Unexpectedly.Count > 0 );
		}

		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the
		/// <see cref="T:Test.iOS.TestsExceptionLogger"/> is reclaimed by garbage collection.
		/// And clear the list of exceptions in ExceptionLogger class.
		/// </summary>
		~TestsExceptionLogger () {
			ExceptionLogger.ClearExceptionLists ();
			GC.Collect ();
		}


    }
}
