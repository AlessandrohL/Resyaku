namespace Resyaku.Domain.Primitives
{
    public abstract class PaginationParameters
    {
        private const int MaxPageSize = 20;
        private int _minPage = 1;
        private int _pageSize = 10;
        public int Page
        {
            get
            {
                return _minPage;
            }
            set
            {
                _minPage = value < _minPage ? _minPage : value;
            }
        }

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value > MaxPageSize ? MaxPageSize : value;
            }
        }
    }
}
