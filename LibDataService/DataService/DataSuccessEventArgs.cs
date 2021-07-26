// // ///-----------------------------------------------------------------
// // ///   Class:          DataSuccessEventArgs.cs
// // ///   Description:    <Description>
// // ///   Author:         ahc                     Date: 21.11.2017
// // ///   Company:        
// // ///   Notes:          <Notes>
// // ///   Revision History:
// // ///-----------------------------------------------------------------
using System;
namespace LibDataService.DataService
{
	public class DataSuccessEventArgs<TType> : EventArgs where TType : class 
	{
        protected int _id;
        public int ID => _id;

        protected Type _source;
        public Type Source => _source;

		protected TType _data;
        public TType Data => _data;

        public DataSuccessEventArgs(TType result, int id = -1, Type source = null)
		{
			_data = result;
            _id = id;
            _source = source;
		}

	}
}
