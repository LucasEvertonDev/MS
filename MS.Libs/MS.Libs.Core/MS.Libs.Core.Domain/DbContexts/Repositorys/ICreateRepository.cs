using MS.Libs.Core.Domain.DbContexts.Entities.Base;

namespace MS.Libs.Core.Domain.DbContexts.Repositorys;

public interface ICreateRepository<T> where T : IEntity
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="domain"></param>
    /// <returns></returns>
    Task<T> CreateAsync(T domain);
}
