using AutoMapper;
using MediatR;
using OutOfOffice.Application.Features.Employees.Requests.Commands;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Domain.Models.Entities;

namespace OutOfOffice.Application.Features.Employees.Handlers.Commands;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Unit>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public UpdateEmployeeCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = mapper.Map<Employee>(request.UpdateEmployeeDto);

        await repositoryManager.Employee.UpdateAsync(employee);
        await repositoryManager.SaveChangesAsync();

        return Unit.Value;

    }
}