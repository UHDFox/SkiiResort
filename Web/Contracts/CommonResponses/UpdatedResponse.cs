namespace Web.Contracts;

public class UpdatedResponse
{
    public Guid Id { get; set; }

    public UpdatedResponse(Guid id)
    {
        Id = id;
    }
}