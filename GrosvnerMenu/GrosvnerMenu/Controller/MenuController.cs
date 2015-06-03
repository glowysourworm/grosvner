using GrosvnerMenu.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrosvnerMenu.Controller
{
    public interface IMenuController
    {
    }
    public class MenuController : IMenuController
    {
        readonly IMenuLoader _menuLoader;
        readonly IMenuInputReader _menuInputReader;

        public MenuController(IMenuLoader menuLoader,
                              IMenuInputReader menuInputReader)
        {
            _menuLoader = menuLoader;
            _menuInputReader = menuInputReader;

            Initialize();
        }

        protected virtual void Initialize()
        {

        }
    }
}
