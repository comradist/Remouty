using OutOfOffice.MVC.Models.Employee;
using OutOfOffice.MVC.Services.Base;
using OutOfOffice.MVC.Shared.RequestFeatures;

namespace OutOfOffice.MVC.Models.Project
{
    public class ProjectIndexVM
    {
        public List<ProjectVM> Projects { get; set; }

        public MetaData MetaData { get; set; }

        public ProjectParameters ProjectParameters { get; set; }

        public List<ProjectType> ProjectTypes { get; set; }

        public List<ProjectStatus> ProjectStatuses { get; set; }

        public List<EmployeeVM> Employees { get; set; } // Assuming you need employees list for assigning Project Managers etc.
        
        public CreateProjectVM CreateProjectVM { get; set; }
    }
}
