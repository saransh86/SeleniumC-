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


namespace UITests
{
    class HomePageTest : BaseTest
    {
        [Test]

        public void TestEnterDescriptionWithNoTitle()
        {
            LoginPage loginPage = new LoginPage(webDriver);
            loginPage.login("richard@piedpiper.com", "password");

            HomePage homePage = new HomePage(webDriver);
            Assert.AreEqual("Richard!", homePage.getLoggedInUsername());

            homePage.enterDescription("This is a description without any title!");
            Assert.IsFalse(homePage.isTodoButtonEnabled());
            homePage.logout();
        }

        [Test]

        public void TestEnterTitleWithoutDescription()
        {
            LoginPage loginPage = new LoginPage(webDriver);
            loginPage.login("richard@piedpiper.com", "password");

            HomePage homePage = new HomePage(webDriver);
            Assert.AreEqual("Richard!", homePage.getLoggedInUsername());

            homePage.enterTitle("Need to Watch Silicon Valley!");
       
            Assert.IsTrue(homePage.isTodoButtonEnabled());
            homePage.logout();
        }

        [Test]
        public void TestAddTodoAndTestActiveTabGetsUpdated()
        {
            LoginPage loginPage = new LoginPage(webDriver);
            loginPage.login("richard@piedpiper.com", "password");

            HomePage homePage = new HomePage(webDriver);
            Assert.AreEqual("Richard!", homePage.getLoggedInUsername());
            /*
             * Initially there should be no items
             */
            Assert.AreEqual("No items left to do!", homePage.ItemsLeftTodo.Text);
            /**
             * Add the first todo
             */
            homePage.enterTitle("Need to Watch Silicon Valley!");
            homePage.enterDescription("It was a long shot to build a new internet!");
            homePage.addTodo();
            homePage.waitTillTodoAdded(1);
            
            /**
             * Check if its added
             */
            Assert.AreEqual("1 item left to do!", homePage.ItemsLeftTodo.Text);
            /**
             * Check if the tab is updated
             */
            Assert.AreEqual("All (1)", homePage.AllTab.Text);
            Assert.AreEqual("Active (1)", homePage.ActiveTab.Text);
            Assert.AreEqual("Completed", homePage.CompletedTab.Text);

            /**
             * Go to the active tab and make sure we see the todo
             */
            homePage.ActiveTab.Click();
            Assert.AreEqual(1, homePage.getAllTodosCount());
            /**
             * In completed tab, we should see nothing
             */ 
            homePage.CompletedTab.Click();
            Assert.AreEqual("You have no completed todo items!", homePage.NoTodo.Text);

            /**
             * Clean up before we leave
             */
            homePage.ActiveTab.Click();
            homePage.deleteTodo(homePage.getAllTodosCount());
            homePage.logout();
        }

        [Test]
        public void TestAddTodoAndTestCompletedTabGetUpdated()
        {
            LoginPage loginPage = new LoginPage(webDriver);
            loginPage.login("richard@piedpiper.com", "password");

            HomePage homePage = new HomePage(webDriver);
            Assert.AreEqual("Richard!", homePage.getLoggedInUsername());
            /*
             * Initially there should be no items
             */
            Assert.AreEqual("No items left to do!", homePage.ItemsLeftTodo.Text);
            /**
             * Add the first todo
             */
            homePage.enterTitle("Need to Watch Silicon Valley!");
            homePage.enterDescription("It was a long shot to build a new internet!");
            homePage.addTodo();
            homePage.waitTillTodoAdded(1);
            /**
             * Check if its added
             */
            Assert.AreEqual("1 item left to do!", homePage.ItemsLeftTodo.Text);
            /**
             * Check if the tab is updated
             */
            Assert.AreEqual("All (1)", homePage.AllTab.Text);
            Assert.AreEqual("Active (1)", homePage.ActiveTab.Text);
            Assert.AreEqual("Completed", homePage.CompletedTab.Text);
     
            /**
             * Complete the checkbox
             */
            homePage.completeTodos(homePage.getAllTodosCount());
            Assert.AreEqual("All (1)", homePage.AllTab.Text);
            Assert.AreEqual("Active", homePage.ActiveTab.Text);
            Assert.AreEqual("Completed (1)", homePage.CompletedTab.Text);

            /**
             * Make sure the tabs are correect
             */
            homePage.ActiveTab.Click();
            Assert.AreEqual("You have no active todo items!", homePage.NoTodo.Text);

            homePage.CompletedTab.Click();
            Assert.AreEqual(1, homePage.getAllTodosCount());

            /**
             * Delete em all!
             */
            homePage.deleteTodo(homePage.getAllTodosCount());
            homePage.logout();

        }

