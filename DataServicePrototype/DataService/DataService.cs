// ///-----------------------------------------------------------------
// ///   Class:          DataService
// ///   Description:    <Description>
// ///   Author:         Dimitri Renke                    Date: 06.10.2016
// ///   Notes:          <Notes>
// ///   Revision History:
// ///   Name:           Date:        Description:
// ///-----------------------------------------------------------------
using System;
using LibDataService;
using LibDataService.DataManager;
using LibDataService.DataModels.Callback;
using LibDataService.DataModels.Container;
using LibDataService.DataModels.Description;
using LibDataService.DataService;
namespace DataServicePrototype.DataService
{
    public class DataService : LibDataService.DataService.DataService
    {
        IDataService UserManager;
        public DataService ()
        {
        }
        /**
         * REGION Overwriting implementation  of IDataService
         */
        #region
        public override void getData ( IDataDescription dataDescriptionInstance, IDataCallback<IDataContainer> callback )
        {
            base.getData( dataDescriptionInstance, callback );
        }

        public override void getData ( IDataDescription dataDescriptionInstance, IDataContainer data, IDataCallback<IDataContainer> callback )
        {
            base.getData( dataDescriptionInstance, data, callback );
        }

        public override void setData ( IDataDescription dataDescriptionInstance, IDataContainer data )
        {
            base.setData ( dataDescriptionInstance, data );
        }

        public override void setData ( IDataDescription dataDescriptionInstance, IDataContainer data, IDataCallback<IDataContainer> callback )
        {
            base.setData ( dataDescriptionInstance, data, callback );
        }
        #endregion

        /**
         * REGION Overwriting member of DataService
         */
        #region
        protected override void performeErrorHandling (IException ex, IDataCallback<IDataContainer> callback)
        {
            base.performeErrorHandling (ex, callback);
        }
        protected override IDataManager selectManager (IDataDescription dataDescriptionInstance)
        {
            return base.selectManager (dataDescriptionInstance);
        }
        protected override void init ()
        {
            base.init ();
        }
        #endregion

        /**
         * REGION REAL App specific methods 
         */
        #region
        public void retrieveUser ( IDataCallback<IDataContainer> callback ) 
        { 
            //TODO retrieveUser Rumpf muss umgesetzt werden

            throw new NotImplementedException ();
        }

        public bool isUserAnonymous () 
        {
            throw new NotImplementedException ();
        }
        public bool isUserAvailable () 
        {
            throw new NotImplementedException ();
        }
        public bool isUserVerified ()
        {
            throw new NotImplementedException ();
        }
        //TODO login muss geprüft werden ob diese funktion benögt wird
        public void login ( String userID, IDataCallback<IDataContainer> callback )
        { 
            throw new NotImplementedException ();
        }
        public void logout (  )
        {
            throw new NotImplementedException ();
        }
        //TODO signup muss geprüft werden ob diese funktion benögt wird
        public void signup () 
        {
            throw new NotImplementedException ();
        }




        #endregion
    }
}
