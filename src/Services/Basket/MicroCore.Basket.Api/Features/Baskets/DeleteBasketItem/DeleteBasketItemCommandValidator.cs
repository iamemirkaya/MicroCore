using FluentValidation;

namespace MicroCore.Basket.Api.Features.Baskets.DeleteBasketItem;

public class DeleteBasketItemCommandValidator : AbstractValidator<DeleteBasketItemCommand>
{
    public DeleteBasketItemCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("CourseId is required");
    }
}