using Contracts.RepositoryInterfaces;
using Domain.Contracts.RepositoryInterfaces;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class RepositoryWrapper(DataContext repositoryContext) : IRepositoryWrapper
    {
#nullable disable
        private readonly DataContext _repoContext = repositoryContext;
        private IAccommodationDetailRepository _ADrepository;
        private IAddressRepository _AddressRepository;
        private IAgentRepository _AgentRepository;
        private IContactDetailRepository _ContactDetailRepository;
        private IContactMessageRepository _ContactMessageRepository;
        private IListingRepository _ListingRepository;
        private IPropertyRepository _PropertyRepository;
        private IRentalFeeRepository _RentalFeeRepository;
        private ISubcriptionRepository _SubcriptionRepository;
        private IUserRepository _UserRepository;
        private IWalletRepository _WalletRepository;

        public IAddressRepository AddressRepository
        {
            get
            {
                _AddressRepository = new AddressRepository(_repoContext);
                return _AddressRepository;
            }
        }

        public IAgentRepository AgentRepository
        {
            get
            {
                _AgentRepository = new AgentRepository(_repoContext);
                return _AgentRepository;
            }
        }

        public IContactDetailRepository ContactDetailRepository
        {
            get
            {
                _ContactDetailRepository = new ContactDetailRepository(_repoContext);
                return _ContactDetailRepository;
            }
        }

        public IContactMessageRepository ContactMessageRepository
        {
            get
            {
                _ContactMessageRepository = new ContactMessageRepository(_repoContext);
                return _ContactMessageRepository;
            }
        }

        public IListingRepository ListingRepository
        {
            get
            {
                _ListingRepository = new ListingRepository(_repoContext);
                return _ListingRepository;
            }
        }

        public IPropertyRepository PropertyRepository
        {
            get
            {
                _PropertyRepository = new PropertyRepository(_repoContext);
                return _PropertyRepository;
            }
        }

        public IRentalFeeRepository RentalFeeRepository
        {
            get
            {
                _RentalFeeRepository = new RentalFeeRepository(_repoContext);
                return _RentalFeeRepository;
            }
        }

        public ISubcriptionRepository SubcriptionRepository
        {
            get
            {
                _SubcriptionRepository = new SubcriptionRepository(_repoContext);
                return _SubcriptionRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                _UserRepository = new UserRepository(_repoContext);
                return _UserRepository;
            }
        }

        public IAccommodationDetailRepository AccommodationDetailRepository
        {
            get
            {
                _ADrepository = new AccommodationDetailRepository(_repoContext);
                return _ADrepository;
            }
        }

        public IWalletRepository WalletRepository
        {
            get
            {
                _WalletRepository = new WalletRepository(_repoContext);
                return _WalletRepository;
            }
        }

        public async Task SaveAsync() => await _repoContext.SaveChangesAsync();
    }
}
