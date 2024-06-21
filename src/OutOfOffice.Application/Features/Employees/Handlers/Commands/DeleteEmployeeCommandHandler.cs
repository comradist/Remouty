using AutoMapper;
using MediatR;
using OutOfOffice.Application.Features.Employees.Requests.Commands;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Domain.Models.Entities;

namespace OutOfOffice.Application.Features.Employees.Handlers.Commands;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Unit>
{
    private readonly IEmployeeRepository employeeRepository;
    private readonly IMapper mapper;

    public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        this.employeeRepository = employeeRepository;
        this.mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var user = await employeeRepository.GetEmployeeByIdAsync(request.Id, false) ?? throw new Exception("Employee not found");

        await employeeRepository.DeleteAsync(user);
        await employeeRepository.SaveChangesAsync();

        return Unit.Value;
    }
}