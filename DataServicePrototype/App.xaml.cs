using Xamarin.Forms;
using System.Diagnostics;
using LibDataService.SettingsManager;
using System;
using Data.Description.Domain;
using LibDataService.DataModels.Description.Domain;
using Data.Description.Domain.Settings;
using Data.Description.Domain.Asset;
using LibDataService;
using Data.Description.DomainAssociator;
using Data.Description.Domain.Cache;
using System.Threading;
using System.Threading.Tasks;
using DataServicePrototype.Asset;
using DataServicePrototype.ImageLoading;
using LibDataService.Helper;
using DataServicePrototype.ShowCases;

namespace DataServicePrototype
{
    public partial class App : Application
    {
        public App ()
        {
            InitializeComponent ();

            MainPage = new DataServicePrototypePage ();

            /**
             * REGION Setting Manager ShowCase
             */
            //performeSettingsShowCase ();

            /**
             * REGION Cache Manager ShowCase
             */
            //performeCacheShowCase ();

            /**
             * REGION Asset Manager ShowCase
             */
            try
            {
                performAssetShowCase ();
            }
            catch (Exception e) 
            {
                Log.i ( (e as BaseException).toJson ().ToString () );   
                Log.i ( e.ToString() );
                Log.i ( e.StackTrace );
                Log.i ( e.Message );
            }


            /* 
             * REGION Image Loading with FFloader
             */
            //MainPage = new MyPage ();

            //BaseException first = new BaseException ("Blabla",new NotImplementedException());
            //Log.i(first.toJson ().ToString());
            //first.Info.Reason = "das ist der Erste Fehler";
            //first.Info.ReasonCode = "Test erster Fehler";
            //BaseException secound = new BaseException ( first );
            //secound.Info.Reason = "das ist der Zweiter Fehler";
            //secound.Info.ReasonCode = "Test Zweiter Fehler";
            //Log.i ( secound.toJson ().ToString () );
            //BaseException therd = new BaseException ( secound );
            //therd.Info.Reason = "das ist der Dritter Fehler";
            //therd.Info.ReasonCode = "Test Dritter Fehler";
            //Log.i ( therd.toJson ().ToString () );

        }
        /**
         * REGION Cache Manager ShowCase
         */
        #region
        void performeCacheShowCase ()
        {
            CacheShowCase showCaseCache = new CacheShowCase ();
            showCaseCache.writeData ();
            showCaseCache.readData ();
            showCaseCache.readDataDelayed ();
        }
        #endregion
        /**
         * REGION Setting Manager ShowCase
         */
        #region
        void performeSettingsShowCase ()
        {
            SettingsShowCase showCaseSetting = new SettingsShowCase ();
            //save settings
            showCaseSetting.saveSettings (34,"ich bin gespeichert");
            //load settings
            showCaseSetting.saveSettings (32,"meiness");
            showCaseSetting.loadSettings ();
        }

        #endregion
        /**
         * REGION Asset Manager ShowCase
         */
        #region
        void performAssetShowCase () 
        {
            
            AssetShowCase showCaseAsset = new AssetShowCase ();
            showCaseAsset.writeWithOutCloud ();
            showCaseAsset.readWithOutCloud ();
            showCaseAsset.readDataDelayed ();



        }
        #endregion

        protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}


    }
}
