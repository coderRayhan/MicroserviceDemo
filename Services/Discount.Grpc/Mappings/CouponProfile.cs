using AutoMapper;
using Discount.Grpc.Models;
using Discount.Grpc.Protos;

namespace Discount.Grpc.Mappings;

public class CouponProfile : Profile
{
    public CouponProfile()
    {
        CreateMap<CouponRequest, Coupon>().ReverseMap();
    }
}
