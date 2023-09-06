using System;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace LoginUiTest;

public class JsonFileReader
{

    #region Login Portal

    public LoginParameter ReadJsonFileSuccesLogin()
    {
        try
        {

            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo projectRoot = Directory.GetParent(currentDirectory).Parent.Parent.Parent;
            string jsonFileName = "JData.json";
            string jsonFilePath = Path.Combine(projectRoot.FullName, jsonFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);

                LoginParameter retVal = JsonConvert.DeserializeObject<LoginParameter>(jsonContent);

                return retVal;
            }


            return new LoginParameter();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new LoginParameter();
        }
    }

    public LoginFailedParameter ReadJsonFileWrongLoginCredential()
    {
        try
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string fileName = Path.Combine(baseDirectory, "TestData.json");

            //string fileName = "TestData.json";

            var jsonContent = File.ReadAllText(fileName);

            LoginFailedParameter retVal = JsonConvert.DeserializeObject<LoginFailedParameter>(jsonContent);

            return retVal;

        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new LoginFailedParameter();
        }
    }

    public LoginEmptyUserNameParameter ReadJsonFileEmptyUserNameLoginCredential()
    {
        try
        {
            string fileName = "TestData.json";

            var jsonContent = File.ReadAllText(fileName);

            LoginEmptyUserNameParameter retVal = JsonConvert.DeserializeObject<LoginEmptyUserNameParameter>(jsonContent);

            return retVal;

        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new LoginEmptyUserNameParameter();
        }
    }

    #endregion


    public DataSector ReadJsonFileCreateSector()
    {
        try
        {

            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo projectRoot = Directory.GetParent(currentDirectory).Parent.Parent.Parent;
            string jsonFileName = "JData.json";
            string jsonFilePath = Path.Combine(projectRoot.FullName, jsonFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);

                DataSector retVal = JsonConvert.DeserializeObject<DataSector>(jsonContent);

                return retVal;
            }


            return new DataSector();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new DataSector();
        }
    }

    public DataSectorEmpty ReadJsonFileCreateSectorFailed()
    {
        try
        {

            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo projectRoot = Directory.GetParent(currentDirectory).Parent.Parent.Parent;
            string jsonFileName = "JData.json";
            string jsonFilePath = Path.Combine(projectRoot.FullName, jsonFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);

                DataSectorEmpty retVal = JsonConvert.DeserializeObject<DataSectorEmpty>(jsonContent);

                return retVal;
            }


            return new DataSectorEmpty();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new DataSectorEmpty();
        }
    }

    public CheckBoxCount ReadJsonFileSelectCheckBoxes()
    {
        try
        {

            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo projectRoot = Directory.GetParent(currentDirectory).Parent.Parent.Parent;
            string jsonFileName = "JData.json";
            string jsonFilePath = Path.Combine(projectRoot.FullName, jsonFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);

                CheckBoxCount retVal = JsonConvert.DeserializeObject<CheckBoxCount>(jsonContent);

                return retVal;
            }


            return new CheckBoxCount();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new CheckBoxCount();
        }
    }

    public Request ReadJsonFileForSelectCheckBoxesProcessNewRequest()
    {
        try
        {

            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo projectRoot = Directory.GetParent(currentDirectory).Parent.Parent.Parent;
            string jsonFileName = "JData.json";
            string jsonFilePath = Path.Combine(projectRoot.FullName, jsonFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);

                Request retVal = JsonConvert.DeserializeObject<Request>(jsonContent);

                return retVal;
            }


            return new Request();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new Request();
        }
    }


	public CatRequest ReadJsonFileForSelectCheckBoxesProcessCatNewRequest()
	{
		try
		{

			string currentDirectory = Directory.GetCurrentDirectory();
			DirectoryInfo projectRoot = Directory.GetParent(currentDirectory).Parent.Parent.Parent;
			string jsonFileName = "JData.json";
			string jsonFilePath = Path.Combine(projectRoot.FullName, jsonFileName);

			if (File.Exists(jsonFilePath))
			{
				var jsonContent = File.ReadAllText(jsonFilePath);

				CatRequest retVal = JsonConvert.DeserializeObject<CatRequest>(jsonContent);

				return retVal;
			}


			return new CatRequest();
		}
		catch (Exception ex)
		{
			var message = ex.Message;

			return new CatRequest();
		}
	}


	public CatalogueContainer ReadJsonFileForSelectOptionCatalogSelector()
    {
        try
        {

            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo projectRoot = Directory.GetParent(currentDirectory).Parent.Parent.Parent;
            string jsonFileName = "JData.json";
            string jsonFilePath = Path.Combine(projectRoot.FullName, jsonFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);

                CatalogueContainer retVal = JsonConvert.DeserializeObject<CatalogueContainer>(jsonContent);

                return retVal;
            }


            return new CatalogueContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new CatalogueContainer();
        }
    }

    public DataCategoryContainer ReadJsonFileForEnterNewDataCategory()
    {
        try
        {

            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo projectRoot = Directory.GetParent(currentDirectory).Parent.Parent.Parent;
            string jsonFileName = "JData.json";
            string jsonFilePath = Path.Combine(projectRoot.FullName, jsonFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);

                DataCategoryContainer retVal = JsonConvert.DeserializeObject<DataCategoryContainer>(jsonContent);

                return retVal;
            }


            return new DataCategoryContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new DataCategoryContainer();
        }
    }

    public TableDataSelectorContainer ReadJsonFileForTableDataSector()
    {
        try
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo projectRoot = Directory.GetParent(currentDirectory).Parent.Parent.Parent;
            string jsonFileName = "JData.json";
            string jsonFilePath = Path.Combine(projectRoot.FullName, jsonFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);

                TableDataSelectorContainer retVal = JsonConvert.DeserializeObject<TableDataSelectorContainer>(jsonContent);

                return retVal;
            }


            return new TableDataSelectorContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new TableDataSelectorContainer();
        }
    }


    public TableFrequencyContainer ReadJsonFileForTableFrequency()
    {
        try
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo projectRoot = Directory.GetParent(currentDirectory).Parent.Parent.Parent;
            string jsonFileName = "JData.json";
            string jsonFilePath = Path.Combine(projectRoot.FullName, jsonFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);

                TableFrequencyContainer retVal = JsonConvert.DeserializeObject<TableFrequencyContainer>(jsonContent);

                return retVal;
            }


            return new TableFrequencyContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new TableFrequencyContainer();
        }
    }

    public TableUnitContainer ReadJsonFileForTableUnit()
    {
        try
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo projectRoot = Directory.GetParent(currentDirectory).Parent.Parent.Parent;
            string jsonFileName = "JData.json";
            string jsonFilePath = Path.Combine(projectRoot.FullName, jsonFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);

                TableUnitContainer retVal = JsonConvert.DeserializeObject<TableUnitContainer>(jsonContent);

                return retVal;
            }


            return new TableUnitContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new TableUnitContainer();
        }
    }


    public TableNewDataContainer ReadJsonFileForNewDataTable()
    {
        try
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo projectRoot = Directory.GetParent(currentDirectory).Parent.Parent.Parent;
            string jsonFileName = "JData.json";
            string jsonFilePath = Path.Combine(projectRoot.FullName, jsonFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                TableNewDataContainer retVal = JsonConvert.DeserializeObject<TableNewDataContainer>(jsonContent);
                return retVal;
            }

            return new TableNewDataContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new TableNewDataContainer();
        }
    }

	public IndicatorRequestDataContainer ReadJsonFileForNewRequestIndicator()
	{
		try
		{
			string currentDirectory = Directory.GetCurrentDirectory();
			DirectoryInfo projectRoot = Directory.GetParent(currentDirectory).Parent.Parent.Parent;
			string jsonFileName = "JData.json";
			string jsonFilePath = Path.Combine(projectRoot.FullName, jsonFileName);

			if (File.Exists(jsonFilePath))
			{
				var jsonContent = File.ReadAllText(jsonFilePath);
				IndicatorRequestDataContainer retVal = JsonConvert.DeserializeObject<IndicatorRequestDataContainer>(jsonContent);
				return retVal;
			}

			return new IndicatorRequestDataContainer();
		}
		catch (Exception ex)
		{
			var message = ex.Message;

			return new IndicatorRequestDataContainer();
		}
	}

	public TableRequestDataContainer ReadJsonFileForNewRequestTable()
	{
		try
		{
			string currentDirectory = Directory.GetCurrentDirectory();
			DirectoryInfo projectRoot = Directory.GetParent(currentDirectory).Parent.Parent.Parent;
			string jsonFileName = "JData.json";
			string jsonFilePath = Path.Combine(projectRoot.FullName, jsonFileName);

			if (File.Exists(jsonFilePath))
			{
				var jsonContent = File.ReadAllText(jsonFilePath);
				TableRequestDataContainer retVal = JsonConvert.DeserializeObject<TableRequestDataContainer>(jsonContent);
				return retVal;
			}

			return new TableRequestDataContainer();
		}
		catch (Exception ex)
		{
			var message = ex.Message;

			return new TableRequestDataContainer();
		}
	}

	public DataTableSettingContainer ReadJsonFileNewDataTableSettings()
    {
        try
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo projectRoot = Directory.GetParent(currentDirectory).Parent.Parent.Parent;
            string jsonFileName = "JData.json";
            string jsonFilePath = Path.Combine(projectRoot.FullName, jsonFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                DataTableSettingContainer retVal = JsonConvert.DeserializeObject<DataTableSettingContainer>(jsonContent);
                return retVal;
            }

            return new DataTableSettingContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new DataTableSettingContainer();
        }
    }

    public DataTableTxtValContainer ReadJsonFileDataTableTxt()
    {
        try
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo projectRoot = Directory.GetParent(currentDirectory).Parent.Parent.Parent;
            string jsonFileName = "JData.json";
            string jsonFilePath = Path.Combine(projectRoot.FullName, jsonFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                DataTableTxtValContainer retVal = JsonConvert.DeserializeObject<DataTableTxtValContainer>(jsonContent);
                return retVal;
            }

            return new DataTableTxtValContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new DataTableTxtValContainer();
        }
    }

    public DataFrequencyContainer ReadJsonFileDataFrequency()
    {
        try
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo projectRoot = Directory.GetParent(currentDirectory).Parent.Parent.Parent;
            string jsonFileName = "JData.json";
            string jsonFilePath = Path.Combine(projectRoot.FullName, jsonFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                DataFrequencyContainer retVal = JsonConvert.DeserializeObject<DataFrequencyContainer>(jsonContent);
                return retVal;
            }

            return new DataFrequencyContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new DataFrequencyContainer();
        }
    }

    public DataUnitContainer ReadJsonFileDataUnit()
    {
        try
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo projectRoot = Directory.GetParent(currentDirectory).Parent.Parent.Parent;
            string jsonFileName = "JData.json";
            string jsonFilePath = Path.Combine(projectRoot.FullName, jsonFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                DataUnitContainer retVal = JsonConvert.DeserializeObject<DataUnitContainer>(jsonContent);
                return retVal;
            }

            return new DataUnitContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new DataUnitContainer();
        }
    }
    public DataEntitiesContainer ReadJsonFileDataEntity()
    {
        try
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo projectRoot = Directory.GetParent(currentDirectory).Parent.Parent.Parent;
            string jsonFileName = "JData.json";
            string jsonFilePath = Path.Combine(projectRoot.FullName, jsonFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                DataEntitiesContainer retVal = JsonConvert.DeserializeObject<DataEntitiesContainer>(jsonContent);
                return retVal;
            }

            return new DataEntitiesContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new DataEntitiesContainer();
        }
    }
    public DataSourceContainer ReadJsonFileDataSource()
    {
        try
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo projectRoot = Directory.GetParent(currentDirectory).Parent.Parent.Parent;
            string jsonFileName = "JData.json";
            string jsonFilePath = Path.Combine(projectRoot.FullName, jsonFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                DataSourceContainer retVal = JsonConvert.DeserializeObject<DataSourceContainer>(jsonContent);
                return retVal;
            }

            return new DataSourceContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new DataSourceContainer();
        }
    }
    public IndicatorDataSelectorContainer ReadJsonFileDataIndicator()
    {
        try
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo projectRoot = Directory.GetParent(currentDirectory).Parent.Parent.Parent;
            string jsonFileName = "JData.json";
            string jsonFilePath = Path.Combine(projectRoot.FullName, jsonFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                IndicatorDataSelectorContainer retVal = JsonConvert.DeserializeObject<IndicatorDataSelectorContainer>(jsonContent);
                return retVal;
            }

            return new IndicatorDataSelectorContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new IndicatorDataSelectorContainer();
        }
    }

    public NewDataIndicatorContainer ReadJsonFileNewDataIndicator()
    {
        try
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo projectRoot = Directory.GetParent(currentDirectory).Parent.Parent.Parent;
            string jsonFileName = "JData.json";
            string jsonFilePath = Path.Combine(projectRoot.FullName, jsonFileName);

            if (File.Exists(jsonFilePath))
            {
                var jsonContent = File.ReadAllText(jsonFilePath);
                NewDataIndicatorContainer retVal = JsonConvert.DeserializeObject<NewDataIndicatorContainer>(jsonContent);
                return retVal;
            }

            return new NewDataIndicatorContainer();
        }
        catch (Exception ex)
        {
            var message = ex.Message;

            return new NewDataIndicatorContainer();
        }
    }


	public WorkFlowSelectionContaner ReadJsonFileWorkFlowSelection()
	{
		try
		{
			string currentDirectory = Directory.GetCurrentDirectory();
			DirectoryInfo projectRoot = Directory.GetParent(currentDirectory).Parent.Parent.Parent;
			string jsonFileName = "JData.json";
			string jsonFilePath = Path.Combine(projectRoot.FullName, jsonFileName);

			if (File.Exists(jsonFilePath))
			{
				var jsonContent = File.ReadAllText(jsonFilePath);
				WorkFlowSelectionContaner retVal = JsonConvert.DeserializeObject<WorkFlowSelectionContaner>(jsonContent);
				return retVal;
			}

			return new WorkFlowSelectionContaner();
		}
		catch (Exception ex)
		{
			var message = ex.Message;

			return new WorkFlowSelectionContaner();
		}
	}
	public ReviewSelectionContaner ReadJsonFileWorkFlowReview()
	{
		try
		{
			string currentDirectory = Directory.GetCurrentDirectory();
			DirectoryInfo projectRoot = Directory.GetParent(currentDirectory).Parent.Parent.Parent;
			string jsonFileName = "JData.json";
			string jsonFilePath = Path.Combine(projectRoot.FullName, jsonFileName);

			if (File.Exists(jsonFilePath))
			{
				var jsonContent = File.ReadAllText(jsonFilePath);
				ReviewSelectionContaner retVal = JsonConvert.DeserializeObject<ReviewSelectionContaner>(jsonContent);
				return retVal;
			}

			return new ReviewSelectionContaner();
		}
		catch (Exception ex)
		{
			var message = ex.Message;

			return new ReviewSelectionContaner();
		}
	}

	public AnalyticsDataSectorContainer ReadJsonCMSAnalytis()
	{
		try
		{
			string currentDirectory = Directory.GetCurrentDirectory();
			DirectoryInfo projectRoot = Directory.GetParent(currentDirectory).Parent.Parent.Parent;
			string jsonFileName = "JData.json";
			string jsonFilePath = Path.Combine(projectRoot.FullName, jsonFileName);

			if (File.Exists(jsonFilePath))
			{
				var jsonContent = File.ReadAllText(jsonFilePath);
				AnalyticsDataSectorContainer retVal = JsonConvert.DeserializeObject<AnalyticsDataSectorContainer>(jsonContent);
				return retVal;
			}
			return new AnalyticsDataSectorContainer();
		}
		catch (Exception ex)
		{
			var message = ex.Message;
			return new AnalyticsDataSectorContainer();
		}

	}


}


