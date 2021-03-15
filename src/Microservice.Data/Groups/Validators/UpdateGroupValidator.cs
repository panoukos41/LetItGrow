﻿using FluentValidation;
using LetItGrow.Microservice.Data.Groups.Requests;

namespace LetItGrow.Microservice.Data.Groups.Validators
{
    /// <summary>
    /// Validates a <see cref="UpdateGroup"/> object.
    /// </summary>
    public class UpdateGroupValidator : AbstractValidator<UpdateGroup>
    {
        public UpdateGroupValidator()
        {
            RuleFor(x => x.Id)
                .ValidId();

            RuleFor(x => x.ConcurrencyStamp)
                .ValidConcurrencyStamp();

            RuleFor(x => x.Name!)
                .ValidName()
                    .When(x => x.Name is not null);

            RuleFor(x => x.Description!)
                .ValidDescription()
                    .When(x => x.Description is not null);

            RuleFor(x => x.Type)
                .IsInEnum();

            //RuleFor(x => x.Type!)
            //    .ValidGroupType()
            //        .When(x => x.Type is not null);
        }
    }
}