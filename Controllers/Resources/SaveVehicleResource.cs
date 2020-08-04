using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vega.Controllers.Resources
{
    public class ContactResource
    {
        [Required]
        [StringLength(255)]
        public string Name {get; set;}
        [Required]
        [StringLength(255)]
        public string Email {get; set;}

        public string Phone {get; set;}

    }
    public class SaveVehicleResource
    {
        public int Id {get; set;}

        public int ModelId {get; set;}

        public bool IsRegistered {get; set;}
        [Required]
        public ContactResource Contact {get; set;}

        public ICollection<int> Features {get; set;}

        public SaveVehicleResource()
        {
            Features = new Collection<int>();
        }
    }
}