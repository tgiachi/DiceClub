using System.Collections;

namespace DiceClub.Api.Data.Rest;

public class PaginatedRestResultObject<TData> 
{
    public List<TData> Result { get; set; }

    public string? Error { get; set; }

    public bool HaveError { get; set; }
    public int PageSize { get; set; }
    public int Page { get; set; }
    public int PageCount { get; set; }
    public long Count { get; set; }
}

public class PaginatedRestResultObjectBuilder<TData>
{
    private readonly PaginatedRestResultObject<TData> _restResult = new();

    public static PaginatedRestResultObjectBuilder<TData> Create()
    {
        return new PaginatedRestResultObjectBuilder<TData>();
    }

    public PaginatedRestResultObjectBuilder<TData> Total(long total)
    {
        _restResult.Count = total;
        return this;
    }

    public PaginatedRestResultObjectBuilder<TData> Page(int page)
    {
        _restResult.Page = page;
        return this;
    }

    public PaginatedRestResultObjectBuilder<TData> PageSize(int pageSize)
    {
        _restResult.PageSize = pageSize;
        return this;
    }

    public PaginatedRestResultObjectBuilder<TData> PageCount(int pageCount)
    {
        _restResult.PageCount = pageCount;
        return this;
    }

    public new PaginatedRestResultObjectBuilder<TData> Data(List<TData> data)
    {
        _restResult.Result = data;
        if (data is IList list)
        {
            _restResult.Count = list.Count;
        }

        return this;
    }

    public new PaginatedRestResultObjectBuilder<TData> Error(Exception ex)
    {
        _restResult.Error = ex.Message;
        _restResult.HaveError = true;
        return this;
    }

    public new PaginatedRestResultObject<TData> Build()
    {
        return _restResult;
    }
}