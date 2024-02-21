using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Reflection;
namespace WorldCitiesAPI.Data
{
    public class ApiResult<T>
    {
        /// <summary>
        /// Private constructor called by the CreateAsync method.
        /// </summary>
        private ApiResult(
            List<T> data,
            int count,
            PaginationQuery query)
        {
            Data = data;
            PageIndex = query.PageIndex;
            PageSize = query.PageSize;
            TotalCount = count;
            TotalPages = (int)Math.Ceiling(count / (double)query.PageSize);
        }

        /// <summary>
        /// Pages and/or sorts a IQueryable source.
        /// </summary>
        /// <param name="source">An IQueryable source of generic 
        /// type</param>
        /// <param name="pageIndex">Zero-based current page index 
        /// (0 = first page)</param>
        /// <param name="pageSize">The actual size of each 
        /// page</param>
        /// <param name="query">An object containing infomation about pagination</param>
        /// <returns>
        /// A object containing the IQueryable paged/sorted result 
        /// and all the relevant paging/sorting navigation info.
        /// </returns>
        public static async Task<ApiResult<T>> CreateAsync(
            IQueryable<T> source, PaginationQuery query)
        {
            if (!string.IsNullOrEmpty(query.FilterColumn) 
                && !string.IsNullOrEmpty(query.FilterQuery)
                && IsValidProperty(query.FilterColumn))
            {
                source = source.Where(
                    string.Format("{0}.StartsWith(@0)", query.FilterColumn),
                    query.FilterQuery);
            }
            var count = await source.CountAsync();
            if (!string.IsNullOrEmpty(query.SortColumn)
                && IsValidProperty(query.SortColumn))
            {
                query.SortOrder = !string.IsNullOrEmpty(query.SortOrder)
                    && query.SortOrder.ToUpper() == "ASC"
                    ? "ASC"
                    : "DESC";
                source = source.OrderBy(
                    string.Format(
                        "{0} {1}",
                        query.SortColumn,
                        query.SortOrder)
                    );
            }
            source = source
                .Skip(query.PageIndex * query.PageSize)
                .Take(query.PageSize);

            var data = await source.ToListAsync();

            return new ApiResult<T>(
                data,
                count,
                query);
        }

        /// <summary>
        /// Checks if the given property name exists
        /// to protect against SQL injection attacks
        /// </summary>
        public static bool IsValidProperty(
            string propertyName,
            bool throwExceptionIfNotFound = true)
        {
            var prop = typeof(T).GetProperty(
                propertyName,
                BindingFlags.IgnoreCase |
                BindingFlags.Public |
                BindingFlags.Instance);
            if (prop == null && throwExceptionIfNotFound)
                throw new NotSupportedException(
                    string.Format(
                        $"ERROR: Property '{propertyName}' does not exist.")
                    );
            return prop != null;
        }

        /// <summary>
        /// The data result.
        /// </summary>
        public List<T> Data { get; private set; }

        /// <summary>
        /// Total items count
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// Zero-based index of current page.
        /// </summary>
        public int PageIndex { get; private set; }

        /// <summary>
        /// Number of items contained in each page.
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// Total pages count
        /// </summary>
        public int TotalPages { get; private set; }

        /// <summary>
        /// TRUE if the current page has a previous page, 
        /// FALSE otherwise.
        /// </summary>
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 0);
            }
        }

        /// <summary>
        /// TRUE if the current page has a next page, FALSE otherwise.
        /// </summary>
        public bool HasNextPage
        {
            get
            {
                return ((PageIndex + 1) < TotalPages);
            }
        }
    }
}
