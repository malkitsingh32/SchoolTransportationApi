namespace DTO.Request.Admin
{
    public class UpdateFamilyTrackingRequestDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsTracking { get; set; }
    }
}
