
using AutoMapper;
using WalletApi.Application.Dtos;
using WalletApi.Domain.Entities;

namespace WalletApi.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Wallet, WalletDto>().ReverseMap();
    }
}
