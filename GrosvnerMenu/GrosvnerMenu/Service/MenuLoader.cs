using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LINQtoCSV;
using GrosvnerMenu.Data;


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
        /// <summary>
        /// Provides mapping to CSV file - embedded in application
        /// </summary>
        protected class CsvMenuRow
        {
            [CsvColumn(FieldIndex=0)]
            public int DishType { get; set; }

            [CsvColumn(FieldIndex = 1)]
            public string MorningDish { get; set; }

            [CsvColumn(FieldIndex = 2)]
            public string NightDish { get; set; }
        }
        public IMenu Load(Stream stream)
        {
            var csv = new CsvContext();
            var records = csv.Read<CsvMenuRow>(new StreamReader(stream));
            
            return new Menu(records.ToDictionary(r => r.DishType, r => r.MorningDish),
                            records.ToDictionary(r => r.DishType, r => r.NightDish));
        }
    }
}
