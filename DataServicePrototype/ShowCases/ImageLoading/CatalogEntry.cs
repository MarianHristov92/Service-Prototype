// Real-App v5	CatalogEntry
// File:			CatalogEntry.cs
// Folder:		/Users/dimtrirenke/Documents/Repo/x_ServicePrototyp/DataServicePrototype/ShowCases/ImageLoading
// Author:		Dimitri Renke - dimitri.renke@wfp2.com
// Created:		03.11.2016 - 13:53
// Last Modified	03.11.2016 - 13:53
// Copyright:	
using System;

using Xamarin.Forms;

namespace DataServicePrototype.ImageLoading
{
    public class CatalogEntry
    {
        public string name { get; set; }
        public string group { get; set; }
        public string bkz { get; set; }
        public string url { get; set; }
        public string pdf { get; set; }
        public string size { get; set; }
        public string pages { get; set; }
        public string width { get; set; }
        public string height { get; set; }
        public string imagesBaseURL { get; set; }
        public string imagesFilePreFix { get; set; }
        public string imagesFilePostFix { get; set; }
        public string imagesFileSize { get; set; }
        public string imageIntroURL { get; set; }
        public string webUrl { get; set; }
        public string pageflipdataUrl { get; set; }
        public string pageflipdataService { get; set; }
        public string week { get; set; }
        public string validFromDate { get; set; }
        public string validFromWeek { get; set; }
        public string validFromWeekYear { get; set; }
        public string validUntilDate { get; set; }
        public string validUntilWeek { get; set; }
        public string validUntilWeekYear { get; set; }
        public string displayFromDate { get; set; }
        public string displayFromWeek { get; set; }
        public string displayFromWeekYear { get; set; }
        public string displayUntilDate { get; set; }
        public string displayUntilWeek { get; set; }
        public string displayUntilWeekYear { get; set; }
        public static CatalogEntry getStaticExample ()
        {
            String rowString = "{\nname: \"Prospekt Woche 44\",\ngroup: \"Handzettel\",\nbkz: \"8017;8004;8929\",\nurl: \"http://shared.real.de/blaetterkatalog/pageflipdata/kataloge/KW44/Handzettel/40004/pdf/40004_KW44_Handzettel.pdf\",\npdf: \"http://shared.real.de/blaetterkatalog/pageflipdata/kataloge/KW44/Handzettel/40004/pdf/src/40004_KW44_Handzettel.pdf\",\nsize: \"51.05 MiB\",\npages: \"64\",\nwidth: \"860\",\nheight: \"1420\",\nimagesBaseURL: \"http://shared.real.de/blaetterkatalog/pageflipdata/kataloge/KW44/Handzettel/40004/images/preview/\",\nimagesFilePreFix: \"page-\",\nimagesFilePostFix: \".jpg\",\nimagesFileSize: \"20.95 MiB\",\nimageIntroURL: \"http://shared.real.de/blaetterkatalog/pageflipdata/kataloge/KW44/Handzettel/40004/images/mobile/page-0.jpg\",\nwebUrl: \"http://prospekt.real.de/wochenprospekte/prospekt/Handzettel/kw/44.html\",\npageflipdataUrl: \"http://shared.real.de/blaetterkatalog/pageflipdata/kataloge/KW44/Handzettel/40004/config/pageflipdata.html\",\npageflipdataService: \"http://shared.real.de/blaetterkatalog/webservice/?mode=pageflipdata&pageflipdataWeek=44&pageflipdataType=Handzettel&pageflipdataWBK=40004&pageflipdataPages=64&pageflipdataWidth=860&pageflipdataHeight=1420\",\nweek: \"44\",\nvalidFromDate: \"2016-10-31\",\nvalidFromWeek: \"44\",\nvalidFromWeekYear: \"2016\",\nvalidUntilDate: \"2016-11-05\",\nvalidUntilWeek: \"44\",\nvalidUntilWeekYear: \"2016\",\ndisplayFromDate: \"2016-10-29\",\ndisplayFromWeek: \"43\",\ndisplayFromWeekYear: \"2016\",\ndisplayUntilDate: \"2016-11-05\",\ndisplayUntilWeek: \"44\",\ndisplayUntilWeekYear: \"2016\"\n}";
            CatalogEntry returnData = Newtonsoft.Json.JsonConvert.DeserializeObject<CatalogEntry> ( rowString );
            return returnData;
        }
    }


}

