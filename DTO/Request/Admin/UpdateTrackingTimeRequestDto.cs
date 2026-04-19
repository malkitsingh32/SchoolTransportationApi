namespace DTO.Request.Admin
{
    public class UpdateTrackingTimeRequestDto
    {
        public int TrackingTimeId { get; set; }
        public int? TrackingStartTime { get; set; }
        public int? TrackingEndTime { get; set; }
    }
}
