namespace Abd.Shared.Core.Filters;

public class CommonFilter : IFilter
{
    private string? _searchTerm;
    public string? SearchTerm
    {
        get => _searchTerm;
        set
        {
            _searchTerm = value;
            OnPropertyChanged();
        }
    }

    private byte? _status;
    public byte? Status
    {
        get => _status;
        set
        {
            _status = value;
            OnPropertyChanged();
        }
    }

    public void Reset()
    {
        _searchTerm = null;
        _status = null;
        OnPropertyChanged();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}