using NewSampleAPI.Domain.Model;

namespace NewSampleAPI.Application.Interface
{
	public interface ICalcServices
	{
		public Task<int> Calculate(CalcModel calcModel);

		public Task<List<CalcModel>> GetAllEntry();

    }
}

