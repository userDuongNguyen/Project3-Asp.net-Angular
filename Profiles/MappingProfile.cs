using AutoMapper;
using Domain.DataTransferObjects.GetDto;
using Domain.DataTransferObjects.PostDto;
using Domain.DataTransferObjects.PutDto;
using Domain.Entities;

namespace Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Create Map GetDto to Entity 
            CreateMap<Address, AddressGetDto>().ReverseMap();

            CreateMap<AccommodationDetail, AccommodationDetailGetDto>().ReverseMap();

            CreateMap<Agent, AgentGetDto>().ReverseMap();

            CreateMap<ContactDetail, ContactDetailGetDto>().ReverseMap();

            CreateMap<ContactMessage, ContactMessageGetDto>().ReverseMap();

            CreateMap<Listing, ListingGetDto>().ReverseMap();

            CreateMap<Property, PropertyGetDto>().ReverseMap();

            CreateMap<RentalFees, RentalFeesGetDto>().ReverseMap();

            CreateMap<Subcription, SubcriptionGetDto>().ReverseMap();

            CreateMap<User, UserGetDto>().ReverseMap();

            CreateMap<Wallet, WalletGetDto>().ReverseMap();
            //Create Map Entity to PostDto
            CreateMap<AddressPostDto, Address>().ReverseMap();

            CreateMap<AccommodationDetailPostDto, AccommodationDetail>().ReverseMap();

            CreateMap<AgentPostDto, Agent>().ReverseMap();

            CreateMap<ContactDetailPostDto, ContactDetail>().ReverseMap();

            CreateMap<ContactMessagePostDto, ContactMessage>().ReverseMap();

            CreateMap<ListingPostDto, Listing>().ReverseMap();

            CreateMap<PropertyPostDto, Property>().ReverseMap();

            CreateMap<RentalFeesPostDto, RentalFees>().ReverseMap();

            CreateMap<SubcriptionPostDto, Subcription>().ReverseMap();

            CreateMap<UserPostDto, User>().ReverseMap();

            CreateMap<WalletPostDto, Wallet>().ReverseMap();
            //Create Map Entity to PutDto
            CreateMap<AddressPutDto, Address>().ReverseMap();

            CreateMap<AccommodationDetailPutDto, AccommodationDetail>().ReverseMap();

            CreateMap<AgentPutDto, Agent>().ReverseMap();

            CreateMap<ContactDetailPutDto, ContactDetail>().ReverseMap();

            CreateMap<ContactMessagePutDto, ContactMessage>().ReverseMap();

            CreateMap<ListingPutDto, Listing>().ReverseMap();

            CreateMap<PropertyPutDto, Property>().ReverseMap();

            CreateMap<RentalFeesPutDto, RentalFees>().ReverseMap();

            CreateMap<SubcriptionPutDto, Subcription>().ReverseMap();

            CreateMap<UserPutDto, User>().ReverseMap();

            CreateMap<WalletPutDto, Wallet>().ReverseMap();
        }
    }
}
