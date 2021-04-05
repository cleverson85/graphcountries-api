namespace Domain.Util
{
    public static class Endpoints
    {
        public static class Route
        {
            public const string POST = "";
            public const string PUT = "";
            public const string DELETE = "{id}";
            public const string GET_BY_ID = "{id}";
            public const string GET_BY_CAPITAL_NAME = "{capitalName}";
            public const string GET_BY_COUNTRY_NAME = "{countryName}";
            public const string GET_PAGE = "{pageIndex}/{pageSize}";
            public const string GET_ALL = "";
        }

        public static class Recursos
        {
            public const string Countries = "countries";
        }
    }
}
