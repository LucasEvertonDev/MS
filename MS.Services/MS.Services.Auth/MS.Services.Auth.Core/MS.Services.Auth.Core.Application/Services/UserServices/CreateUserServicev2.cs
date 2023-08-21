using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Libs.Infra.Utils.Exceptions;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;
using MS.Services.Auth.Core.Domain.Models.ServiceDtos.Users;
using MS.Services.Auth.Core.Domain.Plugins.Cryptography;

namespace MS.Services.Auth.Core.Application.Services.UserServices
{
    public class CreateUserServicev2 : BaseService<CreateUserModel>
    {
        private readonly ISearchRepository<User> _searchRepository;
        private readonly IPasswordHash _passwordHash;
        private readonly IValidatorModel<CreateUserModel> _createUserValidatorModel;
        private readonly ICreateRepository<User> _createRepository;

        public CreatedUserModel CreatedUser { get; set; }

        public CreateUserServicev2(IServiceProvider serviceProvider,
            ISearchRepository<User> searchRepository,
            IPasswordHash passwordHash,
            IValidatorModel<CreateUserModel> createUserValidatorModel,
            ICreateRepository<User> createRepository): base(serviceProvider)
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
                var user = _imapper.Map<User>(param);

                await ValidateAsync(param);

                user.PasswordHash = _passwordHash.GeneratePasswordHash();
                user.Password = _passwordHash.EncryptPassword(user.Password, user.PasswordHash);

                user = await _createRepository.CreateAsync(user);

                this.CreatedUser = _imapper.Map<CreatedUserModel>(user);
            });
        }

        protected override async Task ValidateAsync(CreateUserModel param)
        {
            if (_searchRepository.Queryable().Where(u => u.Username == param.Username).Any())
            {
                throw new BusinessException("There is already a registered user with the entered username.");
            }

            if (_searchRepository.Queryable().Where(u => u.Email == param.Email).Any())
            {
                throw new BusinessException("There is already a registered user with the email provided");
            }

            await _createUserValidatorModel.ValidateModelAsync(param);
        }
    }
}
