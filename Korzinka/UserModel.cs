﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korzinka
{
    public class UserModel
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public UserRole Role { get; set; }
    }
}
