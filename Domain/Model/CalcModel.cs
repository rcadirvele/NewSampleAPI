using NewSampleAPI.Domain.Enum;

namespace NewSampleAPI.Domain.Model
{
    public class CalcModel
    {
		public int firstOperand { get; set; }

        public int secondOperand { get; set; }

        public CalcEnum operators { get; set; }
	}
}

