namespace DTO.Request.Admin
{
    public class AddSeasonFolderRequestDto
    {
        public int Id { get; set; }
        public string SeasonName { get; set; }
        public bool IsDefault { get; set; }
        public int UserId { get; set; }

    }
}
