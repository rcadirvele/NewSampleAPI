using Amazon.DynamoDBv2.DataModel;
using NewSampleAPI.Application.Dtos;
using NewSampleAPI.Domain.Model;
using NewSampleAPI.Application.Interface;
using AutoMapper;
using Amazon.Runtime.Internal.Endpoints.StandardLibrary;

namespace Application.FeatureCalculation.Repos.Command
{
    public class AddCalc : ICalcRespos
	{
		private readonly IDynamoDBContext _context;

        private readonly IMapper _mapper;


        public AddCalc(IDynamoDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;


        }


        public async Task CreateEntryForCal(CalcModel calcMode, int result)
        {

           var map = _mapper.Map<CalcModel, CalcModelDto>(calcMode);

            map.Result = result;

           await _context.SaveAsync(map);

        }

        public async Task<List<CalcModel>> GetAllEntry()
        {

            var calcModelDtosReturn = await _context.ScanAsync<CalcModelDto>(default).GetRemainingAsync();

            var map = _mapper.Map<List<CalcModel>>(calcModelDtosReturn);

            return map;
          
        }

    }
}

