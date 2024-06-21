using AutoMapper;
using MediatR;
using OutOfOffice.Application.Features.Employees.Requests.Commands;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Domain.Models.Entities;

namespace OutOfOffice.Application.Features.Employees.Handlers.Commands;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Guid>
{
    private readonly IEmployeeRepository employeeRepository;
    private readonly IMapper mapper;

    public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        this.employeeRepository = employeeRepository;
        this.mapper = mapper;
    }

    public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        
        // var validator = new CreateNoteDtoValidator();
        // var validatorResult = await validator.ValidateAsync(request.NoteDto, cancellationToken);
        // if (!validatorResult.IsValid)
        // {
        //     throw new ValidationException(validatorResult);
        // }

        //await validator.ValidateAndThrowAsync(request.NoteDto, cancellationToken: cancellationToken);

        var employee = mapper.Map<Employee>(request.EmployeeDto);

        await employeeRepository.CreateAsync(employee);

        return employee.Id;
    }
}
