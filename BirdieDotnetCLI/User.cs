﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdieDotnetCLI
{
    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        //public string Email { get; set; }

        public User(string name, string psw)
        {
            Name = name;
            Password = psw;
        }
    }
}
