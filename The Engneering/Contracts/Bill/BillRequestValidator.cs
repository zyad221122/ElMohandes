namespace The_Engneering.Contracts.Bill;

public class BillRequestValidator : AbstractValidator<BillRequest>
{
    public BillRequestValidator()
    {
        RuleFor(l => l.CustomerName)
            .NotEmpty()
            .WithMessage("اسم العميل مطلوب.")
            .MaximumLength(50)
            .WithMessage("يجب ألا يزيد اسم العميل عن 50 حرفًا.");

        RuleFor(l => l.CustomerPhone)
            .NotEmpty()
            .WithMessage("رقم الهاتف مطلوب.")
            .Matches(@"^01\d{9}$")
            .WithMessage("يجب أن يبدأ رقم الهاتف بـ '01' ويتكون من 11 رقمًا.");

        RuleFor(l => l.PayType)
            .NotEmpty()
            .WithMessage("طريقة الدفع مطلوبة.")
            .Must(payType => payType == "كاش" || payType == "إنستاباي")
            .WithMessage("طريقة الدفع يجب أن تكون إما 'كاش' أو 'إنستاباي'.");

        RuleFor(l => l.Amount)
            .GreaterThanOrEqualTo(1)
            .WithMessage("يجب أن تكون الكمية على الأقل 1");

       
    }
}
