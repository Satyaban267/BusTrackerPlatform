using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracker.Domain
{
    public class Bus
    {
        public int Id { get; set; }
        public string OperatorName { get; set; }=string.Empty;
        public string Route { get; set; }=string.Empty;
        public decimal GeneralPrice { get; set; }

    }
}
