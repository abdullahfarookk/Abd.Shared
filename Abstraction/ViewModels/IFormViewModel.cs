namespace Abd.Shared.Abstraction.ViewModels;
public interface IFormViewModel:IViewModel
{
    void Submit();
    public bool FormLoading { get; set; }
    Func<object, string, Task<IEnumerable<string>>> ValidateValue { get; }
}
public interface IFormViewModel<out T> : IFormViewModel where T : IModel, new()
{
    T? Model { get; }
}