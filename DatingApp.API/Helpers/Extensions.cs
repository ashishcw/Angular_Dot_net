using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            //Below will have the error message what we want to send from the application side to the client side            
            response.Headers.Add("Application-Error", message);

            //Below two add methods, will simply add a security bypass so that the message can get transmitted to the front end of the application side.
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }        
    }
}