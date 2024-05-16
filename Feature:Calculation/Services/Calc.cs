using NewSampleAPI.Application.Interface;
using NewSampleAPI.Domain.Model;
using NewSampleAPI.Domain.Enum;
using Serilog;

namespace NewSampleAPI.Service
{
    public class CalcServices : ICalcServices
    {
        private readonly ILogger _logger;
        private readonly ICalcRespos _calcRespos;

        public CalcServices(ILogger logger, ICalcRespos calcRespos)
        {
            _logger = logger;
            _calcRespos = calcRespos;
        }

        public async Task<int> Calculate(CalcModel calcModel)
        {
            _logger.Information($"Calculating for {calcModel.operators}");
            try
            {
                
                var opr = calcModel.operators switch
                {
                    CalcEnum.Add => calcModel.firstOperand + calcModel.secondOperand,
                    CalcEnum.Subtract => calcModel.firstOperand - calcModel.secondOperand,
                    CalcEnum.Multiply => calcModel.firstOperand * calcModel.secondOperand,
                    CalcEnum.Divide => calcModel.firstOperand/calcModel.secondOperand,
                    CalcEnum.Mod => calcModel.firstOperand % calcModel.secondOperand,

                    _ => throw new NotImplementedException()
                };

              await _calcRespos.CreateEntryForCal(calcModel, opr);

                return opr;
            }

            catch (Exception ex)
            {
                _logger.Error($"Error while calculating {calcModel.firstOperand} {calcModel.operators} {calcModel.secondOperand} - {ex.InnerException}");
                throw;
            }

        }

        public async Task<List<CalcModel>> GetAllEntry()
        {
            try
            {
                return await _calcRespos.GetAllEntry();
            }

            catch (Exception ex)
            {
                _logger.Error($"Error while Getting all records: {ex} ");
                throw;
            }
        }
    }
}

