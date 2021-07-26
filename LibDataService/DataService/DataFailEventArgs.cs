// // ///-----------------------------------------------------------------
// // ///   Class:          DataSuccessEventArgs.cs
// // ///   Description:    <Description>
// // ///   Author:         ahc                     Date: 21.11.2017
// // ///   Company:        
// // ///   Notes:          <Notes>
// // ///   Revision History:
// // ///-----------------------------------------------------------------
using System;
namespace LibDataService.Exception
{
	public class DataFailEventArgs<TType> : EventArgs where TType : IException
	{
		protected int _id;
		public int ID => _id;

        protected Type _source;
        public Type Source  => _source;

		protected TType _error;
		public TType Error => _error;

		public DataFailEventArgs(TType error, int id = -1, Type source = null)
		{
			_error = error;
            _id = id;
            _source = source;
		}

	}
}
