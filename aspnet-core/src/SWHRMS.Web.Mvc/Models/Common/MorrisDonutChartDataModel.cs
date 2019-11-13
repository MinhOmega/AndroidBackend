using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWHRMS.Web.Models.Common
{
    public class MorrisDonutChartDataModel
    {
        public MorrisDonutChartDataModel()
        {
        }
        public MorrisDonutChartDataModel(string label, int value)
        {
            this.label = label;
            this.value = value;
        }

        public string label { get; set; }
        public int value { get; set; }
    }
}
