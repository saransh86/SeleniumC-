using System;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;
using System.IO;

namespace UITests
{
    
   /* [TestFixture(typeof(FirefoxDriver))]
    [TestFixture(typeof(ChromeDriver))]*/

    public class BaseTest 
    {
        protected IWebDriver webDriver;

        [SetUp]
        public void Setup()
        {
            //webDriver = new ChromeDriver("C:\\Users\\saran\\Downloads\\chromedriver_win32");
            //webDriver = new TWebDriver();
            webDriver = new FirefoxDriver(Directory.GetCurrentDirectory() + "\\..\\..\\..\\Drivers");
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            webDriver.Url = "http://localhost:3000";
            webDriver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Close();
        }
    }
}
