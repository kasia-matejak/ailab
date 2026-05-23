using FluentValidation;
using AiLab.Application.Model;

namespace AiLab.Application.Services.Validation;

public interface IStockValidation
{
    IValidator<StockModel> StockValidator { get; }
}

public class StockValidation : IStockValidation
{
    public IValidator<StockModel> StockValidator { get; }

    public StockValidation()
    {
        StockValidator = new StockDtoValidator();
    }

    private class StockDtoValidator : AbstractValidator<StockModel>
    {
        public StockDtoValidator()
        {
            RuleFor(x => x.ItemId)
                .Must(id => id > 0).WithMessage("ItemId must be greater than 0");

            RuleFor(x => x.SizeId)
                .Must(id => id > 0).WithMessage("SizeId must be greater than 0");

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Quantity must be greater than or equal to 0");
        }
    }
}
