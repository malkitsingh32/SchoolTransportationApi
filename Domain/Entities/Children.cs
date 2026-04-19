namespace Domain.Entities
{
    public class Children
    {
        public int? Id { get; set; } 
        public int ParentId { get; set; }
        public string ChildName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
