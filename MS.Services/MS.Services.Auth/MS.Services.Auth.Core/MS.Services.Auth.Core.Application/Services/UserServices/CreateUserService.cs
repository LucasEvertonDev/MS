using MS.Libs.Core.Application.Services;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Libs.Infra.Utils.Exceptions;
using MS.Services.Auth.Core.Domain.Contansts;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;
using MS.Services.Auth.Core.Domain.Models.Users;
using MS.Services.Auth.Core.Domain.Plugins.Cryptography;
using MS.Services.Auth.Core.Domain.Services.UserServices;

namespace MS.Services.Auth.Core.Application.Services.UserServices
{
    public class CreateUserService : BaseService<CreateUserModel>, ICreateUserService
    {
        private readonly ISearchRepository<User> _searchRepository;
        private readonly IPasswordHash _passwordHash;
        private readonly IValidatorModel<CreateUserModel> _createUserValidatorModel;
        private readonly ICreateRepository<User> _createRepository;

        public CreatedUserModel CreatedUser { get; set; }

        public CreateUserService(IServiceProvider serviceProvider,
            ISearchRepository<User> searchRepository,
            IPasswordHash passwordHash,
            IValidatorModel<CreateUserModel> createUserValidatorModel,
            ICreateRepository<User> createRepository) : base(serviceProvider)
        {
            _createRepository = createRepository;
            _searchRepository = searchRepository;
            _passwordHash = passwordHash;
            _createUserValidatorModel = createUserValidatorModel;
        }

        public override async Task ExecuteAsync(CreateUserModel param)
        {
            await OnTransactionAsync(async () =>
            {
                await ValidateAsync(param);

                var user = _imapper.Map<User>(param);

                user.PasswordHash = _passwordHash.GeneratePasswordHash();
                user.Password = _passwordHash.EncryptPassword(user.Password, user.PasswordHash);

                user = await _createRepository.CreateAsync(user);

                this.CreatedUser = _imapper.Map<CreatedUserModel>(user);
            });
        }

        protected override async Task ValidateAsync(CreateUserModel param)
        {
            if (_searchRepository.AsQueriable().Where(u => u.Username == param.Username).Any())
            {
                BusinessException(UserErrors.Business.ALREADY_USERNAME);
            }

            if (_searchRepository.AsQueriable().Where(u => u.Email == param.Email).Any())
            {
                BusinessException(UserErrors.Business.ALREADY_EMAIL);
            }

            await _createUserValidatorModel.ValidateModelAsync(param);
        }
    }
}
