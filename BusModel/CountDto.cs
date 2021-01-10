using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusModel
{
  public   class CountDto
    {

        /// <summary>
        /// 业务id
        /// </summary>
        public string BusId { get; set; }
        public int sucsum { get; set; }
        public int failsum { get; set; }

        public string latTime { get; set; }
    }
}
