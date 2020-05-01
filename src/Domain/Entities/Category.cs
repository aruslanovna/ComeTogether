using System.Collections.Generic;

namespace ComeTogether.Domain.Entities
{
    public class Category
    {
        public Category()
        {
            Products = new HashSet<Project>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }

        public ICollection<Project> Products { get; private set; }
    }
}
