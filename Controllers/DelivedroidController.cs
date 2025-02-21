using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HTTP_5125_Assignment2.Controllers
{
    [Route("api/J1/Delivedroid")]
    [ApiController]
    public class DelivedroidController : ControllerBase
    {
        /// <summary>
        /// Calculates the final score for the Deliv-e-droid game based on the number of deliveries and collisions.
        /// </summary>
        /// <param name="Collisions">The number of collisions the robot made with obstacles. Must be >= 0.</param>
        /// <param name="Deliveries">The number of successful deliveries made by the robot. Must be >= 0.</param>
        /// <returns>
        /// The final score calculated using the following formula:
        /// - +50 points per delivery.
        /// - -10 points per collision.
        /// - A bonus of 500 points if the number of deliveries is greater than the number of collisions.
        /// </returns>
        /// <example>
        /// POST: localhost:xx/api/J1/Delivedroid
        /// REQUEST BODY: Collisions=2&Deliveries=5
        /// RESPONSE: 730
        /// </example>
        /// <example>
        /// POST: localhost:xx/api/J1/Delivedroid
        /// REQUEST BODY: Collisions=10&Deliveries=0
        /// RESPONSE: -100
        /// </example>
        /// <example>
        /// POST: localhost:xx/api/J1/Delivedroid
        /// REQUEST BODY: Collisions=3&Deliveries=2
        /// RESPONSE: 70
        /// </example>
        [HttpPost] // The default POST route for this controller
        [Consumes("application/x-www-form-urlencoded")]
        public IActionResult CalculateScore([FromForm] int Collisions, [FromForm] int Deliveries)
        {
           

            // Check if the parameters are non-negative
            if (Collisions < 0 || Deliveries < 0)
            {
                return BadRequest("Collisions and Deliveries must be non-negative integers.");
            }

            // Breaking down the score calculation
            int pointsGained = 50 * Deliveries;  // Points gained from deliveries
            int pointsLossed = 10 * Collisions; // Points lost from collisions
            int score = pointsGained - pointsLossed; // Initial score

            // Add bonus if deliveries are greater than collisions
            if (Deliveries > Collisions)
            {
                score += 500; // Add bonus points
            }
            

            return Ok(score);  // Return the final calculated score
        }
    }
}
