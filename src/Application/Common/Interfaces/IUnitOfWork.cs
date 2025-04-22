namespace CleanArchitecture.Application.Common.Interfaces;
public interface IUnitOfWork
{
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
