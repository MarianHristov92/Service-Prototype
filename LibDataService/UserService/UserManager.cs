// ///-----------------------------------------------------------------
// ///   Class:          ACRSettings
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 29.09.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using LibDataService.ConnectionService;
using LibDataService.DataModels.Callback;
using LibDataService.DataModels.Container;
using LibDataService.DataModels.Description;
using LibDataService.DataService;
using LibDataService.Exception;
using LibDataService.Tools;

namespace LibDataService.UserService
{
    public class UserManager : IUserManager
    {
        #region Events

        public event EventHandler<DataSuccessEventArgs<IDataContainer>> Success;
        public event EventHandler<DataFailEventArgs<IException>> Fail;

        #endregion

        #region Attributes

        private readonly IDataOperation _connectionManager;

        #endregion

        #region Constructor

        public UserManager()
        {
            this._connectionManager = new ConnectionManagerFlurl();
        }

        #endregion

        #region Public Methods

        public void RequestData(IDataDescription dataDescriptionInstance, IDataCallback callback)
        {
            ForwardLog("RequestData: " + dataDescriptionInstance.ID);
            _connectionManager.RequestData(dataDescriptionInstance, callback);
        }

        public void RequestData<TResult, TParameter>(IDataDescription dataDescriptionInstance, IDataCallback<TResult> callback)
            where TResult : class, IDataContainer
            where TParameter : class, IDataContainer
        {
            ForwardLog("RequestData: " + dataDescriptionInstance.ID);
            _connectionManager.RequestData<TResult, TParameter>(dataDescriptionInstance, callback);
        }

        public void RequestData(IDataDescription dataDescriptionInstance, IDataContainer parameter, IDataCallback callback)
        {
            ForwardLog("RequestData: " + dataDescriptionInstance.ID);
            _connectionManager.RequestData(dataDescriptionInstance, parameter, callback);
        }

        public void RequestData<TResult, TParameter>(IDataDescription dataDescriptionInstance, TParameter parameter, IDataCallback<TResult> callback)
            where TResult : class, IDataContainer
            where TParameter : class, IDataContainer
        {
                ForwardLog("RequestData: " + dataDescriptionInstance.ID);
            _connectionManager.RequestData(dataDescriptionInstance, parameter, callback);
        }

        public void SaveData(IDataDescription dataDescriptionInstance, IDataContainer parameter)
        {
            ForwardLog("SaveData: " + dataDescriptionInstance.ID);
            _connectionManager.SaveData(dataDescriptionInstance, parameter);
        }

        public void SaveData(IDataDescription dataDescriptionInstance, IDataContainer parameter, IDataCallback callback)
        {
            ForwardLog("SaveData: " + dataDescriptionInstance.ID);
            _connectionManager.SaveData(dataDescriptionInstance, parameter, callback);
        }

        #endregion

        #region Private Methods

        private void ForwardLog(string message)
        {
            Tools.Log.ForwardLog?.Invoke(this, new Tools.Log.ForwardEventArgs(this.GetType().Name, message));
        }

        #endregion
    }
}