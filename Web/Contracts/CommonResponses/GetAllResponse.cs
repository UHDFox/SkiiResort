namespace Web.Contracts.CommonResponses;

public sealed class GetAllResponse<T>
{
    public GetAllResponse(IReadOnlyCollection<T> list, int totalAmount)
    {
        List = list;
        TotalAmount = totalAmount;
    }

    public IReadOnlyCollection<T> List { get; set; }

    public int TotalAmount { get; set; }
}