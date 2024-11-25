using FixsyWebApi.Data.Repository;
using FixsyWebApi.Data;
using FixsyWebApi.DTO.Services;
using FixsyWebApi.Data.Extensions;

namespace FixsyWebApi.Services.Service
{
    public class ServicesAppService
    {
        private readonly IGenericRepository<IFixsyDataContext> _repository;
        public ServicesAppService(IGenericRepository<IFixsyDataContext> repository)
        {
            _repository = repository;
        }

        public async Task<List<ServiceDto>> GetServices(GetServicesRequest request)
        {
            string searchValue = request.SearchValue.IsMissingValue() ? string.Empty : request.SearchValue;

            var services = await _repository.GetFilteredAsync<Data.Agg.Service>(s => searchValue == string.Empty || s.Name.Contains(searchValue));

            return services.Select(s => new ServiceDto
            {
               ServiceId = s.ServiceId,
               Name = s.Name,
            }).ToList();
        }
    }
}
