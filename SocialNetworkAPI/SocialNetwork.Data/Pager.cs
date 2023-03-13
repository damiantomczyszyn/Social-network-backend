using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SocialNetwork.Data
{
    public class Pager
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        [BindNever]
        public int Offset => (PageIndex - 1) * PageSize;

        public Pager(int pageIndex, int pageSize = 20)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public Pager() : this(1, 20)
        {
        }
    }

    public static class PagerExtensions
    {
        #region Paginate()
        public static IQueryable<TModel> Paginate<TModel>(this IQueryable<TModel> query, Pager pager) where TModel : class
        {
            query = query
                .Skip<TModel>(pager.Offset)
                .Take<TModel>(pager.PageSize);

            return query;
        }
        #endregion
    }
}
