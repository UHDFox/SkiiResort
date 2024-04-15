namespace SkiiResort.Web.Contracts.CommonResponses;

public class UpdatedResponse
{
    public Guid Id { get; set; }

    public UpdatedResponse(Guid id)
    {
        Id = id;
    }
}
