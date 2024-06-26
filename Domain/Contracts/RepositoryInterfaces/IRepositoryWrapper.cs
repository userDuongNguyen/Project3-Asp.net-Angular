﻿using Domain.Contracts.RepositoryInterfaces;

namespace Contracts.RepositoryInterfaces
{
    public interface IRepositoryWrapper
    {
        IAccommodationDetailRepository AccommodationDetailRepository { get; }
        IAddressRepository AddressRepository { get; }
        IAgentRepository AgentRepository { get; }
        IContactDetailRepository ContactDetailRepository { get; }
        IContactMessageRepository ContactMessageRepository { get; }
        IListingRepository ListingRepository { get; }
        IPropertyRepository PropertyRepository { get; }
        IRentalFeeRepository RentalFeeRepository { get; }
        ISubcriptionRepository SubcriptionRepository { get; }
        IUserRepository UserRepository { get; }
        IWalletRepository WalletRepository { get; }
        Task SaveAsync();
    }
}
