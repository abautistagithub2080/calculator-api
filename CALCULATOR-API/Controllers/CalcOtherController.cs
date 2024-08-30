using CALCULATOR_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CALCULATOR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalcOtherController : ControllerBase
    {

        private readonly Operations _operation;
        public CalcOtherController(Operations operation)
        {
            _operation = operation;
        }

        [HttpPatch]
        public async Task<IActionResult> factorial([FromBody] Calculator oNumbers)
        {
            decimal ResNumber = await _operation.Factory(oNumbers);
            return StatusCode(StatusCodes.Status200OK, new { Results = ResNumber, isSuccess = true });
        }


    }
}
