namespace Web.Contracts.CommonResponses;

public sealed class CreatedResponse
{
    public CreatedResponse(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}