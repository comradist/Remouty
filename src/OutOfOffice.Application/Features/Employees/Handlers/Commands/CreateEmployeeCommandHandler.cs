using AutoMapper;
using MediatR;
using OutOfOffice.Application.Features.Employees.Requests.Commands;
using OutOfOffice.Contracts.Persistence;
using OutOfOffice.Domain.Models.Entities;
using OutOfOffice.Shared.DTOs.Employee;

namespace OutOfOffice.Application.Features.Employees.Handlers.Commands;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, EmployeeDto>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public CreateEmployeeCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
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

        // if(request.EmployeeDto.PeoplePartnerId == Guid.Empty)
        // {
        //     request.EmployeeDto.PeoplePartnerId = null;
        // }
        var employee = mapper.Map<Employee>(request.EmployeeDto);

        if (request.EmployeeDto.ProjectIds != null )
        {
            employee.Id = Guid.NewGuid();

            foreach (var projectId in request.EmployeeDto.ProjectIds)
            {
                if (projectId == null)
                {
                    continue;
                }
                var projectEmployee = new ProjectEmployee()
                {
                    EmployeeId = employee.Id,
                    ProjectId = projectId
                };

                employee.ProjectEmployees.Add(projectEmployee);
            }
        }

        await repositoryManager.Employee.CreateAsync(employee);
        employee = await repositoryManager.Employee.GetEmployeeByIdAsync(employee.Id, false);

        var employeeDto = mapper.Map<EmployeeDto>(employee);

        return employeeDto;
    }
}
