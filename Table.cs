using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBestillinger
{
    public class Table
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public bool IsBooked { get; set; }
    }
}
