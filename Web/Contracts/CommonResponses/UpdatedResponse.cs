using System;

namespace Web.Contracts.CommonResponses;

public sealed class UpdatedResponse
{
    public UpdatedResponse(Guid id, bool isSuccessful)
    {
        Id = id;
        IsSuccessful = isSuccessful;
    }

    public Guid Id { get; set; }
    
    public bool IsSuccessful { get; set; }
}