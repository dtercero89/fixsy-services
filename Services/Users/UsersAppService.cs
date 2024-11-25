using ENCode;
using FixsyWebApi.Data;
using FixsyWebApi.Data.Agg;
using FixsyWebApi.Data.Extensions;
using FixsyWebApi.Data.Repository;
using FixsyWebApi.DTO.Users;
using FixsyWebApi.Resources;
using FixsyWebApi.Data.Helper;

namespace FixsyWebApi.Services.Users
{
    public class UsersAppService
    {
        private readonly IGenericRepository<IFixsyDataContext> _repository;
        private readonly PostgreSqlQueryExecutor _queryExecutor;

        private readonly FixsyUnitOfWork _unitOfWork;


        public UsersAppService(IGenericRepository<IFixsyDataContext> repository, PostgreSqlQueryExecutor queryExecutor)
        {
            if(repository == null) throw new ArgumentNullException(nameof(repository));
            if (queryExecutor == null) throw new ArgumentNullException(nameof(queryExecutor));

            _repository = repository;
            _queryExecutor = queryExecutor;
        }

        public async Task<LoginDto> RegisterUser(RegisterUserRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            UserDto userDto = request.User;
            string validationMessage;
            if (!CanRegisterUser(userDto, out validationMessage))
            {
                return new LoginDto().AddValidationMessage<LoginDto>(validationMessage);
            }

            string encryptedPassword = Hash.Encrypt(userDto.Email, userDto.Password);

            var newUser = new Data.Agg.User(userDto.Name, userDto.Email, encryptedPassword,
                                    userDto.PhoneNumber, null);

            if (userDto.IsCustomer)
            {
                var newCustomer = new Data.Agg.Customer(userDto.Address);
                newUser.AddCustomer(newCustomer);
            }

            _repository.Add(newUser);
            _repository.UnitOfWork.Commit(request.GetTransactionInfo(Transactions.RegisterUser));

            var loginDto = await BuildLoginDto(newUser);
            loginDto.SuccessMessage = Messages.RegistrationSuccessful;

            return loginDto;
        }

        private List<int> GetNewUserDefaultPermission()
        {
            var profilePermission = _repository.GetSingle<Data.Agg.Permission>(s=> s.Name == PermissionsNames.Profile);

            if(profilePermission != null)
            {
                return new List<int> { profilePermission.PermissionId };
            }

            return new List<int>();
        }

        private bool CanRegisterUser(UserDto userDto, out string validationMessage)
        {
            if (userDto is null)
            {
                throw new ArgumentNullException(nameof(userDto));
            }

            if (userDto.Email.IsMissingValue())
            {
                validationMessage = Messages.EmailIsRequiredToRegister;
                return false;
            }
            if (userDto.Name.IsMissingValue())
            {
                validationMessage = Messages.NameIsRequiredToRegister;
                return false;
            }
            if (userDto.Password.IsMissingValue())
            {
                validationMessage = Messages.PasswordIsRequiredToRegister;
                return false;
            }
            if (userDto.PhoneNumber.IsMissingValue())
            {
                validationMessage = Messages.PhoneNumberIsRequiredToRegister;
                return false;
            }

            var existingUserMail = _repository.GetSingle<Data.Agg.User>(d => d.Email == userDto.Email);
            if (existingUserMail != null)
            {
                validationMessage = Messages.EmailEnteredIsAlreadyInUse;
                return false;
            }


            bool passConfirmationPassword = userDto.Password == userDto.PasswordConfirm;
            if (!passConfirmationPassword)
            {
                validationMessage = Messages.WrongPasswordConfirmation;
                return false;
            }

            validationMessage = null;
            return true;
        }

        public async Task<LoginDto> LoginUser(LoginRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var user = _repository.GetSingle<User>(s => s.Email == request.Email);
            
            string validationMessage;
            if (!CanLoginUser(request, user, out validationMessage))
            {
                return new LoginDto().AddValidationMessage<LoginDto>(validationMessage);
            }
            return await BuildLoginDto(user);
        }

        public async Task<LoginDto> GetUserById(GetUserByIdRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var user = _repository.GetSingle<Data.Agg.User>(s => s.UserId == request.Id);

            string validationMessage;
            if (!UserIdValid(request, user, out validationMessage))
            {
                return new LoginDto().AddValidationMessage<LoginDto>(validationMessage);
            }
            return await BuildLoginDto(user);
        }


        private async Task<LoginDto> BuildLoginDto(Data.Agg.User user)
        {
            var permissions = await GetPermissionsByUserCode(user.Email);

            return new LoginDto
            {
                UserId = user.UserId,
                Id = user.UserId,
                UserName = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Permissions = permissions,
                CreationDate = user.TransactionDate.ToISOString(),
                IsCustomer = user.Customers.HasItems(),
                CustomerId = user.GetCustomerId()
            };
        }


        public async Task<List<string>> GetPermissionsByUserCode(string email)
        {
            string sql = @"
            SELECT DISTINCT p.""Name""
            FROM ""Users"" u 
            INNER JOIN ""UserRoles"" ur ON ur.""UserId"" = u.""UserId""
            INNER JOIN ""Roles"" r ON r.""RoleId"" = ur.""RoleId""
            INNER JOIN ""PermissionsByRole"" pr ON pr.""RoleId"" = r.""RoleId""
            INNER JOIN ""Permissions"" p ON p.""PermissionId"" = pr.""PermissionId""
            WHERE u.""Email"" = @Email";


            // Parámetros para la consulta
            var parameters = new { Email = email };

            // Ejecutar la consulta y obtener los permisos
            var permissions = await _queryExecutor.ExecuteQueryAsync<Permission>(sql, parameters);

            return permissions.Select(s=>s.Name).ToList();
        }


        private bool CanLoginUser(LoginRequest request, Data.Agg.User user, out string validationMessage)
        {
     
            if (request.Password.IsMissingValue())
            {
                validationMessage = Messages.MustEnterPsw;
                return false;
            }

            if(user == null)
            {
                validationMessage = Messages.NoUserInfo.AddParameters(request.Email);
                return false;
            }

            bool isValidPassword = Hash.Compare(user.Email, request.Password, user.Password);

            if (!isValidPassword)
            {
                validationMessage = Messages.InvalidPassword;
                return false;
            }
            validationMessage = string.Empty;
            return true;
        }

        private bool UserIdValid(GetUserByIdRequest request, Data.Agg.User user, out string validationMessage)
        {
            if (user == null)
            {
                validationMessage = Messages.NoUserInfo.AddParameters(request.Id);
                return false;
            }

            validationMessage = string.Empty;
            return true;
        }


    }
}
