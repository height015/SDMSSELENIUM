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
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace RunUnitTest;

public class DictionaryTest : IDisposable
{
    private readonly IWebDriver driver;

    private readonly LoginMain loginObj;

	private readonly DataConsole data;

	private static readonly string _URL = "http://197.255.51.104:9035";


    public DictionaryTest()
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
	public void Test_ClickRequestType_Success()
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

	[Fact]
	public void Test_RequestInfBox_Success()
	{
		// Arrange
		var driverMock = new Mock<IWebDriver>();
		var jsonFileReaderMock = new Mock<JsonFileReader>();
		var createSecMock = new Mock<NewRequest>();


		var loginVal = new CheckBoxCount();

		jsonFileReaderMock.Setup(jfr => jfr.ReadJsonFileSelectCheckBoxes()).Returns(loginVal);


		var RequestInforVal = new Request();

		jsonFileReaderMock.Setup(jfr => jfr.ReadJsonFileForSelectCheckBoxesProcessNewRequest()).Returns(new Request
		{
			RequestInformation = new RequestInformation
			{
				Title = "Selenium Test",
				Reason = "Selenium Test",
			}
		}); 

		var datconsole = new DataConsole();

		var result = datconsole.RequestInfBox(driverMock.Object);

		// Assert
		Assert.False(result); 
	}

	[Fact]
	public void Test_CategoryRequestInfBox_Success()
	{
		// Arrange
		var driverMock = new Mock<IWebDriver>();
		var jsonFileReaderMock = new Mock<JsonFileReader>();
		var createSecMock = new Mock<NewRequest>();

		var loginVal = new CheckBoxCount();
		jsonFileReaderMock.Setup(jfr => jfr.ReadJsonFileSelectCheckBoxes()).Returns(loginVal);

		var RequestInforVal = new CatRequest(); 
		jsonFileReaderMock.Setup(jfr => jfr.ReadJsonFileForSelectCheckBoxesProcessCatNewRequest()).Returns(RequestInforVal);

		var dataConsole = new DataConsole();

		// Act
		var result = dataConsole.CategoryRequestInfBox(driverMock.Object);

		// Assert
		Assert.False(result); 
	}

	[Fact]
	public void Test_CreateNew_DataSector_Failed()
	{
		// Arrange
		var driverMock = new Mock<IWebDriver>();
		var createSecMock = new Mock<CreateDataSector>();
		var textMsgResMock = new Mock<IWebElement>();

		textMsgResMock.SetupGet(element => element.Text).Returns("This is an error message");

		driverMock.Setup(driver => driver.FindElement(By.LinkText("Datasets"))).Returns((IWebElement)null); 
		driverMock.Setup(driver => driver.FindElement(By.CssSelector("input[name='SectorName']"))).Returns(textMsgResMock.Object); 
		driverMock.Setup(driver => driver.FindElement(By.CssSelector("input[name='SectorTitle']"))).Returns(textMsgResMock.Object); 

		createSecMock.Setup(cs => cs.ClickNew());
		createSecMock.Setup(cs => cs.ClickSubmit());
		createSecMock.Setup(cs => cs.textMsgRes).Returns(textMsgResMock.Object);
		createSecMock.Setup(cs => cs.ClickOk());
		createSecMock.Setup(cs => cs.ClickClose());

		var jsonFileReaderMock = new Mock<JsonFileReader>();
		jsonFileReaderMock.Setup(jfr => jfr.ReadJsonFileCreateSectorFailed()).Returns(new DataSectorEmpty
		{
			SectorFieldEmpty = new SectorFieldEmpty
			{
				Name = null, 
				Title = "" 
			}
		});

		
		// Act
		var result = DataConsole.CreateNewDataSectorFailed(driverMock.Object);

		// Assert
		Assert.Equal("", result);
	}

