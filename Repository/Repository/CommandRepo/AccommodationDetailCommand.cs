using Domain.Entities;
using Domain.SeedWork;

namespace ProjectInfrastructure.Repository.CommandRepo
{
    internal class AccommodationDetailCommand : IRepositoryContract<AccommodationDetail>
    {
        private IUnitOfWork UoW { get; set; }
        public IUnitOfWork UnitOfWork => UoW;
        public AccommodationDetailCommand()
        {
            UoW = UnitOfWork;
        }

    }
}
