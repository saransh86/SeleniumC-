using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UITests.PageObjects
{
    class HomePage
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        private IWebElement username => _driver.FindElement(By.CssSelector("h2>span"));
        private IWebElement logoutLink => _driver.FindElement(By.CssSelector(".info>a"));

        private IWebElement allTab => _driver.FindElement(By.CssSelector(".header>div>button:nth-child(1)"));

        private IWebElement activeTab => _driver.FindElement(By.CssSelector(".header>div>button:nth-child(2)"));

        private IWebElement completedTab => _driver.FindElement(By.CssSelector(".header>div>button:nth-child(3)"));

        private IWebElement title => _driver.FindElement(By.Id("title"));

        private IWebElement description => _driver.FindElement(By.Id("description"));

        private IWebElement addTodoButton => _driver.FindElement(By.CssSelector("main>div:nth-child(2)>div>button"));

        private IList<IWebElement> getTodo =>  new List<IWebElement>(_driver.FindElements(By.CssSelector("main>div:nth-child(1)>div:nth-child(2)>ul>li")));

        private IWebElement oneTodo => _driver.FindElement(By.CssSelector("main>div:nth-child(1)>div:nth-child(2)>ul>li"));
        private IList<IWebElement> deleteTodos => _driver.FindElements(By.CssSelector("button.remove"));

        private IWebElement itemsLeftTodo => _driver.FindElement(By.CssSelector(".header>div:nth-child(2)>span"));

        private IList<IWebElement> completeCheckbox => _driver.FindElements(By.CssSelector("main>div>div:nth-child(2)>ul>li>header>div>input"));

        private IWebElement noTodo => _driver.FindElement(By.CssSelector("div>h4"));

        public IWebElement NoTodo
        {
            get { return noTodo; }
        }
        public IWebElement ItemsLeftTodo
        {
            get { return itemsLeftTodo; }
        }

        public IWebElement AllTab
        {
            get { return allTab; }
        }

        public IWebElement ActiveTab
        {
            get { return activeTab; }
        }

        public IWebElement CompletedTab
        {
            get { return completedTab; }
        }
        public string getLoggedInUsername()
        {
            _wait.Until(_driver => username.Displayed);
            return username.Text;
        }

        public void addTodo()
        {
            _wait.Until(_driver => addTodoButton.Displayed);
            addTodoButton.Click();
        }

        public int getAllTodosCount()
        {
            return getTodo.Count;
        }

        public String[] getAllTodos()
        {
            String[] allToDoText = new String[getAllTodosCount()];
            int i = 0;
            foreach (IWebElement todo in getTodo)
            {
                allToDoText[i++] = todo.Text;
            }
            return allToDoText;
        }
        public void logout()
        {
            _wait.Until(_driver => logoutLink.Displayed);
            logoutLink.Click();
        }

        public void enterDescription(string descriptionStr)
        {
            _wait.Until(_driver => description.Displayed);
            description.SendKeys(descriptionStr);
        }

        public void enterTitle(string titleStr)
        {
            _wait.Until(_driver => title.Displayed);
            title.SendKeys(titleStr);
        }

        public bool isTodoButtonEnabled()
        {
            try
            {
                //_wait.Until(_driver => addTodoButton.Enabled);
                return addTodoButton.Enabled;
            }
            catch(NoSuchElementException e)
            {
                return false;
            }
        }
        private bool isTodoDeleted(IWebElement todo)
        {
            try
            {
                _wait.Until(_driver => !todo.Displayed);
                return todo.Displayed;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public void waitTillTodoAdded(int todoNumber)
        {
            _wait.Until(_driver => activeTab.Text == "Active (" + todoNumber + ")");
            System.Console.WriteLine("What do we have" + activeTab.Text);
        }

        public void waitTobeCompleted(int completeNumber)
        {
            _wait.Until(_driver => completedTab.Text == "Completed (" + completeNumber + ")");

        }

        public void deleteTodo(int todoCount)
        {
            int i = 0;
            
                foreach (IWebElement todo in deleteTodos)
                {
                    if(i != todoCount)
                    {
                        todo.Click();
                        bool checkIfDeleted = isTodoDeleted(todo);
                        if (!checkIfDeleted)
                        {
                            continue;
                        }
                        else
                        {
                            checkIfDeleted = isTodoDeleted(todo);
                        }
                        i++;
                    }
                    else
                    {
                        break;
                    }
                   
                }
            
        }

        public void completeTodos(int todos)
        {
            int i = 0;
          
          
                foreach (IWebElement todo in completeCheckbox)
                {
                    if(i != todos)
                    { 
                        
                        /**
                        * We can all of this testing in JS instead of using C#
                        * (Its what Richard would have done :))
                        */ 
                        IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
                        js.ExecuteScript("arguments[0].click();", todo);

                        waitTobeCompleted(i + 1);
                        i++;
                    }
                     else
                    {
                        break;
                    }
                
                }
           
          }
    }

    
}