//LoginFailedTest
#region Portal Login
#region Success Login Object

public class LoginParameters
{
    public string Username { get; set; }
    public string Password { get; set; }

}

public class LoginParameter
{
    public LoginParameters LoginParameters { get; set; }

}
#endregion

#region Wrong Credentails Login Object

public class LoginFailedTest
{
    public string Username { get; set; }
    public string Password { get; set; }

}

public class LoginFailedParameter
{
    public LoginFailedTest LoginFailedTest { get; set; }
}
#endregion

#region Wrong Credentails Empty UserName Fields

public class LoginEmptyUserName
{
    public string Username { get; set; }
    public string Password { get; set; }

}

public class LoginEmptyUserNameParameter
{
    public LoginEmptyUserName LoginEmptyUserName { get; set; }
}
#endregion

#endregion


#region New Sector Data Creation

public class SectorField
{
    public string Name { get; set; }
    public string Title { get; set; }

}

public class DataSector
{
    public SectorField SectorField { get; set; }

}

public class SectorFieldEmpty
{
    public string Name { get; set; }
    public string Title { get; set; }

}

public class DataSectorEmpty
{
    public SectorFieldEmpty SectorFieldEmpty { get; set; }

}

#endregion


#region New Request

public class CheckBoxNumbers
{
    public string Index { get; set; }
    public int RequestType { get; set; }


