using OutOfOffice.MVC.Services.Base;

namespace OutOfOffice.MVC.Configuration;

public class LookUpTablesConfiguration
{
    public List<AbsenceReason> AbsenceReasons { get; init; }
    
    public List<Position> Positions { get; init; }

    public List<ProjectStatus> ProjectStatuses { get; init; }

    public List<ProjectType> ProjectTypes { get; init; }

    public List<RequestStatus> RequestStatuses { get; init; }

    public List<Subdivision> Subdivisions { get; init; }


    public LookUpTablesConfiguration()
    {
        AbsenceReasons = new List<AbsenceReason>{
            new AbsenceReason{Id = 1, Name = "Vacation"},
            new AbsenceReason{Id = 2, Name = "Sick Leave"},
            new AbsenceReason{Id = 3, Name = "Business Trip"},
            new AbsenceReason{Id = 5, Name = "Paternity Leave"},
            new AbsenceReason{Id = 4, Name = "Maternity Leave"},
            new AbsenceReason{Id = 6, Name = "Unpaid Leave"},
            new AbsenceReason{Id = 7, Name = "Other"}
        
        };
        Positions = new List<Position>{
            new Position{Id = 1, Name = "Junior Developer"},
            new Position{Id = 2, Name = "Middle Developer"},
            new Position{Id = 3, Name = "Senior Developer"},
            new Position{Id = 4, Name = "Team Lead"},
            new Position{Id = 5, Name = "HR Manager"},
            new Position{Id = 6, Name = "Project Manager"},
            new Position{Id = 7, Name = "Business Analyst"},
            new Position{Id = 8, Name = "QA Engineer"}
        
        };
        ProjectStatuses = new List<ProjectStatus>{
            new ProjectStatus{Id = 1, Name = "In Progress"},
            new ProjectStatus{Id = 2, Name = "Completed"},
            new ProjectStatus{Id = 3, Name = "On Hold"},
            new ProjectStatus{Id = 4, Name = "Cancelled"}
        };
        ProjectTypes = new List<ProjectType>{
            new ProjectType{Id = 1, Name = "Internal"},
            new ProjectType{Id = 2, Name = "External"},
            new ProjectType{Id = 3, Name = "R&D"},
            new ProjectType{Id = 4, Name = "Maintenance"},
            new ProjectType{Id = 5, Name = "Development"}
        
        };
        RequestStatuses = new List<RequestStatus>{
            new RequestStatus{Id = 1, Name = "New"},
            new RequestStatus{Id = 2, Name = "Approved"},
            new RequestStatus{Id = 3, Name = "Rejected"},
            new RequestStatus{Id = 4, Name = "Cancelled"}
        
        };
        Subdivisions = new List<Subdivision>{
            new Subdivision{Id = 1, Name = "Development"},
            new Subdivision{Id = 2, Name = "HR"},
            new Subdivision{Id = 3, Name = "QA"},
            new Subdivision{Id = 4, Name = "Management"}
        };
    }
}