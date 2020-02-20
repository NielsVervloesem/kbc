using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using System;

namespace KBCFoodAndGo.Tests.UITests
{

    [TestFixture]
    public class Test
    {
        private string reportDirectory = "reports";
        private string reportFormat = "xml";
        private string testName = "Untitled";
        protected AndroidDriver<AndroidElement> driver = null;
        AppiumOptions options = new AppiumOptions();
        [SetUp()]
        public void SetupTest()
        {
            options.PlatformName = "Android";

            options.AddAdditionalCapability("reportDirectory", reportDirectory);
            options.AddAdditionalCapability("reportFormat", reportFormat);
            options.AddAdditionalCapability("testName", testName);
            options.AddAdditionalCapability(MobileCapabilityType.Udid, "940ee25f");
            options.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "com.companyname.kbcfoodandgo");
            options.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, "md5d2451249a5895d4d4e5f4225c69e7907.MainActivity");


            var uri = new Uri("http://localhost:4723/wd/hub");
            driver = new AndroidDriver<AndroidElement>(uri, options);
        }
        [Test()]
        public void TestUntitled()
        {
            driver.FindElement(By.XPath("xpath=//*[@text='BEVESTIGEN']")).Click();
            driver.FindElement(By.XPath("xpath=//*[@text='GESCHIEDENIS']")).Click();
            driver.FindElement(By.XPath("xpath=//*[@text='MENU']")).Click();
            driver.FindElement(By.XPath("xpath=//*[@class='android.widget.ImageView' and ./parent::*[@contentDescription='Stats']]")).Click();
            driver.FindElement(By.XPath("xpath=//*[@text='OMZET MAALTIJDEN']")).Click();
            driver.FindElement(By.XPath("xpath=//*[@text='FAVORIETE MAALTIJDEN']")).Click();
            driver.FindElement(By.XPath("xpath=//*[@contentDescription='Beheer']")).Click();
            driver.FindElement(By.XPath("xpath=//*[@contentDescription='Logs']")).Click();
            driver.FindElement(By.XPath("xpath=//*[@class='android.widget.ImageView' and ./parent::*[@contentDescription='Stats']]")).Click();
            driver.FindElement(By.XPath("xpath=//*[@class='android.widget.ImageView' and ./parent::*[@contentDescription='Logs']]")).Click();
            driver.FindElement(By.XPath("xpath=//*[@class='android.widget.ImageView' and ./parent::*[@contentDescription='Home']]")).Click();
            driver.FindElement(By.XPath("xpath=//*[@class='android.widget.ImageView' and ./parent::*[@contentDescription='Beheer']]")).Click();
            driver.FindElement(By.XPath("xpath=//*[@contentDescription='Home']")).Click();
            driver.FindElement(By.XPath("xpath=//*[@contentDescription='Beheer']")).Click();
            driver.FindElement(By.XPath("xpath=//*[@class='android.view.ViewGroup' and ./*[@class='android.view.ViewGroup' and ./*[@text='Naam:'] and ./*[@text='Caesar Salade']]]")).Click();
        }
        [TearDown()]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}