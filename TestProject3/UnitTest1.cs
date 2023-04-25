using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Data;
using System.Threading;

namespace TestProject3
{
    [TestClass]
    public class UnitTest1
    {
        IWebDriver driver;


        [TestInitialize()]
        public void Initialize()
        {
            driver = new ChromeDriver();
            
            driver.Manage().Window.Maximize();
            
        }

        [TestCleanup()]
        public void Cleanup()
        {
            driver.Dispose();//zamyka wszystkie okna aktualnie otwarte przez WebDriver'a i bezpiecznie koñczy sesjê.
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }



        [TestMethod]
        public void DriverLevelInterrogation()
        {
            driver.Navigate().GoToUrl("https://ultimateqa.com/automation/");
            var currentWindow = driver.CurrentWindowHandle; // TODO: Dodaæ Asercje na koñcu testu
            var windows = driver.WindowHandles;
            var pageSourse = driver.PageSource;
            var title = driver.Title;
            var url = driver.Url;
        }

        [TestMethod]
        public void WebElementInterrogation()
        {
            driver.Navigate().GoToUrl("https://ultimateqa.com/automation/");
            IWebElement LinkElement = driver.FindElement(By.XPath("//*[@href='http://courses.ultimateqa.com/users/sign_in']"));
            var x = LinkElement.GetAttribute("href");
            var y = LinkElement.GetCssValue("margin");
            var z = LinkElement.TagName;


        }
        [TestMethod]
        public void WebElementAssertion()
        {
            driver.Navigate().GoToUrl("https://courses.ultimateqa.com/users/sign_in");
            IWebElement LinkElement = driver.FindElement(By.XPath("//button[@type='submit']"));
            int x = 2;
            int y = 4;
            int z = 5;   
            Assert.AreEqual("submit", LinkElement.GetAttribute("type"));
            Assert.AreEqual("normal", LinkElement.GetCssValue("letter-spacing"));
            Assert.IsTrue(LinkElement.Displayed);
            Assert.IsTrue(LinkElement.Enabled);
            Assert.IsFalse(LinkElement.Selected);
            Assert.AreEqual(LinkElement.Text, "Sign in");
            Assert.AreEqual("button", LinkElement.TagName);
            //Assert.AreEqual(270, LinkElement.Location.X);
            //Assert.AreEqual(500, LinkElement.Location.Y);
            Assert.IsTrue(z > x);
        }



        [TestMethod]
        public void TestWithSimpleSelectors()
        {
            driver.Navigate().GoToUrl("https://ultimateqa.com/automation/");
       
            IWebElement LinkToFillOutForm = driver.FindElement(By.LinkText("Fill out forms"));
            LinkToFillOutForm.Click();

            IWebElement InputNameWithCaptcha = driver.FindElement(By.Id("et_pb_contact_name_1"));
            InputNameWithCaptcha.SendKeys("Jan Kowalski");

            string TitleOfThisPage = driver.Title;
            IWebElement InputMessageWithCaptcha = driver.FindElement(By.Id("et_pb_contact_message_1"));
            InputMessageWithCaptcha.SendKeys(TitleOfThisPage);

            IWebElement InputCaptcha = driver.FindElement(By.Name("et_pb_contact_captcha_1"));

            IWebElement Captcha = driver.FindElement(By.ClassName("et_pb_contact_captcha_question"));

            
            string CpatchaResult = GetCaptchaResult(Captcha).ToString();
            InputCaptcha.SendKeys(CpatchaResult);
            Thread.Sleep(2000);
            var ButtonsForCaptchaForm = driver.FindElements(By.Name("et_builder_submit_button"));
            ButtonsForCaptchaForm[1].Click();
            Thread.Sleep(3000);
         
            IWebElement ContactMessage = driver.FindElement(By.CssSelector("#et_pb_contact_form_1 > div"));
            string ThankYouMessage = ContactMessage.Text;
            Assert.AreEqual("Thanks for contacting us", ThankYouMessage);
        }



        [TestMethod]
        public void BrokenTestWithXPATHSelectors()
        {
            driver.Navigate().GoToUrl("https://ultimateqa.com/automation/");

            IWebElement LinkToFillOutForms = driver.FindElement(By.XPath("//a[normalize-space()='Fill out forms']"));
            LinkToFillOutForms.Click();

            IWebElement InputNameWithCaptcha = driver.FindElement(By.XPath("(//input[@id='et_pb_contact_name_1'])[1]"));
            InputNameWithCaptcha.SendKeys("Jan Kowalski");

            IWebElement InputMessageWithCaptcha = driver.FindElement(By.XPath("(//textarea[@id='et_pb_contact_message_0'])[1]"));
            string TitleOfThisPage = driver.Title;
            InputMessageWithCaptcha.SendKeys(TitleOfThisPage);

            IWebElement captcha = driver.FindElement(By.XPath("//span[@class='et_pb_contact_captcha_question']"));
            string CpatchaResult = GetCaptchaResultXpathTest(captcha).ToString();
            IWebElement InputCaptcha = driver.FindElement(By.XPath("(//input[@name='et_pb_contact_captcha_1'])[1]"));
            InputCaptcha.SendKeys(CpatchaResult);

       
            var ButtonForCaptchaForm = driver.FindElement(By.XPath("(//button[@name='et_builder_submit_button'][normalize-space()='Submit'])[21]"));
            ButtonForCaptchaForm.Click();
            
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("#et_pb_contact_form_1 > div"), "Thanks for contacting us"));

            IWebElement ContactMessage = driver.FindElement(By.XPath("//p[normalize-space()='Thanks for contacting us']"));
            string ThankYouMessage = ContactMessage.Text;
            Assert.AreEqual("Thanks for contacting us", ThankYouMessage);
        }


        [TestMethod]
        public void SimpleSelectorTest()
        {
    
            driver.Navigate().GoToUrl("https://testing.todorvachev.com");

            IWebElement selectorsButton = driver.FindElement(By.Id("menu-item-106"));
            Thread.Sleep(2000);
            selectorsButton.Click();
            Thread.Sleep(2000);


            IWebElement nameImage = driver.FindElement(By.PartialLinkText("Name"));
            Thread.Sleep(2000);
            nameImage.Click();
            Thread.Sleep(2000);


            IWebElement element = driver.FindElement(By.Name("myName"));
            Thread.Sleep(2000);
            element.SendKeys("Wpisujê tekst");
            Thread.Sleep(3000);

       
            driver.Navigate().Back();


            Thread.Sleep(2000);
            driver.Navigate().GoToUrl("https://testing.todorvachev.com/class-name/");
            Thread.Sleep(2000);
           
            Thread.Sleep(2000);

            IWebElement classElement = driver.FindElement(By.ClassName("testClass"));
            string pragraphText = classElement.Text;
            Assert.AreEqual("This is a paragraph with text that belongs to a class.", pragraphText);
        }

        private int GetCaptchaResult(IWebElement captcha)
        {
            var table = new DataTable();
            var captchaAnswer = (int)table.Compute(captcha.Text, "");
            var result = captchaAnswer;
            return result;
        }
        private int GetCaptchaResultXpathTest(IWebElement captcha)
        {
            var table = new DataTable();
            var captchaAnswer = (int)table.Compute(captcha.Text, "");
            var result = 1 + captchaAnswer;
            return result;
        }

    }
}