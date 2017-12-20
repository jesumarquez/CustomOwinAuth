using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomOWINAuth.Models
{
    public class UserState
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}