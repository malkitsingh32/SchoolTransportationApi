using Application.Abstraction.Services;
using DTO.Request.Routes;
using Mapster;
using MediatR;

namespace Application.Handler.Routes.Command.DownloadPrintOrder
{
    public class DownloadPrintOrderCommandHandler : IRequestHandler<DownloadPrintOrderCommand, byte[]>
    {
        private readonly IRoutesService _routesService;

        public DownloadPrintOrderCommandHandler(IRoutesService routesService)
        {
            _routesService = routesService;
        }

        public async Task<byte[]> Handle(DownloadPrintOrderCommand downloadPrintOrderCommand, CancellationToken cancellationToken)
        {
            //return await _routesService.DownloadPrintOrder();

            var dto = downloadPrintOrderCommand.Adapt<DownloadPrintForRoutesRequestDto>();
            var printData = await _routesService.DownloadPrintOrder(dto);
            return printData.Data;
        }
    }
}
