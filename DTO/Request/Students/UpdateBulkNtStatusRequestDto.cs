namespace DTO.Request.Students
{
    public class UpdateBulkNtStatusRequestDto
    {
        public List<int> NtIds { get; set; } = new();
        public List<int> StudentIds { get; set; } = new();
    }
}
