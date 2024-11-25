using FixsyWebApi.Data;
using FixsyWebApi.Data.Repository;

namespace FixsyWebApi.Services
{
    public class ProfileAppService
    {
        private readonly IGenericRepository<IFixsyDataContext> _repository;

        public ProfileAppService(IGenericRepository<IFixsyDataContext> repository)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            _repository = repository;
        }


    }
}
