using Microsoft.AspNetCore.Mvc;
using NewSampleAPI.Domain.Model;
using NewSampleAPI.Application.Interface;
using Asp.Versioning;
using NewSampleAPI.Validation;
using FluentValidation;
using FluentValidation.Results;
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewSampleAPI.Controllers
{
    /// <summary>
    ///    Sum of two number
    /// </summary>
    /// <return></return>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CalculatorController : Controller
    {

        private readonly ICalcServices _calcServices;
        private readonly IValidator<CalcModel> _validator;


        public CalculatorController(ICalcServices calcServices, IValidator<CalcModel> validator)
        {
            _calcServices = calcServices;
            _validator = validator;
        }

        [HttpPost]
        [Route("Calculate")]
        public async Task<IActionResult> CalculateOperation([FromQuery]CalcModel calcModel)
        {
            ValidationResult result = await _validator.ValidateAsync(calcModel);

            if (!result.IsValid)
            {
                return ValidationErrors(result);
            }
            var output = await _calcServices.Calculate(calcModel);

            return Ok(output);
        }

        [HttpGet]
        [Route("GetHistory")]
        public async Task<IActionResult> GetAllCalcHistory()
        {

            var output = await _calcServices.GetAllEntry();

            return Ok(output);
        }


        private IActionResult ValidationErrors(ValidationResult result)
        {
            foreach (var err in result.Errors)
            {
                ModelState.AddModelError(err.PropertyName, err.ErrorMessage);
            }

            return ValidationProblem(ModelState);
        }

    }
}

