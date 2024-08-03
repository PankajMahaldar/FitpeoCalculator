using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Threading;
using OpenQA.Selenium.Interactions;

namespace FitpeoCalculator
{
    public class Tests
    {
        IWebDriver driver = new ChromeDriver();
        [SetUp]
        public void Setup()
        {
             
            String url = "https://www.fitpeo.com";
            //navigate to website
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void FitpeoRevenueCalculator()
        {
            
            //navigate to Revenue Calculator
            
            IList<IWebElement> ls = driver.FindElements(By.XPath(SetOfMenu));
            foreach (IWebElement element in ls)
            {
                if (element.Text.Equals("Revenue Calculator"))
                    element.Click();
            }

            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(ExpectedConditions.ElementIsVisible(By.XPath(SliderSectionXPath)));

            var SliderSection = driver.FindElement(By.XPath(SliderSectionXPath));
            Actions actions = new Actions(driver);
            actions.MoveToElement(SliderSection);
            actions.Perform();

            var PatientsInput = driver.FindElement(By.XPath(PatientsInputXPath));
            PatientsInput.Click();
            Thread.Sleep(2000);
            PatientsInput.SendKeys(Keys.Backspace);
            PatientsInput.SendKeys(Keys.Backspace);
            PatientsInput.SendKeys(Keys.Backspace);
            Thread.Sleep(4000);
            PatientsInput.SendKeys("560");
            Thread.Sleep(4000);

            Assert.AreEqual(Int32.Parse(PatientsInput.GetAttribute("value")), 560);


            PatientsInput.Click();
            Thread.Sleep(2000);
            PatientsInput.SendKeys(Keys.Backspace);
            PatientsInput.SendKeys(Keys.Backspace);
            PatientsInput.SendKeys(Keys.Backspace);
            Thread.Sleep(4000);
            PatientsInput.SendKeys("824");
            Thread.Sleep(4000);
            int value= Int32.Parse(driver.FindElement(By.XPath(PatientsInputXPath)).GetAttribute("value"));
            if (value > 820)
            {
                for (int i = 1; i <= value-820; i++)
                {
                    SliderSection.SendKeys(Keys.ArrowLeft);
                }
            }
            else
            {
                for (int i = 1; i <= 820- value; i++)
                {
                    SliderSection.SendKeys(Keys.ArrowRight);
                }
            }
            


            var CPT_99091 = driver.FindElement(By.XPath("//div[contains(@class,'MuiBox')]/p[contains(text(),'CPT-99091')]"));
            actions.MoveToElement(CPT_99091);
            actions.Perform();
            driver.FindElement(By.XPath(CheckboxXPath+"[1]")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath(CheckboxXPath+"[2]")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath(CheckboxXPath+"[3]")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath(CheckboxXPath+"[8]")).Click();
            Thread.Sleep(2000);
            string ExpectedText = "Total Recurring Reimbursement for all Patients Per Month:\r\n$110700";
            string ActualText = driver.FindElement(By.XPath(ReimbursementXPath)).Text;
            Thread.Sleep(2000);
            Assert.That(ActualText.Contains("$110700"));
        }

        
        public static readonly String SetOfMenu = "//a[@style='text-decoration:none']/div[contains(@class, 'satoshi')]";
        public static readonly String SliderSectionXPath = "//input[@aria-orientation='horizontal']";
        public static readonly String PatientsInputXPath = "//input[contains(@class,'MuiInputBase-input')]";
        public static readonly String CheckboxXPath = "(//input[contains(@class,'PrivateSwitchBase-input')])";
        public static readonly String ReimbursementXPath = "//p[contains(text(),'Total Recurring Reimbursement for all Patients Per Month:')]";

    [TearDown]
        public void TearDown()
        {
           
            driver.Quit();

        }
    }
}