	public int[] GetIndexArray()
	{
		return Index?.Split(',').Select(int.Parse).ToArray() ?? new int[0];
	}
}

public class CheckBoxCount
{
    public CheckBoxNumbers CheckBoxNumbers { get; set; }

}

public class RequestInformation
{
    public string Title { get; set; }
    public string Reason { get; set; }

}

public class Request
{
    public RequestInformation RequestInformation { get; set; }

}


public class CatRequestInformation
{
	public string Title { get; set; }
	public string Reason { get; set; }

}

public class CatRequest
{
	public CatRequestInformation CatRequestInformation { get; set; }

}


#endregion

#region Cataloguo

public class CatalogueSelector
{
    public int OptionToSelect { get; set; }

}
public class CatalogueContainer
{
    public CatalogueSelector CatalogueSelector { get; set; }
}

public class CatalogueSelectorValue
{
    public int value { get; set; }
}

public class CatalogueSelectorValueContainer
{
    public CatalogueSelectorValue CatalogueSelectorValue { get; set; }
}

#endregion

#region Category

public class DataCategory
{
    public string Name { get; set; }
    public string Title { get; set; }

}

public class DataCategoryContainer
{
    public DataCategory DataCategory { get; set; }

}

#endregion
/// <summary>
/// Table
/// </summary>

