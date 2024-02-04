using System.Collections.Generic;

namespace Web.Contracts.CommonResponses;

public sealed class GetAllResponse<T>
{
    public IReadOnlyCollection<T> List;
    public int TotalAmount;

    public GetAllResponse(IReadOnlyCollection<T> list, int totalAmount)
    {
        List = list;
        TotalAmount = totalAmount;
    }
}