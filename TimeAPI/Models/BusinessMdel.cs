using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeAPI.Models
{
    public class BusinessMdel
    {
        public Business business { get; set; }
        public Logdetail log { get; set; }
        public Count count { get; set; }
    }

    public class BusinessMdellist
    {
        public List<BusinessMdel> busiMoList { get; set; }
    }
}