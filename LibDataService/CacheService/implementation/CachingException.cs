// ///-----------------------------------------------------------------
// ///   Class:          CachingException
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 07.11.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using System.Collections.Generic;
using LibDataService.Exception;

namespace LibDataService.CacheService
{
	public class CachingException : BaseException
    {
		public enum CachedExceptionType
		{
			DataNotFound=1,
			DataInvalid=2,
			CantCacheNullValue=3,
			Unexpectedly=4
		};
		/// <summary>
		/// Return Exception type
		/// 	DataNotFound=1,
		///		DataInvalid=2,
		///		CantCacheNullValue=3,
		///		Unexpectedly=4
		/// </summary>
		/// <value>The type of the cached.</value>
		public CachedExceptionType CachedType { get;}
        public static CachingException ExceptionDataInvalid()
        {
			CachingException exeption = new CachingException ( CachedExceptionType.DataInvalid);
			exeption.Info.Reason = CachedExceptionType.DataInvalid.ToString ();
			exeption.Info.Criticality = ExceptionType.NoFatal;
			return exeption;
        }
		public static CachingException ExceptionDataNotFound ()
		{
			CachingException exeption = new CachingException  (CachedExceptionType.DataNotFound);
			exeption.Info.Reason = CachedExceptionType.DataNotFound.ToString ();
			exeption.Info.Criticality = ExceptionType.NoFatal;
			return exeption;
		}
		public static CachingException ExceptionCantCacheNullValue ()
		{
			CachingException exeption = new CachingException (CachedExceptionType.CantCacheNullValue);
			exeption.Info.Reason = CachedExceptionType.CantCacheNullValue.ToString ();
			exeption.Info.Criticality = ExceptionType.NoFatal;
			return exeption;
		}
		public static CachingException ExceptionUnexpectedlye (System.Exception ex)
		{
			CachingException exeption = new CachingException ( ex,CachedExceptionType.Unexpectedly );
			exeption.Info.Reason = CachedExceptionType.Unexpectedly.ToString ();
			return exeption;
		}
		private CachingException (IException exceptionbefor) :base(exceptionbefor)
		{
		}
		private CachingException ( System.Exception exceptionbefor, CachedExceptionType cachedExceptionType) : base (cachedExceptionType.ToString(), exceptionbefor )
		{
			CachedType = cachedExceptionType;
		}
		private CachingException (CachedExceptionType cachedExceptionType) : base ( )
		{
			CachedType = cachedExceptionType;
		}

    }
}
