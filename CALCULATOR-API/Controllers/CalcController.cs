using CALCULATOR_API.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace CALCULATOR_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CalcController : ControllerBase
    {

        private readonly Operations _operation;
        public CalcController(Operations operation)
        {
            _operation = operation;
        }

        [HttpPost]
        public async Task<IActionResult> generateOperations([FromBody] Calculator oNumbers)
        {

            decimal ResNumber = await _operation.GenerateOperations(oNumbers);
            return StatusCode(StatusCodes.Status200OK, new { Results = ResNumber, isSuccess = true });
        }
    }
}
