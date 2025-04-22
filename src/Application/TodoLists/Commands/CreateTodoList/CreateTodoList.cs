using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.TodoLists.Commands.CreateTodoList;

public record CreateTodoListCommand : IRequest<int>
{
    public string? Title { get; init; }
}

public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTodoListCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var entity = new TodoList
            {
                Title = request.Title,
                Created = DateTime.UtcNow
            };

            _context.TodoLists.Add(entity);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Có thể có thêm xử lý logic liên quan

            await _unitOfWork.CommitAsync();

            return entity.Id;
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}
