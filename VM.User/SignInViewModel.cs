﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.User
{
    public class SignInViewModel
    {
       public string Email { get; set; }
        public string Password { get; set; }

        public long? EmployeeId { get; set; }

        public bool? IsActive { get; set; }

        public string RetUrl { get; set; }
        public long? RoleId { get; set; }
    }
}
