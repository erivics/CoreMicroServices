using AutoMapper;
using EventBus.Messages.Events;
using Ordering.Application.Features.Orders.Command.CheckOutOrder;

namespace Ordering.API.Mapping
{
    public class OrderingProfile : Profile
    {
        public OrderingProfile()
        {
                           //
                CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>().ReverseMap();
        }
    }
}
