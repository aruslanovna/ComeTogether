namespace ComeTogether.Domain.Entities
{
    public class Deal
    {
        public int DealId { get; set; }

        public int? ProjectId { get; set; }
        public Project project { get; set; }
        public string PartnerId { get; set; }
        public ApplicationUser Partner { get; set; }
    }
}
