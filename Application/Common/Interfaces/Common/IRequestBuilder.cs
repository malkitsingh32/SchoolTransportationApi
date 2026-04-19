using Application.Common.Dtos;

namespace Application.Common.Interfaces.Common
{
    public interface IRequestBuilder
    {
        void AssignRequest(ServerRowsRequest request);
        IRequestBuilder GetRequestBuilder(ServerRowsRequest request);
        string GetFilters();
        string GetSorts();
        int GetPageIndex();
        int GetPageSize();
    }
}
