namespace KBCFoodAndGo.Shared.Constants
{
    public class ApiConstants
    {
        protected ApiConstants()
        {
        }

        private const string BackendUrl = "http://192.168.43.135:8080";
        private const string PythonBackendUrl = "http://192.168.43.135:5000";

        public const string BaseApiUrl = BackendUrl;
        public const string PythonBaseApiUrl = PythonBackendUrl;
        public const string MealEndpoint = "/api/meal/";
        public const string LogEndPoint = "/api/log/";
        public const string MenuEndpoint = "/api/menu/";
        public const string DayMenuEndpoint = "/api/menu/name/";
        public const string UserEndpoint = "/api/user/";
        public const string MealSearchEndpoint = "/api/meal/search/";
        public const string MealHistoryEndpoint = "api/mealHistory/";

        private const string AwsUrl = "https://7nc6panmcf.execute-api.us-east-1.amazonaws.com";
        public const string BaseAwsApiUrl = AwsUrl;
        public const string ScanMealEndPoint = "/test/image"; 
        public const string ScanFaceEndPoint = "/test/face";
    }
}
