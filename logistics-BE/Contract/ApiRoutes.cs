using System;
namespace logistics_BE.Contract
{
    public class ApiRoutes
    {
        private const string RootApi = "api";
        public static class CityAPI
        {
            private const string City = RootApi + "/City";

            //GET endpoints
            public const string GetAll = City + "/All";

            // Post
            public const string Post = City + "/Create";

            public const string PostBulk = City + "/BulkCreate";

            public const string Update = City + "/Update";
        }

        public static class RoadApi
        {
            private const string Road = RootApi + "/Road";

            //GET endpoints
            public const string GetAll = Road + "/All";

            // Post
            public const string Post = Road + "/Create";

            public const string PostBulk = Road + "/BulkCreate";

            public const string Update = Road + "/Update";

        }

        public static class LogisticsCenterApi
        {
            private const string LogisticsCenter = RootApi + "/LogisticsCenter";

            // GET ENDPOINTS
            public const string GetLogisticsCenter = LogisticsCenter + "/Retrieve";
        }
    }
}
