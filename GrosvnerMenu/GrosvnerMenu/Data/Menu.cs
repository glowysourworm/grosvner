using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrosvnerMenu.Data
{
    public interface IMenu
    {
        IDictionary<int, string> MorningMenu { get; }
        IDictionary<int, string> NightMenu { get; }
    }
    public class Menu : IMenu
    {
        readonly IDictionary<int, string> _morningMenu;
        readonly IDictionary<int, string> _nightMenu;

        public Menu(IDictionary<int, string> morningMenu,
                    IDictionary<int, string> nightMenu)
        {
            _morningMenu = morningMenu;
            _nightMenu = nightMenu;
        }

        public IDictionary<int, string> MorningMenu
        {
            get { return _morningMenu; }
        }

        public IDictionary<int, string> NightMenu
        {
            get { return _nightMenu; }
        }
    }
}
