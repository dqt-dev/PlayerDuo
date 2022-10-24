namespace PlayerDuo.DTOs.Common
{
    public class PagedResult<T>
    {
        private int _totalPage;
        public int TotalPage 
        { 
            get
            {
                return _totalPage;
            } 
            set
            {
                _totalPage = value < 1 ? 1 : value;
            } 
        }
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }
    }
}
