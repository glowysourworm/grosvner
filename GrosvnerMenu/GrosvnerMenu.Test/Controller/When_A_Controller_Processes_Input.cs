using GrosvnerMenu.Controller;
using GrosvnerMenu.Data;
using GrosvnerMenu.Service;
using GrosvnerMenu.Test.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrosvnerMenu.Test.Controller
{
    [TestClass]
    public class When_A_Controller_Processes_Input
    {
        Mock<IMenu> _menu;
        Mock<IMenuLoader> _loader;
        Mock<IMenuInputReader> _reader;
        Mock<IMenuSource> _source;
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

            _source = new Mock<IMenuSource>();
            _source.Setup(x => x.Open()).Returns(new MemoryStream());

            _loader = new Mock<IMenuLoader>();
            _loader.Setup(x => 
                          x.Load(_source.Object.Open()))
                   .Returns(_menu.Object);

            _reader = new Mock<IMenuInputReader>();            

            // load test data
            using (var stream = new StreamReader(typeof(When_The_MenuInputReader_Is_Called).Assembly.GetManifestResourceStream("GrosvnerMenu.Test.Resources.input-output-sample.json")))
            {
                var json = stream.ReadToEnd();
                var jarray = JArray.Parse(json);
                _testData = jarray.Select(x => new KeyValuePair<string, string>((string)x["Input"], (string)x["Output"])).ToList();
            }
        }

        [TestMethod]
        public void An_Output_Is_Always_Produced()
        {
            var controller = new MenuController(_source.Object, _loader.Object, _reader.Object);

            for (int i=0;i<5;i++)
            {
                var something = i.GetHashCode().ToString();
                _reader.Setup(x => x.Read(something, _menu.Object)).Returns("Something");
                var output = controller.ProcessInput(something);
                Assert.IsTrue(!string.IsNullOrEmpty(output));
            }
        }
    }
}
