using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Infra.Utils.Exceptions;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;
using MS.Services.Auth.Core.Domain.Models.ServiceDtos.Users;
using MS.Services.Auth.Core.Domain.Plugins.Cryptography;
using System;

namespace MS.Services.Auth.Core.Application.Services.UserServices
{
    public class CreateUserServicev2 : BaseService<CreateUserDTO>
    {
        private readonly ISearchRepository<User> _searchRepository;
        private readonly IPasswordHash _passwordHash;
        private readonly ICreateRepository<User> _createRepository;

        public CreateUserServicev2(IServiceProvider serviceProvider,
            ISearchRepository<User> searchRepository,
            IPasswordHash passwordHash,
            ICreateRepository<User> createRepository): base(serviceProvider)
        {
            _createRepository = createRepository;
            _searchRepository = searchRepository;
            _passwordHash = passwordHash;
        }

        public async override Task<CreateUserDTO> ExecuteAsync(CreateUserDTO serviceDTO)
        {
            return await OnTransactionAsync(async () =>
            {
                var user = _imapper.Map<User>(serviceDTO.Request);

                await ValidateAsync(serviceDTO);

                user.PasswordHash = _passwordHash.GeneratePasswordHash();
                user.Password = _passwordHash.EncryptPassword(user.Password, user.PasswordHash);

                user = await _createRepository.CreateAsync(user);

                serviceDTO.Response = _imapper.Map<CreatedUserModel>(user);

                return serviceDTO;
            });
        }

        public override Task ValidateAsync(CreateUserDTO serviceDTO)
        {
            if (_searchRepository.Queryable().Where(u => u.Username == serviceDTO.Request.Username).Any())
            {
                throw new BusinessException("There is already a registered user with the entered username.");
            }

            if (_searchRepository.Queryable().Where(u => u.Email == serviceDTO.Request.Email).Any())
            {
                throw new BusinessException("There is already a registered user with the email provided");
            }

            return Task.CompletedTask;
        }
    }
}
