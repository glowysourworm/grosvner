﻿using GrosvnerMenu.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrosvnerMenu.Service
{
    public interface IMenuInputReader
    {
        string Read(string input, IMenu menu);
    }
    public class MenuInputReader : IMenuInputReader
    {
        public string Read(string input, IMenu menu)
        {
            throw new NotImplementedException();
        }
    }
}
