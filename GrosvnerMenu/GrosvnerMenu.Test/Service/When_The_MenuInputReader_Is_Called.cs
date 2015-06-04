using GrosvnerMenu.Data;
using GrosvnerMenu.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrosvnerMenu.Test.Service
{
    [TestClass]
    public class When_The_MenuInputReader_Is_Called
    {
        Mock<IMenu> _menu;
        IEnumerable<KeyValuePair<string, string>> _testData;

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

            // load test data
            using (var stream = new StreamReader(typeof(When_The_MenuInputReader_Is_Called).Assembly.GetManifestResourceStream("GrosvnerMenu.Test.Resources.input-output-sample.json")))
            {
                var json = stream.ReadToEnd();
                var jarray = JArray.Parse(json);
                _testData = jarray.Select(x => new KeyValuePair<string, string>((string)x["Input"], (string)x["Output"])).ToList();
            }
        }

        [TestMethod]
        public void The_Correct_Output_Is_Produced()
        {
            var reader = new MenuInputReader();
            foreach (var data in _testData)
            {
                var output = reader.Read(data.Key, _menu.Object);

                // Not case sensitive - ignore white space
                Assert.AreEqual(output.ToLower().Replace(" ", ""), data.Value.ToLower().Replace(" ", ""));
            }
        }
    }
}
