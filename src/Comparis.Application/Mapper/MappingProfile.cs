using AutoMapper;
using Comparis.Application.Models;
using Comparis.Application.UseCases.Payment.Commands.ProccessPayment;
using Comparis.Domain.Entities;

namespace Comparis.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProccessPaymentCommand, Payment>().ReverseMap();
            CreateMap<Payment, PaymentModel>().ReverseMap();
        }
    }
}