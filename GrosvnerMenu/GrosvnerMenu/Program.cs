using GrosvnerMenu.Controller;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrosvnerMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            // load unity container - see App.config for component mapping
            var container = new UnityContainer();
            container.LoadConfiguration();

            // Resolve primary controller component
            var controller = container.Resolve<IMenuController>();

            // Hello message
            Console.WriteLine("Grosvner Menu Processor \r\n");

            // Enter main input loop
            do
            {
                Console.Write("Input: ");
                var input = Console.ReadLine();
                var output = controller.ProcessInput(input);
                Console.Write("Output: {0} \r\n\n", output);

            } while (true);
        }
    }
}
