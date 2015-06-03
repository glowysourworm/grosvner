using CsvHelper;
using GrosvnerMenu.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrosvnerMenu.Service
{
    /// <summary>
    /// Describes a component responsible for loading an IMenu from an input stream
    /// </summary>
    public interface IMenuLoader
    {
        /// <summary>
        /// Loads IMenu from a stream - leaving the stream open
        /// </summary>
        IMenu Load(Stream stream);
    }
    public class CsvMenuLoader : IMenuLoader
    {
        protected class CsvMenuRow
        {
            public int DishType { get; set; }
            public string MorningDish { get; set; }
            public string NightDish { get; set; }
        }
        public IMenu Load(Stream stream)
        {
            var csv = new CsvReader(new StreamReader(stream));
            var records = csv.GetRecords<CsvMenuRow>();

            return new Menu(records.ToDictionary(r => r.DishType, r => r.MorningDish),
                            records.ToDictionary(r => r.DishType, r => r.NightDish));
        }
    }
}
