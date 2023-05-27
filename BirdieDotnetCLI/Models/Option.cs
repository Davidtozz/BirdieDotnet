using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdieDotnetCLI.Models
{
    public class Option
    {
        public string Text { get; set; }
        public bool IsChecked { get; set; }
        public Action Callback { get; set; }

        public Option() {
            throw new NotImplementedException();
        }
    }
}
