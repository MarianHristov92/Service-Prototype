// Real-App v5	CatalogConstructor
// File:			CatalogConstructor.cs
// Folder:		/Users/dimtrirenke/Documents/Repo/x_ServicePrototyp/DataServicePrototype/ShowCases/ImageLoading
// Author:		Dimitri Renke - dimitri.renke@wfp2.com
// Created:		03.11.2016 - 13:54
// Last Modified	03.11.2016 - 13:54
// Copyright:	
using System;
using System.Text;

namespace DataServicePrototype.ImageLoading
{
    public class CatalogConstructor
    {
        CatalogEntry Data;
        public CatalogConstructor ( CatalogEntry data)
        {
            Data = data;   
        }
        public int getSiteCount ()
        {
            int count = 0;
            if ( Data != null )
                int.TryParse ( Data.pages, out count );
            return count;
        }
        public String getImageURL(int index) 
        {
            StringBuilder builder = new StringBuilder ();
            builder.Append ( Data.imagesBaseURL )
                   .Append ( Data.imagesFilePreFix )
                   .Append ( index )
                   .Append ( Data.imagesFilePostFix);
            return builder.ToString ();
        }
    }

}
