

using StrawberryShake;

namespace Tests;

public class ResponseMappingTests
{
    [Fact]
    public void Should_MapResponse_Enumerable()
    {
        // Arrange
        IOperationResult data = new OperationResult
        {
            Errors = Array.Empty<IClientError>(),
            Data = new
            {
                Banners = new Banner1[]
                {
                    new()
                    {
                        Id = 1,
                        Name = "Banner 1",
                    },
                    new()
                    {
                        Id = 2,
                        Name = "Banner 2",
                    },
                }
            }
        };
        
        // Act
        var resutlObs = Observable.Return(data).MapEnumerable<Banner2>();
        
        // Assert
        var result = resutlObs.LastOrDefault();
        result?.Value?.Should().NotBeNull();
        result?.Value?.Count().Should().Be(2);
        result?.Value?.First().Id.Should().Be(1);
        result?.Value?.First().Name.Should().Be("Banner 1");
        result?.Value?.Last().Id.Should().Be(2);
        result?.Value?.Last().Name.Should().Be("Banner 2");

    }
}

public class OperationResult : IOperationResult
{
    public object? Data { get; set; }
    public Type DataType { get; }
    public IOperationResultDataInfo? DataInfo { get; }
    public object DataFactory { get; }
    public IReadOnlyList<IClientError> Errors { get; set; }
    public IReadOnlyDictionary<string, object?> Extensions { get; }
    public IReadOnlyDictionary<string, object?> ContextData { get; }
}

public class Banner
{
    // id,name
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Banner1 : Banner
{
    
}
public class Banner2:Banner
{
    public Banner2(Banner1 banner1)
    {
        Id = banner1.Id;
        Name = banner1.Name;
    }
}