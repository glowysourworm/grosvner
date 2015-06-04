using GrosvnerMenu.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrosvnerMenu.Test.Data
{
    [TestClass]
    public class When_The_MenuSource_Is_Called_The_Resulting_Stream_Is_Available
    {
        [TestMethod]
        public void For_A_Csv_Menu_Source()
        {
            var source = new CsvMenuSource();

            var stream = source.Open();
            Assert.IsNotNull(stream);

            stream.Close();
            stream.Dispose();
        }
    }
}
