namespace DiceClub.Api.Data.Rest;

public class PaginatedRestResultObject<TData> : RestResultObject<TData>
{
    public int Page { get; set; }
    public int PageCount { get; set; }
    public long Total { get; set; }
}