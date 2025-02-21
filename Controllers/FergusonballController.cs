using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HTTP_5125_Assignment2.Controllers
{
    [Route("api/j2/Fergusonball")]
    [ApiController]
    public class FergusonballController : ControllerBase
    {
        /// <summary>
        /// Calculates the number of players with a star rating greater than 40.
        /// Determines if the team is a "gold team".
        /// </summary>
        /// <param name="players">List of player statistics (Points and Fouls).</param>
        /// <returns>
        /// The number of players with a rating > 40. 
        /// If all players have a rating > 40, a "+" is appended.
        /// </returns>
        /// <example>
        /// **Example 1 (Gold Team):**  
        /// POST: api/j2/Fergusonball  
        /// Body: {"players": [{"points": 15, "fouls": 5}, {"points": 11, "fouls": 2}, {"points": 10, "fouls": 1}]}  
        /// Response: "3+"  
        /// </example>
        /// <example>
        /// **Example 2 (Not a Gold Team):**  
        /// POST: api/j2/Fergusonball  
        /// Body: {"players": [{"points": 6, "fouls": 1}, {"points": 10, "fouls": 4}, {"points": 12, "fouls": 3}]}  
        /// Response: "1"  
        /// </example>
        /// <example>
        /// **Example 3 (no Player with Rating > 40):**  
        /// POST: api/j2/Fergusonball  
        /// Body: {"players": [{"points": 8, "fouls": 0}, {"points": 5, "fouls": 2}]}  
        /// Response: "0"  
        /// </example>
        /// <example>
        /// **Example 4 (Invalid Input - Empty List):**  
        /// POST: api/j2/Fergusonball  
        /// Body: {"players": []}  
        /// Response: 400 Bad Request ("Invalid input. The list of players must not be empty.")
        /// </example>
        [HttpPost]
        [Consumes("application/json")]
        public IActionResult CalculateStarRatings([FromBody] TeamData request)
        {
            if (request == null || request.Players == null || request.Players.Count == 0)
            {
                return BadRequest("Invalid input. The list of players must not be empty.");
            }

            int countAbove40 = 0;
            bool isGoldTeam = true;

            foreach (var player in request.Players)
            {
                int starRating = (player.Points * 5) - (player.Fouls * 3);

                if (starRating > 40)
                {
                    countAbove40++;
                }
                else
                {
                    isGoldTeam = false;
                }
            }

            string result = countAbove40.ToString();
            if (isGoldTeam)
            {
                result += "+";
            }

            return Ok(result);
        }
    }

    /// <summary>
    /// Represents the request body containing the list of players.
    /// </summary>
    public class TeamData
    {
        public List<Player> Players { get; set; }
    }

    /// <summary>
    /// Represents an individual player with points and fouls.
    /// </summary>
    public class Player
    {
        public int Points { get; set; }
        public int Fouls { get; set; }
    }
}