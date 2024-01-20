using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EcoFarm.Domain.Common.Values.Enums.HelperEnums;

namespace EcoFarm.UseCases.Products.Create
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Mã sản phẩm không được để trống")
                .MaximumLength(10).WithMessage("Mã sản phẩm có độ dài tối đa 10 ký tự");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên sản phẩm không được để trống")
                .MaximumLength(100).WithMessage("Tên sản phẩm có độ dài tối đa 100 ký tự");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Mô tả sản phẩm không được để trống");
            //RuleFor(x => x)
            //    .Must(MustHaveCurrency).WithMessage("Mệnh giá không được để trống.");
        }

        public bool MustHaveCurrency(CreateProductCommand command)
        {
            if (command.Currency.HasValue) return true;
            if (command.Price.HasValue || command.PriceForRegistered.HasValue) return false;
            return true;
        }
    }
}
