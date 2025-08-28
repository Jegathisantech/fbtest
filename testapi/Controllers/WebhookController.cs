using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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

            if (mode == "subscribe" && token == "test")
            {
                Console.WriteLine("Webhook verified successfully");
                return Content(challenge, "text/plain");
            }

            Console.WriteLine("Webhook verification failed");
            return BadRequest("Verification failed");
        }

        [HttpPost]
        public async Task<IActionResult> ReceiveWebhook()
        {
            try
            {
                using var reader = new StreamReader(Request.Body);
                var requestBody = await reader.ReadToEndAsync();

                if (string.IsNullOrWhiteSpace(requestBody))
                {
                    Console.WriteLine("⚠️ Empty request body received");
                    return Ok();
                }

                Console.WriteLine($"Received webhook payload: {requestBody}");

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing webhook: {ex.Message}");
                return StatusCode(500, $"Error processing webhook: {ex.Message}");
            }
        }
    }
}
