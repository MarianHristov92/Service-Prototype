// ///-----------------------------------------------------------------
// ///   Class:          CacheDataContainer
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 07.10.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;

namespace LibDataService.CacheService
{
	public class CacheDataContainer<T> : ICacheDataContainer<T>
	{

		#region Attributes

		protected long _validUntilMilliSeconds = 25000;
		public long ValidUntilMilliSeconds {
			get { return _validUntilMilliSeconds; }
			set {
				_validUntilMilliSeconds = value;
			}
		}

		protected T _wrappedData;

		public T Data {
			get { return _wrappedData; }
			set {
				_wrappedData = value;
				_createdAt = DateTime.Now; //set creation time
				_validUntil = _createdAt.AddMilliseconds(_validUntilMilliSeconds); //set  Time at which the record becomes invalid.
			}
		}

		#endregion

		#region Properties

		protected DateTime _createdAt;
		public DateTime CreatedAt => _createdAt;

		protected DateTime _validUntil;
		public DateTime ValidUntil => _validUntil;

		public bool IsValid => _validUntil.CompareTo(DateTime.Now) > 0;

		#endregion

		#region Constructor

		public CacheDataContainer()
		{
			_createdAt = new DateTime();
			_validUntil = _createdAt;
		}

		#endregion

		#region Public Methods

		#endregion

	}
}
