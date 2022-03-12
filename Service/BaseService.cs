using Abd.Shared.Core.Validation;

namespace Abd.Shared.Core.Services;

public abstract class BaseService:IService
{
    
    private readonly IValidationService _validationService;

    protected BaseService(IValidationService validationService)
    {
        _validationService = validationService;
    }
      

    public IValidationResult Validate(object model)
    => _validationService.Validate(model);
}