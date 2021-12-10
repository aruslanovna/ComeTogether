using System.Collections.Generic;

namespace ComeTogether.Domain.Entities
{
   
    public class Nacel
    {
        public Nacel()
        {
            BusinessRegisters = new HashSet<BusinessRegister>();
        }

        public int NacelId { get; set; }
        public string NacelName { get; set; }
        public string Section { get; set; }
    
        public ICollection<BusinessRegister> BusinessRegisters { get; private set; }
    }
}