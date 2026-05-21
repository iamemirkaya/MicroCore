using FluentValidation;

namespace MicroCore.Basket.Api.Features.Baskets.CheckoutBasket;

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("Ad alanı boş olamaz.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyad alanı boş olamaz.");
        RuleFor(x => x.Address).NotEmpty().WithMessage("Adres boş olamaz.");
        RuleFor(x => x.City).NotEmpty().WithMessage("Şehir boş olamaz.");
        RuleFor(x => x.CardNumber).NotEmpty().Length(16).WithMessage("Geçerli bir kart numarası giriniz.");
        RuleFor(x => x.CardHolderName).NotEmpty().WithMessage("Kart üzerindeki isim boş olamaz.");
        RuleFor(x => x.CVV).NotEmpty().Length(3).WithMessage("CVV 3 haneli olmalıdır.");
    }
}