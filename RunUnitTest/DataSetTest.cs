using System.Buffers.Text;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using DataSetUiTest;
using LoginUiTest;
using Moq;
using NSubstitute;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace RunUnitTest;

public class DataSetTest : IDisposable
{
    private readonly IWebDriver driver;

    private readonly LoginMain loginObj;

	private readonly DataConsole data;

	private static readonly string _URL = "http://197.255.51.104:9035";


    public DataSetTest()
    {
        driver = new ChromeDriver();
        loginObj = new LoginMain();
	    data = new DataConsole();

	}

	[Fact]
	public void ClickDataSet_ShouldReturnTrueOnSuccess()
	{
		bool loginSuccess = loginObj.LoginSuccess(driver);
		Assert.True(loginSuccess.Equals(true));

		var clickDataSet = new DataConsole();

		var check = clickDataSet.ClickDataSet(driver);

		Assert.True(check.Equals(true));

	}

	[Fact]
	public void ClickSector_ShouldReturnTrueOnSuccess()
	{
		bool loginSuccess = loginObj.LoginSuccess(driver);
		Assert.True(loginSuccess.Equals(true));


		var clickDataSet = new DataConsole();

		clickDataSet.ClickDataSet(driver);
		var check = clickDataSet.ClickSector(driver);

		Assert.True(check.Equals(true));

	}

    [Fact]
    public void Click_CreateNew_DataSector_Success()
    {
		
		var createSecMock = new Mock<CreateDataSector>();
        createSecMock.Setup(cs => cs.ClickNew());
        createSecMock.Setup(cs => cs.EnterNameAndTitle(It.IsAny<string>(), It.IsAny<string>()));
        createSecMock.Setup(cs => cs.ClickSubmit());
        createSecMock.Setup(cs => cs.textMsgRes.Text).Returns("Data Sector information was saved successfully"); 

        var jsonFileReaderMock = new Mock<JsonFileReader>();
        jsonFileReaderMock.Setup(jfr => jfr.ReadJsonFileCreateSector()).Returns(new DataSector
        {
            SectorField = new SectorField
            {
                Name = "XUnit Test",
                Title = "XUnit Title"
            }
        });

		var myClass = new DataConsole();

		var result = myClass.CreateNewDataSectorSuccess(driver);


		//// Assert
		Assert.Equal("", result);

	}


	[Fact]
	public void ClickNewRequest_ShouldReturnTrueOnSuccess()
	{
		var driverMock = new Mock<IWebDriver>();
		var elementMock = new Mock<IWebElement>();

		driverMock
			.Setup(d => d.FindElement(It.IsAny<By>()))
			.Returns(elementMock.Object);

		bool result = DataConsole.ClickNewRequest(driverMock.Object);

		Assert.True(result);

		elementMock.Verify(e => e.Click(), Times.Once);
	}

	[Fact]
	public void ClickNewRequest_ShouldReturnFalseOnError()
	{
		var driverMock = new Mock<IWebDriver>();

		driverMock
			.Setup(d => d.FindElement(It.IsAny<By>()))
			.Throws(new Exception("Test Exception"));

		var result = DataConsole.ClickNewRequest(driverMock.Object);

		Assert.False(result);
	}

	[Fact]
	public void TestClickRequestType()
	{
		var driverMock = new Mock<IWebDriver>();
		var newRequestMock = new Mock<NewRequest>();

		var jsonFileReaderMock = new Mock<JsonFileReader>();
		jsonFileReaderMock.Setup(jfr => jfr.ReadJsonFileSelectCheckBoxes()).Returns(
			new CheckBoxCount
			{
				CheckBoxNumbers = new CheckBoxNumbers
				{
					RequestType = (int)RequestType.AuthorizationRequest
				}
			});

		var rows = new ReadOnlyCollection<IWebElement>(new List<IWebElement>());
		newRequestMock.Setup(nr => nr.table).Returns(driverMock.Object.FindElement(By.CssSelector("table.table-selector")));
		newRequestMock.Setup(nr => nr.rows).Returns(rows.ToList());

		var rowsList = rows.ToList();



		var result = DataConsole.ClickRequestType(driverMock.Object);


		Assert.Equal("0", result);
	}

	

	public void Dispose()
	{
	
		driver.Quit();
	
		driver.Dispose();

    }
}


