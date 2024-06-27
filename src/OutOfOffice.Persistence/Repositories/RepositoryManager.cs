using OutOfOffice.Contracts.Persistence;

namespace OutOfOffice.Persistence.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryOutOfOfficeDbContext repositoryContext;

    private readonly Lazy<ILeaveRequestRepository> leaveRequestRepository;

    private readonly Lazy<IApprovalRequestRepository> approvalRequestRepository;

    private readonly Lazy<IProjectRepository> projectRepository;
    
    private readonly Lazy<IEmployeeRepository> employeeRepository;

    public RepositoryManager(RepositoryOutOfOfficeDbContext repositoryContext)
    {
        this.repositoryContext = repositoryContext;
        employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(repositoryContext));
        projectRepository = new Lazy<IProjectRepository>(() => new ProjectRepository(repositoryContext));
        leaveRequestRepository = new Lazy<ILeaveRequestRepository>(() => new LeaveRequestRepository(repositoryContext));
        approvalRequestRepository = new Lazy<IApprovalRequestRepository>(() => new ApprovalRequestRepository(repositoryContext));
    }

    public IEmployeeRepository Employee => employeeRepository.Value;

    public IProjectRepository Project => projectRepository.Value;

    public ILeaveRequestRepository LeaveRequest => leaveRequestRepository.Value;

    public IApprovalRequestRepository ApprovalRequest => approvalRequestRepository.Value;

    public async Task SaveChangesAsync()
    {
        await repositoryContext.SaveChangesAsync();
    }
}