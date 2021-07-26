// Real-App v5	LoyalityStatus
// File:			LoyalityStatus.cs
// Folder:		/Users/dimtrirenke/Documents/Repo/x_ServicePrototyp/DataServicePrototype/ShowCases/Asset/loyalityExample
// Author:		Dimitri Renke - dimitri.renke@wfp2.com
// Created:		02.11.2016 - 10:41
// Last Modified	02.11.2016 - 10:41
// Copyright:	
using System;
namespace DataServicePrototype.loyalityExample
{
    public class GetAccountStatusResponse
    {
        public string mastercard { get; set; }
        public string creationDate { get; set; }
        public string partnercard { get; set; }
        public int currentPoints { get; set; }
        public int totalPoints { get; set; }
        public bool isLoyaltySubscriber { get; set; }
        public string lastStatusChange { get; set; }
        public bool eBonUser { get; set; }
        public bool wantPrintReceipt { get; set; }
        public string errorCode { get; set; }
        public int numUnredeemedCoupons { get; set; }
        public int totalUnredeemedPoints { get; set; }
        public int pointsToNextCoupon { get; set; }
        public int redeemedCoupons { get; set; }
        public string redeemableText { get; set; }
    }

    public class Result
    {
        public GetAccountStatusResponse getAccountStatusResponse { get; set; }
    }

    public class RootObject
    {
        public string status { get; set; }
        public Result result { get; set; }
        public static RootObject fromString ()
        {
            String json_row = "{\n    \"status\": \"ok\",\n    \"result\": {\n        \"getAccountStatusResponse\": {\n            \"mastercard\": \"3083421170128393\",\n            \"creationDate\": \"2014-01-20T11:34:22.058+01:00\",\n            \"partnercard\": \"3083421170128401\",\n            \"currentPoints\": 10,\n            \"totalPoints\": 10,\n            \"isLoyaltySubscriber\": true,\n            \"lastStatusChange\": \"2016-10-13T14:14:31.836+02:00\",\n            \"eBonUser\": false,\n            \"wantPrintReceipt\": true,\n            \"errorCode\": \"0000\",\n            \"numUnredeemedCoupons\": 0,\n            \"totalUnredeemedPoints\": 10,\n            \"pointsToNextCoupon\": 10,\n            \"redeemedCoupons\": 0,\n            \"redeemableText\": \"Nur noch <strong>10 Treue-Marke(n)</strong> bis zu Ihrer nächsten Prämie!\"\n        }\n    }\n}";
            RootObject returnData = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject> ( json_row );
            return returnData;
        }
    }


}
