using Auction.Domain.Entities;
using System.Linq.Expressions;

namespace Auction.Application.Contracts.Repositories;

public interface IReadRepository<T>
    where T : BaseEntity
{
    Task<T> GetByIdAsync(string id);
    Task<IReadOnlyCollection<T>> GetAllAsync();
    Task<IReadOnlyCollection<T>> GetAsync(Expression<Func<T, bool>> predicate);
}