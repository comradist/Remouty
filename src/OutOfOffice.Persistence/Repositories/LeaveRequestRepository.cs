
using OutOfOffice.Application.Interfaces;
using OutOfOffice.Domain.Models.Entities;

namespace OutOfOffice.Persistence.Repositories;

public class LeaveRequestRepository : GenericRepositoryManager<LeaveRequest, Guid>, ILeaveRequestRepository
{
    public LeaveRequestRepository(RepositoryOutOfOfficeDbContext repositoryOutOfOfficeDbContext) : base(repositoryOutOfOfficeDbContext)
    {
    }
}