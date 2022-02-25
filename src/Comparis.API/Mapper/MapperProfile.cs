using AutoMapper;
using Comparis.API.Requests.Payment;
using Comparis.API.Responses.Payment;
using Comparis.Application.Models;
using Comparis.Application.UseCases.Payment.Commands.ProccessPayment;

namespace Comparis.API.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ProccessPaymentRequest, ProccessPaymentCommand>().ReverseMap();
            CreateMap<PaymentResponse, PaymentModel>().ReverseMap();
        }
    }
}