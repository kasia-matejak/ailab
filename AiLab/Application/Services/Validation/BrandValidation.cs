using FluentValidation;
using AiLab.Application.Model.Dtos;

namespace AiLab.Application.Services.Validation;

public interface IBrandValidation
{
    IValidator<CreateBrandDto> CreateValidator { get; }
    IValidator<UpdateBrandDto> UpdateValidator { get; }
}

public class BrandValidation : IBrandValidation
{
    public IValidator<CreateBrandDto> CreateValidator { get; }
    public IValidator<UpdateBrandDto> UpdateValidator { get; }

    public BrandValidation()
    {
        CreateValidator = new CreateBrandDtoValidator();
        UpdateValidator = new UpdateBrandDtoValidator();
    }

    private class CreateBrandDtoValidator : AbstractValidator<CreateBrandDto>
    {
        public CreateBrandDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(200).WithMessage("Name must not exceed 200 characters");
        }
    }

    private class UpdateBrandDtoValidator : AbstractValidator<UpdateBrandDto>
    {
        public UpdateBrandDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(200).WithMessage("Name must not exceed 200 characters");
        }
    }
}
