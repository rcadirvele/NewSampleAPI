using FluentValidation;
using NewSampleAPI.Domain.Model;

namespace NewSampleAPI.Validation
{
	public class CalcValidator : AbstractValidator<CalcModel>
	{
		public CalcValidator()
		{
            RuleFor(x => x.firstOperand).NotEmpty().GreaterThan(0);

			RuleFor(x => x.secondOperand).NotEmpty().GreaterThan(0);

			RuleFor(x => x.operators).IsInEnum();

        }
	}
}

