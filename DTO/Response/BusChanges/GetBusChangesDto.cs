namespace DTO.Response.BusChanges
{
    public class GetBusChangesDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int FamilyId { get; set; }
        public string StudentName { get; set; }
        public string StudentOriginalAddress { get; set; }
        public string BusChangeAddress { get; set; }
        public string Area { get; set; }
        public int SchoolId { get; set; }
        public int Gender { get; set; }
        public int Grade { get; set; }
        public int BranchId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string StartFrom { get; set; }
        public string Include { get; set; }
        public string BusChangePhone { get; set; }
        public string MotherCell { get; set; }
        public string RouteName { get; set; }
        public string SchoolName { get; set; }
        public string GenderName { get; set; }
        public string GradeName { get; set; }
        public string BranchName { get; set; }
        public string RouteId { get; set; }
        public string Payment { get; set; }
        public string ProcessPaymentMethod { get; set; }
    }
}