#region Table
public class TableDataSelector
{
    public int OptionToSelect { get; set; }
}

public class TableDataSelectorContainer
{
    public TableDataSelector TableDataSelector { get; set; }
}

public class TableNewData
{
    public string Name { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

}

public class TableNewDataContainer
{
    public TableNewData TableNewData { get; set; }
}

public class TableRequestData
{
	public string Title { get; set; }
	public string Reason { get; set; }


}
public class TableRequestDataContainer
{
    public TableRequestData TableRequestData { get; set; }
}
public class TableFrequency
{
    public int OptionToSelect { get; set; }
}

public class TableFrequencyContainer
{
    public TableFrequency TableFrequency { get; set; }
}

public class TableUnit
{
    public int OptionToSelect { get; set; }
}

public class TableUnitContainer
{
    public TableUnit TableUnit { get; set; }
}

public class DataTableSetting
{
    public bool Data1 { get; set; }
    public bool Data2 { get; set; }
    public bool Data3 { get; set; }
    public bool Data4 { get; set; }
    public bool Data5 { get; set; }

}

public class DataTableSettingContainer
{
    public DataTableSetting DataTableSetting { get; set; }

}


public class DataTableTxtVal
{
    public string Txt1 { get; set; }
    public string Txt2 { get; set; }
    public string Txt3 { get; set; }
    public string Txt4 { get; set; }
    public string Txt5 { get; set; }

}

public class DataTableTxtValContainer
{
    public DataTableTxtVal DataTableTxtVal { get; set; }
}
#endregion


#region  Indicators
public class IndicatorRequestData
{
    public string Title { get; set; }
    public string Reason { get; set; }
}

public class IndicatorRequestDataContainer
{
    public IndicatorRequestData IndicatorRequestData { get; set; }
} 
#endregion

//Frequncy

#region Frequency
public class DataFrequency
{
    public string Name { get; set; }
    public string ShortName { get; set; }
}

public class DataFrequencyContainer
{
    public DataFrequency DataFrequency { get; set; }
}

#endregion

#region Units
public class DataUnit
{
    public string Name { get; set; }
    public string ShortName { get; set; }
}

public class DataUnitContainer
{
    public DataUnit DataUnit { get; set; }
}

#endregion

#region Data Entities
public class DataEntities
{
    public string Name { get; set; }
    public string ShortName { get; set; }
}

public class DataEntitiesContainer
{
    public DataEntities DataEntities { get; set; }
} 
#endregion

#region Data Source
public class DataSource
{
    public string Name { get; set; }
    public string ShortName { get; set; }
}

public class DataSourceContainer
{
    public DataSource DataSource { get; set; }
}


#endregion

#region Indicator PopUp
public class IndicatorDataSelector
{
    public int SectorIndex { get; set; }
    public int CategoryIndex { get; set; }
    public int TableIndex { get; set; }

}

public class IndicatorDataSelectorContainer
{
    public IndicatorDataSelector IndicatorDataSelector { get; set; }
}

#endregion

#region New Data Indicator PopUp

public class NewDataIndicator
{
    public int TopLevelIndi { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }

