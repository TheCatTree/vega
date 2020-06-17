using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vega.Models
{
        [Table("Features")]
        public class Feature
    {
        public int Id {get; set;}

        [Required]
        [StringLength(255)]
        public string Name {get; set;}

        public IList<FeatureMakeJoin> FeatureMakeJoins {get; set;} 

        public Feature()
        {
            FeatureMakeJoins = new Collection<FeatureMakeJoin>();
        }
    }


        public class FeatureMakeJoin
    {    
    public int FeatureId { get; set; }
    public Feature Feature { get; set; }
    public int MakeId { get; set; }
    public Make Make { get; set; }
    }

}