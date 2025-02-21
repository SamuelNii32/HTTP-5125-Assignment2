using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HTTP_5125_Assignment2.Controllers
{
    [Route("api/J3/DecodeInstructions")]
    [ApiController]
    public class DecodeInstructionsController : ControllerBase
    {
        /// <summary>
        /// Decodes instructions to determine the direction and number of steps to take.
        /// 
        /// **Example 1 (Valid Input):**  
        /// POST: api/J3/DecodeInstructions  
        /// Body: Instruction=57234&Instruction=00907&Instruction=34100&Instruction=99999  
        /// Response:  
        /// [  
        ///    "right 234",  
        ///    "right 907",  
        ///    "left 100"  
        /// ]  
        ///  
        /// **Example 2 (Valid Input - Different Instructions):**  
        /// POST: api/J3/DecodeInstructions  
        /// Body: Instruction=34100&Instruction=00999&Instruction=57123&Instruction=99999  
        /// Response:  
        /// [  
        ///    "left 100",  
        ///    "right 999",  
        ///    "left 123"
        /// ]  
        ///  
        /// **Example 3 (Invalid Input - Empty Instruction List):**  
        /// POST: api/J3/DecodeInstructions  
        /// Body:  
        /// {  
        ///    "error": "Invalid input. At least two instructions are required."  
        /// }  
        /// </summary>
        /// <param name="Instructions">The list of instructions containing direction and steps (5 digits).</param>
        /// <returns>The decoded direction and steps in the format: "direction steps".</returns>
        [HttpPost]
        [Consumes("application/x-www-form-urlencoded")]
        public IActionResult DecodeInstructions([FromForm] List<string> Instructions)
        {
            // Ensure that instructions list has at least two instructions before 99999
            if (Instructions == null || Instructions.Count < 2 || Instructions[^1] != "99999")
            {
                return BadRequest("Invalid input. At least two instructions are required, and the last instruction must be 99999.");
            }

            var results = new List<string>();
            string previousDirection = null;

            // Process instructions, ignoring the last "99999"
            foreach (var instruction in Instructions)
            {
                if (instruction == "99999")
                    break;  // Stop processing when encountering "99999"

                if (instruction.Length != 5)
                {
                    return BadRequest("Each instruction must be exactly 5 digits.");
                }

                int firstDigit = int.Parse(instruction.Substring(0, 1));
                int secondDigit = int.Parse(instruction.Substring(1, 1));
                int steps = int.Parse(instruction.Substring(2, 3));

                string direction = string.Empty;
                int sum = firstDigit + secondDigit;

                if (sum == 0)
                {
                    // Direction is the same as the previous instruction
                    if (previousDirection == null)
                    {
                        return BadRequest("First instruction cannot have 00 as the first two digits.");
                    }
                    direction = previousDirection;
                }
                else if (sum % 2 == 1)
                {
                    // Odd sum means "left"
                    direction = "left";
                }
                else
                {
                    // Even sum (excluding zero) means "right"
                    direction = "right";
                }

                // Prepare the decoded instruction
                results.Add($"{direction} {steps}");

                // Update the previous direction for future instructions
                previousDirection = direction;
            }

            return Ok(results);
        }
    }
}
