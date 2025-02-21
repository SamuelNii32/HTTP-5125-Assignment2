using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HTTP_5125_Assignment2.Controllers
{
    [Route("api/J2/ChiliPeppers")]
    [ApiController]
    public class ChiliPeppersController : ControllerBase
    {
        private static readonly Dictionary<string, int> PepperSHU = new()
        {
            {"poblano", 1500},
            {"mirasol", 6000},
            {"serrano", 15500},
            {"cayenne", 40000},
            {"thai", 75000},
            {"habanero", 125000}
        };

        /// <summary>
        /// Calculates the total spiciness of the chili based on the peppers added.
        /// </summary>
        /// <param name="NumPeppers">The number of peppers added to the chili.</param>
        /// <param name="Ingredients">A comma-separated list of pepper names added to the chili.</param>
        /// <returns>The total spiciness (SHU) of the chili.</returns>
        /// <example>
        /// GET: https://localhost:7173/api/ChiliPeppers?NumPeppers=4&Ingredients=Poblano,Cayenne,Thai,Poblano
        /// RESPONSE: 118000
        /// 
        /// GET: https://localhost:7173/api/ChiliPeppers?NumPeppers=5&Ingredients=Mirasol,Serrano,Cayenne,Thai,Habanero
        /// RESPONSE: 261500
        /// </example>
        [HttpGet]
        public IActionResult GetTotalSpiciness(int NumPeppers, string Ingredients)
        {
            // Split the Ingredients by comma and make sure the number of peppers matches the given count
            var peppers = Ingredients.Split(',').Select(p => p.Trim().ToLower()).ToArray();

            if (peppers.Length != NumPeppers)
            {
                return BadRequest("The number of peppers provided does not match the NumPeppers parameter.");
            }

            int totalSpiciness = 0;

            // Calculate the total spiciness based on the peppers added
            foreach (var pepper in peppers)
            {
                if (PepperSHU.TryGetValue(pepper, out int value))
                {
                    totalSpiciness += value;
                }
                else
                {
                    return BadRequest($"Unknown pepper: {pepper}");
                }
            }

            return Ok(totalSpiciness);
        }

    }

}