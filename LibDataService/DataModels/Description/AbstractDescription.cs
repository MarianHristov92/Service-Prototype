// // ///-----------------------------------------------------------------
// // ///   Class:          AbstractDescription.cs
// // ///   Description:    <Description>
// // ///   Author:         Dimitri Renke                     Date: 10.01.2018
// // ///   Company:        
// // ///   Notes:          <Notes>
// // ///   Revision History:
// // ///-----------------------------------------------------------------

using System;
using LibDataService.DataModels.Description.Domain;
using LibDataService.DataService;
using LibDataService.Exception;

namespace LibDataService.DataModels.Description
{
    public abstract class AbstractDescription : IDomainConstruction, IDataDescription
    {
        int _id;
        public int ID { 
            get { return _id; } 
            set {
                _id = value;
                System.Diagnostics.Debug.WriteLine($"PaybackTokenDescription ID: {_id}");
            } 
        }

        Type _source;
        public Type Source { 
            get { return _source; } 
            set {
                _source = value;
                System.Diagnostics.Debug.WriteLine($"PaybackTokenDescription Source: {_source}");
            } 
        }

        public abstract String GetDomain();
        public virtual void ReportError(object sender, EventArgs e)
        {
            var connectionArgs = (e as ExceptionEventArgs);
            ExceptionEventArgs temEventArgs = new ExceptionEventArgs(connectionArgs.Exception);
            DataManager.Instance.OnReportError(sender, connectionArgs);
        }
    }
}