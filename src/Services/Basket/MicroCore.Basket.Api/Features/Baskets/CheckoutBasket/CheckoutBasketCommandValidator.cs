using FluentValidation;

namespace MicroCore.Basket.Api.Features.Baskets.CheckoutBasket;


public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.Province).NotEmpty().WithMessage("İl/Şehir boş olamaz.");
        RuleFor(x => x.District).NotEmpty().WithMessage("İlçe boş olamaz.");
        RuleFor(x => x.Street).NotEmpty().WithMessage("Sokak boş olamaz.");
        RuleFor(x => x.ZipCode).NotEmpty().WithMessage("Posta kodu boş olamaz.");
        RuleFor(x => x.Line).NotEmpty().WithMessage("Açık adres boş olamaz.");
        RuleFor(x => x.CardNumber).NotEmpty().Length(16).WithMessage("Geçerli bir kart numarası giriniz.");
        RuleFor(x => x.CardHolderName).NotEmpty().WithMessage("Kart üzerindeki isim boş olamaz.");
        RuleFor(x => x.Expiration).NotEmpty().WithMessage("Son kullanma tarihi boş olamaz.");
        RuleFor(x => x.CVV).NotEmpty().Length(3).WithMessage("CVV 3 haneli olmalıdır.");
    }
}