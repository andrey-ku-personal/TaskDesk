namespace TaskDesk.Shared.Models;

public class Error
{
    public Error(string message, string description)
    {
        Message = message;
        Description = description;
    }

    public string Message { get; set; }
    public string Description { get; set; }

    public override string ToString()
    {
        return $"Message {Message}, Description {Description}";
    }
}