	[Fact]
	public void Test_ClickData_CatalogCard_Success()
	{
		var driverMock = new Mock<IWebDriver>();
		var cartCardMock = new Mock<IWebElement>();

		driverMock.Setup(driver => driver.FindElement(By.LinkText("Data Catalogues."))).Returns(cartCardMock.Object);
		cartCardMock.Setup(card => card.Click());


		// Act
		DataConsole.ClickDataCatalogCard(driverMock.Object);

		// Assert
		cartCardMock.Verify(card => card.Click(), Times.Once);
	}

	[Fact]
	public void TestClick_CategoryCard_Success()
	{
		// Arrange
		var driverMock = new Mock<IWebDriver>();
		var cartCardMock = new Mock<IWebElement>();
		var selectElementMock = new Mock<SelectElement>();

		driverMock.Setup(driver => driver.FindElement(By.LinkText("Categories"))).Returns(cartCardMock.Object);
		cartCardMock.Setup(card => card.Click());

		//driverMock.Setup(driver => new CategorySector(driverMock.Object)).Returns(new CategorySector(driverMock.Object));
		//driverMock.Setup(driver => new SelectElement(It.IsAny<IWebElement>())).Returns(selectElementMock.Object);

		var jsonFileReaderMock = new Mock<JsonFileReader>();
		jsonFileReaderMock.Setup(jfr => jfr.ReadJsonFileForSelectOptionCatalogSelector()).Returns(new CatalogueContainer 
		{
			CatalogueSelector = new CatalogueSelector 
			{
				OptionToSelect = 1 
			}
		});

		// Act
		DataConsole.ClickCategoryCard(driverMock.Object);

		// Assert
		cartCardMock.Verify(card => card.Click(), Times.Once);
		//selectElementMock.Verify(dropdown => dropdown.SelectByIndex(0), Times.Once); 
	
	}

	[Fact]
	public void Test_ClickNew_DataCategoryButton_Success()
	{
		// Arrange
		var driverMock = new Mock<IWebDriver>();
		var newDataCategoryButtonMock = new Mock<IWebElement>();
		var catSecMock = new Mock<CategorySector>();
		var jsonFileReaderMock = new Mock<JsonFileReader>();

		driverMock.Setup(driver => driver.FindElement(By.CssSelector("a.item-button[href*='/dataset/categories/add']"))).Returns(newDataCategoryButtonMock.Object);
		newDataCategoryButtonMock.Setup(button => button.Click());

		//driverMock.Setup(driver => new CategorySector(driverMock.Object)).Returns(catSecMock.Object);

		jsonFileReaderMock.Setup(jfr => jfr.ReadJsonFileForEnterNewDataCategory())
			.Returns(new DataCategoryContainer
		{
			DataCategory = new DataCategory
			{
				Name = "CategoryName",
				Title = "CategoryTitle"
			}
		});

		// Act
		var result = DataConsole.ClickNewDataCategoryButton(driverMock.Object);

		// Assert
		newDataCategoryButtonMock.Verify(button => button.Click(), Times.Once);
																									  
		Assert.False(result);
	}


	[Fact]
	public void Test_ClickTable_Card_Success()
	{
		// Arrange
		var driverMock = new Mock<IWebDriver>();
		var elementMock = new Mock<IWebElement>();

		driverMock.Setup(driver => driver.FindElement(By.LinkText("Tables"))).Returns(elementMock.Object);

		var yourService = new DataConsole();

		// Act
		DataConsole.ClickTableCard(driverMock.Object);

		// Assert
		driverMock.Verify(driver => driver.FindElement(By.LinkText("Tables")), Times.Once);

		elementMock.Verify(element => element.Click(), Times.Once);
	}

	[Fact]
	public void TestClickIndicators()
	{
		// Arrange
		var driverMock = new Mock<IWebDriver>();

		driverMock.Setup(driver => driver.FindElement(By.LinkText("Indicators"))).Returns(Mock.Of<IWebElement>());

		DataConsole.ClickIndicators(driverMock.Object);

		driverMock.Verify(driver => driver.FindElement(By.LinkText("Indicators")), Times.Once);

	}

	public void Dispose()
	{
		driver.Quit();
		driver.Dispose();

    }
}


