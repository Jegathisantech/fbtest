using Microsoft.AspNetCore.Mvc;

namespace testapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebhookController : ControllerBase
    {
        // ✅ Facebook Webhook Verification (GET request)
        [HttpGet]
        public IActionResult VerifyWebhook()
        {
            Console.WriteLine("Received verification request");

            var mode = Request.Query["hub.mode"].ToString();
            var challenge = Request.Query["hub.challenge"].ToString();
            var token = Request.Query["hub.verify_token"].ToString();

            Console.WriteLine($"Mode: {mode}, Token: {token}, Challenge: {challenge}");


            // Verify the mode and token
            if (mode == "subscribe" && token == "test")
            {
                Console.WriteLine("Webhook verified successfully");
                return Content(challenge, "text/plain");
            }

            Console.WriteLine("Webhook verification failed");
            return BadRequest("Verification failed");
        }

        // ✅ Facebook Webhook Event Notification (POST request)
        [HttpPost]
        public IActionResult Receive([FromBody] object payload)
        {
            // Log or process the callback payload
            Console.WriteLine($"Webhook received: {payload}");

            return Ok(new { status = "success" });
        }
    }
}
