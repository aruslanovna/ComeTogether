namespace ComeTogether.Domain.Entities
{
    public class BusinessRegister
    {
        public int BusinessRegisterId { get; set; }

        public int? NacelId { get; set; }
        public Nacel nacel { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}