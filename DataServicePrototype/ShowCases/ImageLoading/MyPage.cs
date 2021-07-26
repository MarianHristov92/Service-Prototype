// Real-App v5	MyPage
// File:			MyPage.cs
// Folder:		/Users/dimtrirenke/Documents/Repo/x_ServicePrototyp/DataServicePrototype/ShowCases/ImageLoading
// Author:		Dimitri Renke - dimitri.renke@wfp2.com
// Created:		03.11.2016 - 13:21
// Last Modified	03.11.2016 - 13:21
// Copyright:	
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FFImageLoading.Forms;
using Xamarin.Forms;

namespace DataServicePrototype.ImageLoading
{
    

    public class MyPage : CarouselPage
    {
        ScrollView ScrView;
        StackLayout StLInternerContainer;
        //public MyPage ()
        //{
            //Padding = new Thickness ( 0, Device.OS == TargetPlatform.iOS ? 20 : 0, 0, 0 );

            //StLInternerContainer=new StackLayout
            //{
            //    Orientation = StackOrientation.Horizontal
            //};
            //ScrView = new ScrollView
            //{
            //    HorizontalOptions = LayoutOptions.Fill,
            //    Orientation = ScrollOrientation.Horizontal,
            //    Content=StLInternerContainer
            //};
            //CarouselPage pge = new CarouselPage ();
            //pge.s

            //Content = new StackLayout
            //{
            //    Children = {
            //        new Label { Text = "Prospekte" },
            //        ScrView
            //        }
            //};
          //  addImages ();

        //}
        public MyPage () 
        {
            ItemTemplate = new DataTemplate ( () =>
            {
                var image =new CachedImage ()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    WidthRequest = 400,
                    HeightRequest = 640,
                    CacheDuration = TimeSpan.FromDays ( 30 ),
                    DownsampleToViewSize = true,
                    RetryCount = 0,
                    RetryDelay = 250,
                    TransparencyEnabled = false,
                };

                image.SetBinding ( CachedImage.SourceProperty, "imageURL" );
               // image.Source = "http://shared.real.de/blaetterkatalog/pageflipdata/kataloge/KW44/Handzettel/40004/images/preview/page-1.jpg";


                return new ContentPage
                {
                    Padding = new Thickness ( 0, Device.OnPlatform ( 40, 40, 0 ), 0, 0 ),
                    Content = new StackLayout
                    {
                        Children = {
                        image
                    }
                    }
                };
            } );
            ObservableCollection<catalogIm> Zoos = getImages ();

            ItemsSource = Zoos;
            
        }

        //StackLayout getImageCollection () 

        //{
        //    //CatalogEntry catalogData = CatalogEntry.getStaticExample ();
        //    //CatalogConstructor catalogConstructor = new CatalogConstructor ( catalogData );

        //    //return new StackLayout
        //    //{
        //    //    Orientation = StackOrientation.Horizontal,
        //    //    Children = getImageViewCollection ()

        //    //};
        //}



        ObservableCollection<catalogIm> getImages () 
        {
            ObservableCollection<catalogIm> returnCollection = new ObservableCollection<catalogIm> ();
            CatalogEntry catalogData = CatalogEntry.getStaticExample ();
            CatalogConstructor catalogConstructor = new CatalogConstructor ( catalogData );
            for ( int i = 0; i < catalogConstructor.getSiteCount (); i++ )
            {
                string imamgeURL = catalogConstructor.getImageURL ( i );
                returnCollection.Add(new catalogIm{ imageURL=imamgeURL}); 
            }
            return returnCollection;
        }

        Image getImageView (String imageURL="https://t3.ftcdn.net/jpg/00/92/37/74/500_F_92377444_nUBHVrT9MWFfBkqRMV6GHDKaQ2KnrA8V.jpg")
        {
            

            return new Image
            {
                Aspect = Aspect.AspectFit,
                Source = new UriImageSource
                {
                    Uri = new Uri ( imageURL ),
                    CachingEnabled = true,
                    CacheValidity = new TimeSpan ( 0, 0, 1, 0 )

                }
            };
        }
        View getImageFFloader ( String imageURL ="https://t3.ftcdn.net/jpg/00/92/37/74/500_F_92377444_nUBHVrT9MWFfBkqRMV6GHDKaQ2KnrA8V.jpg") 
        { 

            var cachedImage = new CachedImage ()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = 300,
                HeightRequest = 420,
                CacheDuration = TimeSpan.FromDays ( 30 ),
                DownsampleToViewSize = true,
                RetryCount = 0,
                RetryDelay = 250,
                TransparencyEnabled = false,
            };

          

            cachedImage.Source = imageURL;
            return cachedImage;
        }


    }
    public class catalogIm
    {
        public string imageURL { get; set; }
    }
}

