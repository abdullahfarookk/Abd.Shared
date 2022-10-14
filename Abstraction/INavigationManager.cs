namespace Abd.Shared.Abstraction;

public interface INavigationManager
{
    public string CurrentRoute { get; set; }
    void NavigateTo(string uri);
}
