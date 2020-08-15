using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vega.Core.Models
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
}