namespace WorldCitiesAPI.Data
{
    public class PaginationQuery
    {
        /// <summary>
        /// Zero-based index of current page.
        /// </summary>
        public int PageIndex { get; set; } = 0;

        /// <summary>
        /// Number of items contained in each page.
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Sorting Column name (or null if none set)
        /// </summary>
        public string? SortColumn { get; set; }

        /// <summary>
        /// Sorting Order ("ASC", "DESC" or null if none set)
        /// </summary>
        public string? SortOrder { get; set; }

        /// <summary>
        /// Filter Column name (or null if none set)
        /// </summary>
        public string? FilterColumn { get; set; }

        /// <summary>
        /// Filter Query string 
        /// (to be used within the given FilterColumn)
        /// </summary>
        public string? FilterQuery { get; set; }
    }
}
