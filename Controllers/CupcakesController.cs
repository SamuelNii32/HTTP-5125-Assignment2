using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HTTP_5125_Assignment2.Controllers
{
    [Route("api/J1/Cupcakes")]
    [ApiController]
    public class CupcakesController : ControllerBase
    {
        /// <summary>
        /// Calculates the leftover cupcakes after distributing one cupcake per student.
        /// </summary>
        /// <param name="RegularBoxes">The number of regular boxes. Each box contains 8 cupcakes.</param>
        /// <param name="SmallBoxes">The number of small boxes. Each box contains 3 cupcakes.</param>
        /// <returns>
        /// The number of leftover cupcakes after distributing one cupcake to each student.
        /// </returns>
        /// <example>
        /// POST: localhost:xx/api/J1/Cupcakes
        /// REQUEST BODY: RegularBoxes=0&SmallBoxes=10
        /// RESPONSE: 2
        /// 
        /// POST: localhost:xx/api/J1/Cupcakes
        /// REQUEST BODY: RegularBoxes=5&SmallBoxes=2
        /// RESPONSE: 18
        /// 
        /// POST: localhost:xx/api/J1/Cupcakes
        /// REQUEST BODY: RegularBoxes=2&SmallBoxes=4
        /// RESPONSE: 0
        /// </example>
        [HttpPost] // The default POST route for this controller
        [Consumes("application/x-www-form-urlencoded")]
        public int CalculateLeftovers([FromForm] int RegularBoxes, [FromForm] int SmallBoxes)
        {
            // Constants
            int cupcakesPerRegularBox = 8;  // Each regular box contains 8 cupcakes
            int cupcakesPerSmallBox = 3;    // Each small box contains 3 cupcakes
            int students = 28;              // Total number of students

            // Calculate the total number of cupcakes
            int totalCupcakes = (RegularBoxes * cupcakesPerRegularBox) + (SmallBoxes * cupcakesPerSmallBox);

            // Calculate leftovers
            int leftovers = totalCupcakes - students;

            // Return the number of leftover cupcakes
            return leftovers;
        }
    }
}