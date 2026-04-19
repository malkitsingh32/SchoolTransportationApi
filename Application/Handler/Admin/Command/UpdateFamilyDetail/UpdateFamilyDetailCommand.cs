using DTO.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;


namespace Application.Handler.Admin.Command.UpdateFamilyDetail
{
    public  class UpdateFamilyDetailCommand : IRequest<CommonResultResponseDto<string>>
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FatherFirstName { get; set; }
        [Required]
        public string MotherFirstName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string HomeNumber { get; set; }
        [Required]
        public string FatherCell { get; set; }
        [Required]
        public string MotherCell { get; set; }
        [Required]
        public int State { get; set; }
        [Required]
        public int City { get; set; }
        [Required]
        public int Zipcode { get; set; }
        public int Area { get; set; }
        public int District { get; set; }
        public int UserId { get; set; }
    }
}
