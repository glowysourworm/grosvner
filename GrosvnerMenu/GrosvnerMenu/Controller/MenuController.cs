using GrosvnerMenu.Data;
using GrosvnerMenu.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrosvnerMenu.Controller
{
    /// <summary>
    /// Component responsible for processing main user input and producing correct output
    /// </summary>
    public interface IMenuController
    {
        string ProcessInput(string input);
    }
    public class MenuController : IMenuController
    {
        readonly IMenuSource _menuSource;
        readonly IMenuLoader _menuLoader;
        readonly IMenuInputReader _menuInputReader;

        IMenu _menu;

        public MenuController(IMenuSource menuSource,
                              IMenuLoader menuLoader,
                              IMenuInputReader menuInputReader)
        {
            _menuSource = menuSource;
            _menuLoader = menuLoader;
            _menuInputReader = menuInputReader;

            Initialize();
        }

        protected virtual void Initialize()
        {
            using (var stream = _menuSource.Open())
            {
                _menu = _menuLoader.Load(stream);
            }
        }

        public string ProcessInput(string input)
        {
            return _menuInputReader.Read(input, _menu);
        }
    }
}
