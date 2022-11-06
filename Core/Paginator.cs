// ReSharper disable ConstantConditionalAccessQualifier
namespace Abd.Shared.Core;

public sealed class Paginator:IPaginator
    {
        public Paginator(byte? pageSize = null)
        {
            if (pageSize is { })
            {
                First = pageSize;
                PageSize = pageSize.Value;
            }
            else
            {
                First = PageSize;
            }
            After = null;
            Last = null;
            Before = null;
        }
        public int? First { get; private set; }
        public int? Last { get; private set; }
        public string? Before { get; private set; }
        public string? After { get; private set; }
        public bool HasPreviousPage { get; private set; }
        public bool HasNextPage { get; private set; }
        public string? StartCursor { get; private set; }
        public string? EndCursor { get; private set; }
        public int? TotalCount { get; set; }
        public byte PageSize { get; private set; } = 5;
        public byte PageIndex { get; private set; }
        
        private void ResetIndex()
        {
            if (PageIndex > 0)
            {
                First = PageSize; 
                Before = null; 
                After = null;
                Last = null;
            }
            OnPropertyChanged();
        }

        public void ChangeSize(byte pageSize)
        {
            First = pageSize; 
            Before = null; 
            After = null;
            Last = null;
            PageSize = pageSize;
            OnPropertyChanged();
        }

        public void FirstPage()
        {
            First = PageSize; 
            Before = null; 
            After = null;
            Last = null;
            PageIndex = 0;
            OnPropertyChanged();
        }
        public void LastPage()
        {
            First = null; 
            Before = null; 
            After = null;
            Last = PageSize;
            PageIndex = (byte)(TotalCount??1 / PageSize);
            OnPropertyChanged();
        }
        

        public void NextPage()
        {
            if (!HasNextPage) return;
            
            First = PageSize; 
            Before = null; 
            After = EndCursor;
            Last = null;
            PageIndex++;
            OnPropertyChanged();
        }


        public void PrevPage()
        {
            if (!HasPreviousPage) return;
            
            First = null; 
            Before = StartCursor;
            After = null;
            Last = PageSize;
            PageIndex--;
            OnPropertyChanged();
        }

        public void ChangePageResult(IPageResult? pageResult)
        {
            // map pageResult.PageInfo to this
            var pageInfo = pageResult?.PageInfo;
            // TODO: map pageInfo to this
            HasNextPage = pageInfo?.HasNextPage ?? false;
            HasPreviousPage = pageInfo?.HasPreviousPage ?? false;
            StartCursor = pageInfo?.StartCursor;
            EndCursor = pageInfo?.EndCursor;
            TotalCount = pageResult?.TotalCount;
        }

        public void ChangePageInfo(IPageResult? pageResult)
        {
            var pageInfo = pageResult?.PageInfo;
            HasNextPage = pageInfo?.HasNextPage??false;
            HasPreviousPage = pageInfo?.HasPreviousPage??false;
            StartCursor = pageInfo?.StartCursor;
            EndCursor = pageInfo?.EndCursor;
            TotalCount = pageResult?.TotalCount;
            
        }
        public void ChangePageInfo(IPageInfo? pageInfo, int? totalCount = null)
        {
            HasNextPage = pageInfo?.HasNextPage??false;
            HasPreviousPage = pageInfo?.HasPreviousPage??false;
            StartCursor = pageInfo?.StartCursor;
            EndCursor = pageInfo?.EndCursor;
            TotalCount = totalCount;
        }

        public List<byte> PageSizeOptions => new (){ 2, 5, 10, 20, 30 };

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
