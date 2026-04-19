using DTO.Response.Routes;
using DTO.Response;
using MediatR;

namespace Application.Handler.Routes.Command.DownloadPrintOrder
{
    public class DownloadPrintOrderCommand : IRequest<byte[]>
    {
        public int? RouteId { get; set; }
        public DateTime? Date { get; set; }

    }
}
