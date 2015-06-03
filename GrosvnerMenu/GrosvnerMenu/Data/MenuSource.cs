using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GrosvnerMenu.Data
{
    /// <summary>
    /// Component responsible for providing resource data
    /// </summary>
    public interface IMenuSource
    {
        /// <summary>
        /// Returns open resource stream. Caller is responsible for disposing of the stream
        /// </summary>
        Stream Open();
    }
    public class CsvMenuSource : IMenuSource
    {
        public Stream Open()
        {
            // Menu.csv is an embedded resource - so can safely call from the executing assembly
            var assembly = Assembly.GetAssembly(typeof(CsvMenuSource));
            return assembly.GetManifestResourceStream("GrosvnerMenu.Resources.Menu.csv");
        }
    }
}
