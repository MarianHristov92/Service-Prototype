// Real-App v5	EmptyClass
// File:			EmptyClass.cs
// Folder:		/Users/dimtrirenke/Documents/Repo/x_ServicePrototyp/DataServicePrototype/ShowCases
// Author:		Dimitri Renke - dimitri.renke@wfp2.com
// Created:		27.10.2016 - 09:25
// Last Modified	27.10.2016 - 09:25
// Copyright:	
using System;
using Data.Description.Domain.Asset;
using LibDataService.CacheManager;
using Data.Description.DomainAssociator;
using LibDataService.DataModels.Container;
using LibDataService.SettingsManager;
using System.Threading.Tasks;

namespace Data.Description.Domain.Cache
{
    public class CacheShowCase
    {
        CacheManagerDictonary cacheMan = new CacheManagerDictonary (); 
        public CacheShowCase ()
        {
        }

        public void writeData () 
        {

            cacheMan.setData ( DomainAssociator.DomainAssociator.Asset.Example.IntExampleAssetDomain, new SettingsDataInt( 2 ) );
            cacheMan.setData ( DomainAssociator.DomainAssociator.Asset.Example.StringExampleAssetDomain, new SettingsDataString ( "hier mein text" ) );
        }
        public void readData () 
        {
            cacheMan.getData ( DomainAssociator.DomainAssociator.Asset.Example.IntExampleAssetDomain, new SettingsCallbackDebug<IDataContainer> () );
            cacheMan.getData ( DomainAssociator.DomainAssociator.Asset.Example.StringExampleAssetDomain, new SettingsCallbackDebug<IDataContainer> () );
        }
        public void readDataDelayed () 
        {
            ReadWithDelay ();
        }
        private async Task ReadWithDelay ()
        {
            await Task.Delay ( 1000 );
            readData ();

        }
        public void deleteData () 
        {
        }
        public void readDataNotExist ()
        {
            cacheMan.getData ( DomainAssociator.DomainAssociator.Asset.Example.JSONExampleAssetDomain, new SettingsCallbackDebug<IDataContainer> () );
        }



    }
}
