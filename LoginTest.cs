using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UITests.PageObjects;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace UITests
{

    
    public class LoginTest : BaseTest
    {
        
        [Test]
        public void TestValidLogin()
            {

                LoginPage loginPage = new LoginPage(webDriver);
                loginPage.login("richard@piedpiper.com", "password");

                HomePage homePage = new HomePage(webDriver);
                Assert.AreEqual("Richard!", homePage.getLoggedInUsername());

                homePage.logout();
                Assert.IsTrue(loginPage.isLoginPage());
            
            }
        [Test]
        public void TestInValigLogin()
        {
            LoginPage loginPage = new LoginPage(webDriver);
            loginPage.login("richard@piedPiper.com", "password12");

            Assert.AreEqual(" There is no registered account with the given email or password provided is not valid.", loginPage.getError().Text);
        }
        
        [Test]
        public void TestLoginWithNoUsername()
        {
            
            LoginPage loginPage = new LoginPage(webDriver);
            loginPage.sendKeys("password");

            Assert.IsFalse(loginPage.isLoginButtonEnabled());
            
        }
        [Test]
        public void TestLoggedInUser()
        {
            LoginPage loginPage = new LoginPage(webDriver);
            loginPage.login("richard@piedpiper.com", "password");

            HomePage homePage = new HomePage(webDriver);
            Assert.AreEqual("Richard!", homePage.getLoggedInUsername());

            /**
             * Clear local storage
             */
            loginPage.clearLocalStorage();
            webDriver.Navigate().Refresh();
            Assert.IsTrue(loginPage.isLoginPage());
        }
    }
}