using Contracts.RepositoryInterfaces;
using Infrastructure;
using ProjectApplication.Repository;

namespace Repository.Repository
{
    public class RepositoryWrapper(RepositoryContext repositoryContext) : IRepositoryWrapper
    {
#nullable disable
        private readonly RepositoryContext _repoContext = repositoryContext;
        private IAccommodationDetailRepository _ADrepository;
        private IAddressRepository _AddressRepository;
        private IAgentRepository _AgentRepository;
        private IContactDetailRepository _ContactDetailRepository;
        private IContactMessageRepository _ContactMessageRepository;
        private IListingRepository _ListingRepository;
        private IPropertyRepository _PropertyRepository;
        private IRentalFeesRepository _RentalFeesRepository;
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

        public IRentalFeesRepository RentalFeesRepository
        {
            get
            {
                _RentalFeesRepository = new RentalFeesRepository(_repoContext);
                return _RentalFeesRepository;
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
