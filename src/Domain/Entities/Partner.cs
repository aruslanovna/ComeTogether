//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace ComeTogether.Domain.Entities
//{
//   public class Partner
//    {
//        public Partner()
//        {
//            PartnerTerritories = new HashSet<PartnerTerritory>();
//            //DirectReports = new HashSet<Partner>();
//            //Orders = new HashSet<Order>();
           
//        }
//        public int foundedPartnerId { get; set; }
//        public int joinedPartnerId { get; set; }
//        public string UserId { get; set; }
//        public string LastName { get; set; }
//        public string FirstName { get; set; }
//      //  public string Title { get; set; }
//      //  public string TitleOfCourtesy { get; set; }
//        public DateTime? BirthDate { get; set; }

//        public string Address { get; set; }
//        public string City { get; set; }
//        public string Region { get; set; }
//        public string PostalCode { get; set; }
//        public string Country { get; set; }
//        public string HomePhone { get; set; }
//       // public string Extension { get; set; }
//        public byte[] Photo { get; set; }
//        public string Notes { get; set; }
//        // public int? ReportsTo { get; set; }
//        // public string PhotoPath { get; set; }
//       // public Project Projects { get; set; }
//      //  public Partner Manager { get; set; }
//        public ICollection<PartnerTerritory> PartnerTerritories { get; private set; }
//      //  public ICollection<Partner> DirectReports { get; private set; }
//       // public ICollection<Project> Projects { get; private set; }
//    }
//}
