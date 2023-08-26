using MS.Libs.Core.Domain.DbContexts.Entities.Base;
using System.Linq.Expressions;

namespace MS.Libs.Core.Domain.DbContexts.Repositorys;
public interface IDeleteRepository<T> where T : IEntity
{
    Task DeleteAsync(Expression<Func<T, bool>> predicate);
    Task DeleteLogicAsync(Expression<Func<T, bool>> predicate);
}
