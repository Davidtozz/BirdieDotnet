using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdieDotnetCLI.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Psw { get; set; }
        //public string Email { get; set; }
        public string AuthorizationToken { get; set; }
        public string ConnectionId { get; set; }
        

        public User(string name, string psw)
        {
            Name = name;
            Psw = psw;
        }
    }
}
