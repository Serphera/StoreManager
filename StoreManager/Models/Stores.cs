using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreManager.Models
{
    public partial class Stores {

        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public Guid CompanyId { get; set; }

        [Required]
        [MaxLength(512)]
        public string Address { get; set; }

        [Required]
        [MaxLength(512)]
        public string City { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [MaxLength(16)]
        public string Zip { get; set; }

        [Required]
        [MaxLength(50)]
        public string Country { get; set; }

        
        [MaxLength(15)]
        [RegularExpression(@"[-]{0,1}[\d]{1,2}[.,]{0,1}[\d]{0,13}", ErrorMessage = "Value must be a number")]
        public string Longitude { get; set; }

        
        [MaxLength(15)]
        [RegularExpression(@"[-]{0,1}[\d]{1,2}[.,]{0,1}[\d]{0,13}", ErrorMessage = "Value must be a number")]
        public string Latitude { get; set; }

        [Required]
        public virtual Companies Company { get; set; }
    }
}
