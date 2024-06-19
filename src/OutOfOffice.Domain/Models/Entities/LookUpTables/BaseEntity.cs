namespace OutOfOffice.Domain.Models.Entities.LookUpTables;

public abstract class BaseEntity
{
    public int ID { get; set; }

    public string Name { get; set; }
}