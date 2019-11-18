using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreManager.Models
{
    public partial class Companies {

        public Companies() {
            Stores = new HashSet<Stores>();
        }

        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public int OrganizationNumber { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<Stores> Stores { get; set; }
    }
}
