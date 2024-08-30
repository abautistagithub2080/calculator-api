
using System;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace CALCULATOR_API.Models
{
    public class Operations
    {
        public async Task<decimal> Sum(Calculator oNumbers)
        {
            decimal Result = 0;
            Result = decimal.Parse(oNumbers.NumberOne) + decimal.Parse(oNumbers.NumberTwo);              
            return Result;
        }
        public async Task<decimal> Subtraction(Calculator oNumbers)
        {
            decimal Result = 0;
            Result = decimal.Parse(oNumbers.NumberOne) - decimal.Parse(oNumbers.NumberTwo);
            return Result;
        }
        public async Task<decimal> Multiply(Calculator oNumbers)
        {
            decimal Result = 0;
            Result = decimal.Parse(oNumbers.NumberOne) * decimal.Parse(oNumbers.NumberTwo);
            return Result;
        }
        public async Task<decimal>Div(Calculator oNumbers)
        {
            decimal Result = 0;
            Result = decimal.Parse(oNumbers.NumberOne) / decimal.Parse(oNumbers.NumberTwo);
            return Result;
        }

        public async Task<decimal> GenerateOperations(Calculator oNumbers)
        {
            string iOperations = oNumbers.NumberOne;
            decimal Result = EvaluatorExpressions.EvaluarExpresionPostFija(iOperations);
            return Result;
        }

        public async Task<decimal> Factory(Calculator oNumbers)
        {
            int input = int.Parse(oNumbers.NumberOne);
            int result = 1;
            for (int i = input; i > 0; i--)
                result *= i;
            return result;
        }
        
    }
}
