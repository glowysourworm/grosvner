using System;
using Moq;
using GrosvnerMenu.Data;
using System.Collections.Generic;
using GrosvnerMenu.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GrosvnerMenu.Test.Service
{
    /// <summary>
    /// Purpose of test fixture is to test the result of loading a menu v.s. the expected result
    /// </summary>
    [TestClass]
    public class When_The_MenuLoader_Loads_A_Menu
    {
        Mock<IMenu> _menu;

        [TestInitialize]
        public void Initialize()
        {
            _menu = new Mock<IMenu>();
            _menu.SetupGet(m => m.MorningMenu).Returns(new Dictionary<int, string>()
            {
                { 1, "eggs" },
                { 2, "Toast" },
                { 3, "coffee" },
                { 4, "Not Applicable" }
            });
            _menu.SetupGet(m => m.NightMenu).Returns(new Dictionary<int, string>()
            {
                { 1, "steak" },
                { 2, "potato" },
                { 3, "wine" },
                { 4, "cake" }
            });
        }

        [TestMethod]
        public void The_Resulting_Menu_Is_Accurate_For_The_Csv_Loader()
        {
            // load stream by hand for this test since we're not testing IMenuSource
            try
            {
                var assembly = typeof(Menu).Assembly;
                var stream = assembly.GetManifestResourceStream("GrosvnerMenu.Resources.Menu.csv");
                var loader = new CsvMenuLoader();
                var menu = loader.Load(stream);

                // Assert each menu item equals the mocked object - NOT CASE SENSITIVE
                foreach (var keyValuePair in _menu.Object.MorningMenu)
                {
                    Assert.IsTrue(menu.MorningMenu.ContainsKey(keyValuePair.Key));
                    Assert.IsTrue(menu.MorningMenu[keyValuePair.Key].ToLower() == keyValuePair.Value.ToLower());
                }
                foreach (var keyValuePair in _menu.Object.NightMenu)
                {
                    Assert.IsTrue(menu.NightMenu.ContainsKey(keyValuePair.Key));
                    Assert.IsTrue(menu.NightMenu[keyValuePair.Key].ToLower() == keyValuePair.Value.ToLower());
                }
            }
            catch(Exception)
            {
                Assert.Fail();
            }
        }
    }
}
