using System.ComponentModel.DataAnnotations;

namespace Ecom.Core.Sharing
{
    public class ProductParams
    {

        public int MaxPageSize { get; set; } = 15;

        private int _pageSize { get; set; } = 3;

        public int PageSize { get { return _pageSize; } set { _pageSize = value > MaxPageSize ? _pageSize : value; } }
        public int PageNumber { get; set; } = 1;
        public string? sort { get; set; }
        public int? CategoryId { get; set; }

        private string? _Search { get; set; }

        public string? Search
        {
            get { return _Search; }
            set { _Search = value?.ToLower(); }
        }
    }
}