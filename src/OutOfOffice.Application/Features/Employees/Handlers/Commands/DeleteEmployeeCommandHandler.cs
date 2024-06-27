using AutoMapper;
using MediatR;
using OutOfOffice.Application.Features.Employees.Requests.Commands;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Domain.Models.Entities;

namespace OutOfOffice.Application.Features.Employees.Handlers.Commands;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Unit>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public DeleteEmployeeCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await repositoryManager.Employee.GetEmployeeByIdAsync(request.Id, false) ?? throw new Exception("Employee not found");

        await repositoryManager.Employee.DeleteAsync(employee);
        await repositoryManager.SaveChangesAsync();

        return Unit.Value;
    }
}