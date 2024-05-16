using NewSampleAPI.Application.Interface;
using NewSampleAPI.Domain.Enum;
using NewSampleAPI.Domain.Model;
using NewSampleAPI.Service;
using Serilog;

namespace NewSampleAPI.Tests;

[TestFixture]
public class CalcTests
{
    private ICalcServices _calcServices;
    private Mock<ICalcServices> _calcMock;
    private Mock<ICalcRespos> _calcReposMock;
    private Mock<ILogger> _logger;
    private static CalcModel _calcModel;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _calcMock = new Mock<ICalcServices>();
        _calcReposMock = new Mock<ICalcRespos>();
        _logger = new Mock<ILogger>();
        _calcServices = new CalcServices(_logger.Object, _calcReposMock.Object);
    }

    [SetUp]
    public void Setup()
    {
        _calcModel = new CalcModel
        {
            firstOperand = 4,
            secondOperand = 2
        };
    }

    [Test]
    public async Task Calc_Add_Test_Success() 
    {
        //Arrange

        _calcReposMock.Setup(x => x.CreateEntryForCal(_calcModel, It.IsAny<int>()));

        _calcMock.Setup(x => x.Calculate(_calcModel)).ReturnsAsync(It.IsAny<int>());

        //Act
        _calcModel.operators = CalcEnum.Add;
        var actual = await _calcServices.Calculate(_calcModel);

        //Assert
        int expected = 6;
        Assert.That(actual, Is.EqualTo(expected));
        
    }

    [Test]
    public async Task Calc_Add_Test_Failure()
    {
        //Arrange



        _calcReposMock.Setup(x => x.CreateEntryForCal(_calcModel, It.IsAny<int>()));

        _calcMock.Setup(x => x.Calculate(_calcModel)).ReturnsAsync(It.IsAny<int>());

        //Act
        _calcModel.operators = CalcEnum.Add;
        var actual = await _calcServices.Calculate(_calcModel);

        //Assert
        int expected = 7;
        Assert.That(expected, Is.Not.EqualTo(actual));

    }

    [Test]
    public async Task calc_History_Success()
    {
        List<CalcModel> calcModels = new List<CalcModel>();

        var cal = new CalcModel(){ firstOperand = 3, operators = CalcEnum.Multiply, secondOperand = 2 };

        calcModels.Add(cal);

        _calcModel.operators = CalcEnum.Add;
        _calcReposMock.Setup(c => c.GetAllEntry()).ReturnsAsync(calcModels);

        _calcMock.Setup(x => x.GetAllEntry()).ReturnsAsync(calcModels);

        var result = await _calcServices.GetAllEntry();


        Assert.IsTrue(result.Count is not 0);
        Assert.That(result.First().firstOperand, Is.EqualTo(cal.firstOperand));
        Assert.That(result.First().operators, Is.EqualTo(cal.operators));


    }
}
