namespace OutOfOffice.MVC.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException(string message, Guid id) : base($"{message} can`t be find with this key {id.ToString()}")
    {

    }
}