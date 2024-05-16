using System;
using NewSampleAPI.Application.Dtos;
using NewSampleAPI.Domain.Model;

namespace NewSampleAPI.Application.Interface
{
	public interface ICalcRespos
	{
        public Task CreateEntryForCal(CalcModel calcModel, int result);

        public Task<List<CalcModel>> GetAllEntry();

    }
}

