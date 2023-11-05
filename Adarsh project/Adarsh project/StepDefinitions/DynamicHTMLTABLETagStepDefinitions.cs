using Adarsh_project.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Adarsh_project.StepDefinitions
{
    [Binding]
    public class DynamicHTMLTABLETagStepDefinitions
    {
        //public IWebDriver driver = Hook.driver;
        public static IWebDriver driver;
        public List<Dictionary<string, object>> jsonData;
        public List<DataClass> jsonDataObject;

        [StepDefinition(@"I navigate to the launch browaer")]
        public void GivenINavigateToTheLaunchBrowaer()
        {
            driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory);
            driver.Manage().Window.Maximize();
        }

        [StepDefinition(@"I navigated to the Demo web page")]
        public void WhenINavigatedToTheDemoWebPage()
        {
            driver.Navigate().GoToUrl("https://testpages.herokuapp.com/styled/tag/dynamic-table.html");
        }

        [StepDefinition(@"I click on Table Data button for text box")]
        public void WhenIClickOnTableDataButtonForTextBox()
        {

            driver.FindElement(By.XPath("//summary")).Click();
        }

        [StepDefinition(@"I enter the given data in the text box")]
        public void WhenIEnterTheGivenDataInTheTextBox()
        {
            string jsonData = File.ReadAllText(@"../../../testData.json");
            jsonDataObject = JsonConvert.DeserializeObject<List<DataClass>>(jsonData);



            var TextBox = driver.FindElement(By.Id("jsondata"));
            TextBox.Clear();
            TextBox.SendKeys(jsonData);

        }

        [StepDefinition(@"I click on refresh button")]
        public void WhenIClickOnRefreshButton()
        {
            driver.FindElement(By.Id("refreshtable")).Click();
        }

        [StepDefinition(@"entered data should be populated in the table")]
        public void ThenEnteredDataWillBePopulatedInTheTable()
        {
            var table = driver.FindElement(By.Id("dynamictable"));
            var tableRows = table.FindElements(By.TagName("tr"));

            var tableData = new List<Dictionary<string, object>>();
            int totalAsserted = 0;

            foreach (var row in tableRows)
            {
                var columns = row.FindElements(By.TagName("td"));
                if (columns.Count == 0)
                {
                    continue;
                }
                var name = columns[0].Text;
                var age = int.Parse(columns[1].Text);
                var gender = columns[2].Text;
                var expecteRow = new Dictionary<string, object> { { "name", name }, { "age", age }, { "gender", gender } };
                tableData.Add(expecteRow);



                foreach (var obj in jsonDataObject)
                {
                    if (obj.name == name)
                    {
                        Assert.True(obj.name == name);
                        Assert.True(obj.age == age);
                        Assert.True(obj.gender == gender);
                        totalAsserted++;
                        break;
                    }
                }
            }
            Assert.True(tableData.Count == jsonDataObject.Count && tableData.Count == totalAsserted);

        }

        [AfterScenario]
        public void Teardown()
        {
            driver.Quit();
        }



    }
}
