using Microsoft.AspNetCore.Http;

namespace DatingApp.API.helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message) {
            // when the server returns error, it does not return the header which make the error
            // message confusing, that is why we are adding them.
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}