using GrosvnerMenu.Service;
using System;
using System.Collections.Generic;
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
        public MenuController(IMenuLoader menuLoader)
        {

        }
    }
}
