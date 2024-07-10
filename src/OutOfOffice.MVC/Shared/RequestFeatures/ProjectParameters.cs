using OutOfOffice.Shared.RequestFeatures;

namespace OutOfOffice.MVC.Shared.RequestFeatures
{
    public class ProjectParameters : MetaData
    {
        public Guid? Id { get; set; }

        public int? ProjectTypeId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Guid? ProjectManagerId { get; set; }

        public string? Comment { get; set; }

        public int? StatusId { get; set; }

        public string? OrderBy { get; set; }
    }
}