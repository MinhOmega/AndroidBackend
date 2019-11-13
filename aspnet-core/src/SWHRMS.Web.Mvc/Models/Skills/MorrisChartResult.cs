using SWHRMS.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWHRMS.Web.Models.Skills
{
    public class MorrisChartResult
    {
        public MorrisChartResult()
        {
        }
        public MorrisChartResult(List<MorrisDonutChartDataModel> data, List<string> colors)
        {
            this.Datas = data;
            this.Colors = colors;
        }

        public List<MorrisDonutChartDataModel> Datas { get; set; }
        public List<string> Colors { get; set; }
    }
}
