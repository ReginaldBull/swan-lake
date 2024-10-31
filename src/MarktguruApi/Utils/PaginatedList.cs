namespace MarktguruApi.Utils
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Represents a paginated list of items.
    /// </summary>
    /// <typeparam name="T">The type of items in the list.</typeparam>
    public class PaginatedList<T>
    {
        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the total number of pages.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the total count of items.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets a value indicating whether there is a previous page.
        /// </summary>
        public bool HasPrevious => CurrentPage > 1;

        /// <summary>
        /// Gets a value indicating whether there is a next page.
        /// </summary>
        public bool HasNext => CurrentPage < TotalPages;

        /// <summary>
        /// Gets or sets the list of items.
        /// </summary>
        public List<T> Items { get; set; } = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedList{T}"/> class.
        /// </summary>
        /// <param name="items">The list of items.</param>
        /// <param name="count">The total count of items.</param>
        /// <param name="pageNumber">The current page number.</param>
        /// <param name="pageSize">The size of the page.</param>
        private PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items.AddRange(items);
        }

        /// <summary>
        /// Creates a paginated list from a source queryable asynchronously.
        /// </summary>
        /// <param name="source">The source queryable.</param>
        /// <param name="pageNumber">The current page number.</param>
        /// <param name="pageSize">The size of the page.</param>
        /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the paginated list.</returns>
        public static async Task<PaginatedList<T>> ToPaginatedList(IQueryable<T> source, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            int count = source.Count();
            List<T> items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }
    }
}