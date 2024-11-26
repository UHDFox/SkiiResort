namespace Application;

public abstract class ServiceModel
{
    public ServiceModel(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
