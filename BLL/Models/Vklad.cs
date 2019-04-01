using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class VkladMod
    {
        public int Id { get; set; }

        public int Balance { get; set; }

        public DateTime DateOpen { get; set; }

        public int Client_code { get; set; }

        public int Prog_code { get; set; }

    }
}
