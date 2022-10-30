using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JSONResponseModel
{
    public class City
    {
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public ICollection<string> Cities { get; set; } = new List<string>();
    }
}
