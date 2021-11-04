using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb.Models.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<NationalParkModel> NationalParkList { get; set; }
        public IEnumerable<TrailModel> TrailList { get; set; }
    }
}
