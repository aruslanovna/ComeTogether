using System.Collections.Generic;

namespace ComeTogether.Domain.Entities
{
    public class Territory
    {
        public Territory()
        {
            //EmployeeTerritories = new HashSet<EmployeeTerritory>();
            PartnerTerritories = new HashSet<PartnerTerritory>();
        }

        public string TerritoryId { get; set; }
        public string TerritoryDescription { get; set; }
        public int RegionId { get; set; }

        public Region Region { get; set; }
      //  public ICollection<EmployeeTerritory> EmployeeTerritories { get; private set; }
        public ICollection<PartnerTerritory> PartnerTerritories { get; private set; }
    }
}
