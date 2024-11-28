using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidatior:AbstractValidator<Writer>
    {
        public WriterValidatior()
        {
            RuleFor(x=>x.WriterName).NotEmpty().WithMessage("Yazar adı ve soyadı boş geçilemez");
            RuleFor(x=>x.WriterMail).NotEmpty().WithMessage("Mail adresi boş geçilemez");
            RuleFor(x => x.WriterName).MinimumLength(2).WithMessage("En az 2 karakter girişi olmalı");
            RuleFor(x => x.WriterName).MaximumLength(40).WithMessage("Lütfen en fazla 50 karakter girişi olmalı");
            //RuleFor(x => x.WriterPassword)
            //.NotEmpty().WithMessage("Şifre boş geçilemez.")
            //.MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalı.")
            //.Matches("[A-Z]").WithMessage("Şifre en az 1 büyük harf içermeli.")
            //.Matches("[a-z]").WithMessage("Şifre en az 1 küçük harf içermeli.")
            //.Matches("[0-9]").WithMessage("Şifre en az 1 sayı içermeli.");
        }
    }
}
