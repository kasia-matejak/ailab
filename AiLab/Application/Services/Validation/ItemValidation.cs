using FluentValidation;
using AiLab.Application.Model.Dtos;

namespace AiLab.Application.Services.Validation;

public interface IItemValidation
{
    IValidator<CreateItemDto> CreateValidator { get; }
    IValidator<UpdateItemDto> UpdateValidator { get; }
}

public class ItemValidation : IItemValidation
{
    public IValidator<CreateItemDto> CreateValidator { get; }
    public IValidator<UpdateItemDto> UpdateValidator { get; }

    public ItemValidation()
    {
        CreateValidator = new CreateItemDtoValidator();
        UpdateValidator = new UpdateItemDtoValidator();
    }

    private class CreateItemDtoValidator : AbstractValidator<CreateItemDto>
    {
        public CreateItemDtoValidator()
        {
            RuleFor(x => x.BrandId).Must(id => id > 0).WithMessage("BrandId must be greater than 0");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").MaximumLength(200);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
        }
    }

    private class UpdateItemDtoValidator : AbstractValidator<UpdateItemDto>
    {
        public UpdateItemDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").MaximumLength(200);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
        }
    }
}
