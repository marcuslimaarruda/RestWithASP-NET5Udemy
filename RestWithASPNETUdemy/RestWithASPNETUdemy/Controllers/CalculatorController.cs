using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        // Faz a soma da calculadora
        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult Sum(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);
                return Ok(sum.ToString());
            }
            return BadRequest("Números informados não são validos!");
        }

        // Faz a diferença
        [HttpGet("Subtracao/{firstNumber}/{secondNumber}")]
        public IActionResult Subtracao(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var subtracao = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNumber);
                return Ok(subtracao.ToString());
            }
            return BadRequest("Números informados não são validos!");
        }

        // Faz a diferença
        [HttpGet("divisao/{firstNumber}/{secondNumber}")]
        public IActionResult Divisao(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber) && (ConvertToDecimal(secondNumber) != 0))
            {
                var divisao = ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNumber);
                return Ok(divisao.ToString());
            }
            return BadRequest("Números informados não são validos!");
        }

        [HttpGet("media/{firstNumber}/{secondNumber}")]
        public IActionResult Media(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var media = (ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber))/ 2;
                return Ok(media.ToString());
            }
            return BadRequest("Números informados não são validos!");
        }

        [HttpGet("raiz/{Number}")]
        public IActionResult raiz(string Number)
        {
            if (IsNumeric(Number))
            {
                double raiz = Math.Sqrt(ConvertToDouble(Number));
                return Ok(raiz.ToString());
            }
            return BadRequest("Números informados não são validos!");
        }

        private bool IsNumeric(string strNumber)
        {
            double number;
            // Tenta converter para numero retornando um bollean e aceitando qualquer 
            // padrão de numero levando em conta o pais de origem.
            // Colocando o no final o valor na variável number.
            bool isNumber = double.TryParse(strNumber, 
                System.Globalization.NumberStyles.Any, 
                System.Globalization.NumberFormatInfo.InvariantInfo, 
                out number);

            return isNumber;
        }

        private decimal ConvertToDecimal(string strNumber)
        {
            decimal decimalValue;
            if (decimal.TryParse(strNumber, out decimalValue))
            {
                return decimalValue;
            }
            return 0;
        }

        private double ConvertToDouble(string strNumber)
        {
            double doubleValue;
            if (double.TryParse(strNumber, out doubleValue))
            {
                return doubleValue;
            }
            return 0;
        }

    }
}
