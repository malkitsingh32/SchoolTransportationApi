using Application.Abstraction.Services;
using DTO.Request.Students;
using DTO.Response;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Students.Command.AddFamilyCharge
{
    public class AddFamilyChargeCommandHandler: IRequestHandler<AddFamilyChargeCommand, CommonResultResponseDto<string>>
    {
        private readonly IStudentsService _studentsService;

        public AddFamilyChargeCommandHandler(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(
            AddFamilyChargeCommand request,
            CancellationToken cancellationToken)
        {
            return await _studentsService.AddFamilyCharge(
                request.Adapt<AddFamilyChargeDto>()
            );
        }
    }
}
