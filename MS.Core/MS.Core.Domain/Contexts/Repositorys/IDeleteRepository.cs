using MS.Core.Domain.Contexts.Entities.Base;

namespace MS.Core.Domain.Contexts.Repositorys;
public interface IDeleteRepository<T> where T : IEntity
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="domain"></param>
    /// <returns></returns>
    Task<T> Delete(T domain);
}
