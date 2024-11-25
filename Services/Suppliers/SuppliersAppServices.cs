using FixsyWebApi.Data.Repository;
using FixsyWebApi.Data;
using FixsyWebApi.DTO.Suppliers;
using FixsyWebApi.Data.Agg;
using FixsyWebApi.Data.Extensions;
using FixsyWebApi.DTO;
using FixsyWebApi.Data.Helper;
using FixsyWebApi.Resources;

namespace FixsyWebApi.Services.Suppliers
{
    public class SuppliersAppServices
    {
        private readonly IGenericRepository<IFixsyDataContext> _repository;
        private readonly PostgreSqlQueryExecutor _sqlQueryExecutor;

        public SuppliersAppServices(IGenericRepository<IFixsyDataContext> repository,
            PostgreSqlQueryExecutor sqlQueryExecutor)
        {
            _repository = repository;
            _sqlQueryExecutor = sqlQueryExecutor;
        }


        public async Task<List<SupplierDto>> GetSuppliers(GetSuppliersRequest request)
        {
            string searchValue = request.SearchValue.IsMissingValue() ? string.Empty : request.SearchValue;

            var suppliers = await _repository.GetFilteredAsync<Supplier>(s=> s.Name.Contains(searchValue) || searchValue == string.Empty);

            var suppliersId = suppliers.Select(s=>s.SupplierId);

        
                var suppliersSercvices = await _repository.GetFilteredAsync<SupplierServices>(r => suppliersId.Contains(r.SupplierId));

                return suppliers.Select(s => new SupplierDto
                {
                    Name = s.Name,
                    SupplierId = s.SupplierId,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    Services = GetSupplierServices(suppliersSercvices, s.SupplierId)
                }).ToList();
        }

        private string GetSupplierServices(IEnumerable<SupplierServices> suppliersSercvices, int supplierId)
        {
            var supplierServices = suppliersSercvices.Where(w => w.SupplierId == supplierId);

            if (supplierServices.HasItems())
            {
               return supplierServices.Select(s => s.GetServiceName()).ToStringConcatenated();
            }

            return string.Empty;

        }

        public async Task<PagedResponse<SupplierDto>> GetSuppliersPaged(GetSuppliersPagedRequest request)
        {
            var parameters = new
            {
                PageNumber = request.Page,
                PageSize = request.PageSize,
                SearchValue = request.SearchValue,
            };

            var suppliersDto = await _sqlQueryExecutor.ExecuteQueryAsync<SupplierDto>(SupplierQueryHelper.GetSupplierPagedQuery(), parameters);

            int totalCount = 0;
            if (suppliersDto.HasItems())
            {
                totalCount = suppliersDto.FirstOrDefault().TotalCount;
            }

            return new PagedResponse<SupplierDto>(suppliersDto, request.Page, request.PageSize, totalCount);
        }

        public async Task<SupplierDto> CreateSupplier(CreateSupplierRequest request)
        {
            if(request == null || request.Supplier.IsNull())
            {
                return new SupplierDto { ValidationErrorMessage = Messages.NoInfoWasFoundToProcessRequest };
            }

            var supplierDto = request.Supplier;

            if (!CanCreateSupplier(supplierDto, out string validationMessage))
            {
                return new SupplierDto { ValidationErrorMessage = validationMessage };
            }

            var existingSupplier = await _repository.GetSingleAsync<Supplier>(s=> s.SupplierId == supplierDto.SupplierId);

            if (existingSupplier.IsNull()) {
                existingSupplier = new Supplier();

                _repository.Add(existingSupplier);
            }

            existingSupplier.Name = supplierDto.Name;
            existingSupplier.Status = supplierDto.Status;
            existingSupplier.PhoneNumber = supplierDto.PhoneNumber;
            existingSupplier.Email = supplierDto.Email;

            _repository.UnitOfWork.Commit(request.GetTransactionInfo(Transactions.CreateSupplier));

            return new SupplierDto { SuccessMessage = Messages.SuccessfullySavedRecords, SupplierId = existingSupplier.SupplierId }; 
        }

        private bool CanCreateSupplier(SupplierDto supplierDto, out string validationMessage)
        {
            if (supplierDto.Name.IsMissingValue())
            {
                validationMessage = Messages.MustEnterSupplierName;
                return false;
            }
            if (supplierDto.PhoneNumber.IsMissingValue())
            {
                validationMessage = Messages.MustEnterPhoneNumber;
                return false;
            }
            if (supplierDto.Email.IsMissingValue())
            {
                validationMessage = Messages.MustEnterEmail;
                return false;
            }
            if (supplierDto.Status.IsMissingValue())
            {
                validationMessage = Messages.MustEnterStatus;
                return false;
            }

            validationMessage = string.Empty;
            return true;
        }

        public async Task<SupplierDto> GetSuppliersById(GetSuppliersByIdRequest request)
        {
            var existingSupplier = await _repository.GetSingleAsync<Supplier>(s=> s.SupplierId == request.SupplierId);

            if (existingSupplier != null) {

                return new SupplierDto
                {
                    Name = existingSupplier.Name,
                    PhoneNumber = existingSupplier.PhoneNumber,
                    Email = existingSupplier.Email,
                    SupplierId = request.SupplierId,
                    Status = existingSupplier.Status,
                };
            }
            return null;
        }
    }
}
