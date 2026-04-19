using DTO.Response.BackgroundServices;

namespace DTO.Request.BackgroundServices
{
    public class AddHistoryRequestDto
    {
        public IList<GetHistoryResponseDto> History { get; set; }
    }
}
