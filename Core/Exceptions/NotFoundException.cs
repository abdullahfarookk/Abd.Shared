namespace Abd.Shared.Core.Exceptions;

public class NotFoundException : QuickException
{
    public NotFoundException(string description) : base("Not Found",description, 404) { }
}
public static class NotFoundCustomException
{
    // To Do: convert 'this string' to 'this GuidEntity' for NotFound and others like this.
    public static NotFoundException NotFound(this string entity, object id)
        => new NotFoundException($" {entity} Not Found against Id: {id.ToString()}");
}