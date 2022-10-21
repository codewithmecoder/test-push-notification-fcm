using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace test_push_notification_fcm.Controllers;

[ApiController]
[Route("testpush/[controller]/[action]")]
public class PushNotificationFCMController : ControllerBase
{

    [HttpPost("{token}")]
    public async Task<ActionResult> PushNotification(string token)
    {
        //var registrationToken = "fgfOPRTyS-CquiSFmsI7_k:APA91bGrLvRVRkNyaWucMS5487EeM7M9lIxepOgdkT9ETLVXIChAQBOj2qqC1MBbYSaQ-PXpxbVe7ck2Id_-cUvfoV4YYsPCSqWSe92PNEHCDXQfAx0Y04rwwgiAG8oKUAq7f1RRT1j1";
        //var registrationToken = "d5rNHiv1Q2e7oMrBjL04hI:APA91bF_soQu5kq9kuXGvF0UZxPLZcbIZKrPJGY1tYqKqIxNZyi5-7AQ8KGrg9uw7CiEAbuM9CaMKP2CE42mGy8qGFY_kxAO1KFFI6n8JA6qqMGwuWHTiD-JZ4VhPXnLItza7YWoK8Jt";
        var message = new Message()
        {
            Data = new Dictionary<string, string>()
            {
                { "score", "850" },
                { "time", "2:45" },
            },
            Token = token,
        };

        // Send a message to the device corresponding to the provided
        // registration token.
        string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
        // Response is a message ID string.
        Console.WriteLine("Successfully sent message: " + response);
        return Ok(response);
    }
    [HttpPost]
    public async Task<ActionResult> PushNotifications()
    {
        var registrationTokens = new List<string>()
        {
            "fgfOPRTyS-CquiSFmsI7_k:APA91bGrLvRVRkNyaWucMS5487EeM7M9lIxepOgdkT9ETLVXIChAQBOj2qqC1MBbYSaQ-PXpxbVe7ck2Id_-cUvfoV4YYsPCSqWSe92PNEHCDXQfAx0Y04rwwgiAG8oKUAq7f1RRT1j1",
            "fiw7gcMjTIWHArYJVtYngl:APA91bFGnPykohFsLWGUjxrPRVofo57qoocyvd-imu_3J9XnkhHUdga33ol_PXWAyFz3aeHXBlmXv4kwxdiWvM5sZYGQp896CnqlUGw7iltWjmUr6TcL13Ng46XaJBNRyCvoxZqXr8VC"
        };
        var message = new MulticastMessage()
        {
            Tokens = registrationTokens,
            Data = new Dictionary<string, string>()
            {
                { "score", "850" },
                { "time", "2:45" },
            },
        };

        var response = await FirebaseMessaging.DefaultInstance.SendMulticastAsync(message);
        // See the BatchResponse reference documentation
        // for the contents of response.
        Console.WriteLine($"{response.SuccessCount} messages were sent successfully");
        return Ok();
    }
}
