namespace ClimaLocal.Domain.Models;

public class Log
{
    public int Id { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public string Level { get; set; }
    public string Message { get; set; }
}
