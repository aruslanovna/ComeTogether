using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComeTogether.Domain.Entities
{
   public class PartnerTerritory {
        [Key]
        public int PartnerId { get; set; }
    public string TerritoryId { get; set; }

   // public Partner Partner { get; set; }
    public Territory Territory { get; set; }

    
    }
}
