using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vega.Models
{
    public class FeatureMakeJoin
    {    
        public int FeatureId { get; set; }
        public Feature Feature { get; set; }
        public int MakeId { get; set; }
        public Make Make { get; set; }
    }
}