// Real-App v5	AssetShowCase
// File:			AssetShowCase.cs
// Folder:		/Users/dimtrirenke/Documents/Repo/x_ServicePrototyp/DataServicePrototype/ShowCases/Asset
// Author:		Dimitri Renke - dimitri.renke@wfp2.com
// Created:		27.10.2016 - 13:43
// Last Modified	27.10.2016 - 13:43
// Copyright:	
using System;
using System.Threading.Tasks;
using Data.Description.DomainAssociator;
using LibDataService;
using LibDataService.AssetManager;
using LibDataService.CacheManager;
using LibDataService.DataModels.Container;
using LibDataService.SettingsManager;

namespace DataServicePrototype.Asset
{
    public class AssetShowCase
    {
        AssetManager AssetMan = new AssetManager ();
        public AssetShowCase ()
        {
            
        }
        public void readWithOutCloud ()
        {   
            Log.i ( "readWithOutCloud" );
            AssetMan.getData ( DomainAssociator.Asset.Example.IntExampleAssetDomain, new SettingsCallbackDebug<IDataContainer> () );
            AssetMan.getData ( DomainAssociator.Asset.Example.ComplexDynDomainExampleAssetDomain, new DTUserDataContainer("1170128393"), new SettingsCallbackDebug<IDataContainer> () );

        }
        public void writeWithOutCloud ()
        {
            AssetMan.setData ( DomainAssociator.Asset.Example.IntExampleAssetDomain, new SettingsDataInt ( 2 ),new SettingsCallbackDebug<IDataContainer> () );
        }
        public void readDataDelayed ()
        {
            ReadWithDelay (10);
            ReadWithDelay ( 300 );
        }
        private async Task ReadWithDelay (int DELAY)
        {
            
            await Task.Delay ( DELAY *100);
            Log.i ( "readWithOutCloud with delay of " + DELAY );
            readWithOutCloud ();
            Log.show ( typeof ( CacheManagerDictonary ) );

        }
    }
}
