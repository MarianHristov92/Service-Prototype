// ///-----------------------------------------------------------------
// ///   Class:          IDataSource
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 23.11.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
namespace LibDataService.DataModels.Container
{
    public interface IDataSource<OriginSource>
    {
        T getData<T> ( String Tag );
        Boolean hasData<T> ( String Tag );
        void setData<T> ( String Tag, T data );
        OriginSource getSource ();
        void setSource (OriginSource source);
    }
}