    public bool EmboldenIndicatorTitle { get; set; }

    public int DisplayOrder { get; set; }

    public bool DisplayInChart { get; set; }

    public string TitleToDisplay { get; set; }

}

public class NewDataIndicatorContainer
{
    public NewDataIndicator NewDataIndicator { get; set; }
}

#endregion


#region Authorization Workflow
public class WorkFlowSelection
{
    public int CategoryIndex { get; set; }
    public int SourceTypeIndex { get; set; }
    public int RoleIndex { get; set; }

}

public class WorkFlowSelectionContaner
{
    public WorkFlowSelection WorkFlowSelection { get; set; }

}

public class ReviewSelection
{
	public string Comment { get; set; }
	public bool Status { get; set; }
}

public class ReviewSelectionContaner
{
	public ReviewSelection ReviewSelection { get; set; }

}




#endregion



#region    CMS Analytics
public class AnalyticsDataSector
{
    public int SectorIndex { get; set; }
    public int CategoryIndex { get; set; }
    public int TableIndex { get; set; }
    public string StartDate { get; set; }
    public string StopDate { get; set; }
	public string IndicatorsIndexToSelect { get; set; }


	public string Name { get; set; }
	public string Title { get; set; }
	public string SeriesTitle { get; set; }

    public int ChartTypeIndex { get; set; }
    public int ContentSpotIndex { get; set; }

    public string Note { get; set; }

    public int[] GetIndexArray()
    {
        return IndicatorsIndexToSelect?.Split(',').Select(int.Parse).ToArray() ?? new int[0];
    }

}

public class AnalyticsDataSectorContainer
{
    public AnalyticsDataSector AnalyticsDataSector { get; set; }
} 
#endregion