        [Test]
        public void TestTodoIsExactlySaved()
        {
            LoginPage loginPage = new LoginPage(webDriver);
            loginPage.login("richard@piedpiper.com", "password");

            HomePage homePage = new HomePage(webDriver);
            Assert.AreEqual("Richard!", homePage.getLoggedInUsername());
            /*
             * Initially there should be no items
             */
            Assert.AreEqual("No items left to do!", homePage.ItemsLeftTodo.Text);
            /**
             * Add Todo's
             */
            String[] title = new String[2];
            String[] description = new string[2];
            title[0] = "Need to Watch Silicon Valley!";
            title[1] = "Google, remind me that JS is the best!";
            description[0] = "It was a long shot to build a new internet!";
            description[1] = "Why does C# hate me.";

            int i = 0;
            while(i != 2)
            {
                homePage.enterTitle(title[i]);
                homePage.enterDescription(description[i]);
                homePage.addTodo();
                homePage.waitTillTodoAdded(i + 1);
                i++;
            }
            /**
             * Check if its added
             */
            Assert.AreEqual("2 items left to do!", homePage.ItemsLeftTodo.Text);
            String[] res = homePage.getAllTodos();
            i = 1;
            foreach(string toDos in res)
            {
                String[] toDo = toDos.Split("\n", 3);
                Assert.AreEqual(title[i], toDo[0].Trim());
                Assert.AreEqual(description[i], toDo[1].Trim());
                Assert.AreEqual(DateTime.Now.ToString("MMMM d, yyyy"), toDo[2].Trim());
                i--;
            }
            /**
            * Delete em all!
            */
            homePage.deleteTodo(homePage.getAllTodosCount());
            homePage.logout();
        }

        [Test]
        public void TestAdd2TodoandDelete1()
        {
            LoginPage loginPage = new LoginPage(webDriver);
            loginPage.login("richard@piedpiper.com", "password");

            HomePage homePage = new HomePage(webDriver);
            Assert.AreEqual("Richard!", homePage.getLoggedInUsername());
            /*
             * Initially there should be no items
             */
            Assert.AreEqual("No items left to do!", homePage.ItemsLeftTodo.Text);
            /**
             * Add Todo's
             */
            String[] title = new String[2];
            String[] description = new string[2];
            title[0] = "Need to Watch Silicon Valley!";
            title[1] = "Google, remind me that JS is the best!";
            description[0] = "It was a long shot to build a new internet!";
            description[1] = "Why does C# hate me.";

            int i = 0;
            while (i != 2)
            {
                homePage.enterTitle(title[i]);
                homePage.enterDescription(description[i]);
                homePage.addTodo();
                homePage.waitTillTodoAdded(i + 1);
                i++;
            }
            /**
             * Check if its added
             */
            Assert.AreEqual("2 items left to do!", homePage.ItemsLeftTodo.Text);

            /**
             * Complete the first to do
             */
            homePage.completeTodos(1);
            //System.Threading.Thread.Sleep(10000);
            /**
             * Go to the active tab and we need to see the second todo we added
             */
            homePage.ActiveTab.Click();
            String[] res = homePage.getAllTodos();
           
            foreach (string toDos in res)
            {
                String[] toDo = toDos.Split("\n", 3);
                Assert.AreEqual(title[0], toDo[0].Trim());
                Assert.AreEqual(description[0], toDo[1].Trim());
                Assert.AreEqual(DateTime.Now.ToString("MMMM d, yyyy"), toDo[2].Trim());

            }
            /**
             * Now go to the Completed tab
             */
            homePage.CompletedTab.Click();
           
            String[] completeres = homePage.getAllTodos();

            foreach (string toDos in completeres)
            {
                String[] toDo = toDos.Split("\n", 3);
                Assert.AreEqual(title[1], toDo[0].Trim());
                Assert.AreEqual(description[1], toDo[1].Trim());
                Assert.AreEqual(DateTime.Now.ToString("MMMM d, yyyy"), toDo[2].Trim());
            }

            /**
             * Delete em all
             */
            homePage.AllTab.Click();
            homePage.deleteTodo(homePage.getAllTodosCount());
            homePage.logout();
        }
    }

}
