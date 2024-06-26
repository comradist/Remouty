using AutoMapper;
using MediatR;
using OutOfOffice.Application.Features.Employees.Requests.Commands;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Domain.Models.Entities;
using OutOfOffice.Shared.DTOs.Employee;

namespace OutOfOffice.Application.Features.Employees.Handlers.Commands;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, EmployeeDto>
{
    private readonly IEmployeeRepository employeeRepository;
    private readonly IMapper mapper;

    public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        this.employeeRepository = employeeRepository;
        this.mapper = mapper;
    }

    public async Task<EmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        
        // var validator = new CreateNoteDtoValidator();
        // var validatorResult = await validator.ValidateAsync(request.NoteDto, cancellationToken);
        // if (!validatorResult.IsValid)
        // {
        //     throw new ValidationException(validatorResult);
        // }

        //await validator.ValidateAndThrowAsync(request.NoteDto, cancellationToken: cancellationToken);

        if(request.EmployeeDto.PeoplePartnerId == Guid.Empty)
        {
            request.EmployeeDto.PeoplePartnerId = null;
        }

        var employee = mapper.Map<Employee>(request.EmployeeDto);

        await employeeRepository.CreateAsync(employee);
        employee = await employeeRepository.GetEmployeeByIdAsync(employee.Id, false);

        var employeeDto = mapper.Map<EmployeeDto>(employee);

        return employeeDto;
    }
}
