﻿using FluentValidation;
using LetItGrow.Web.Data.Nodes.Requests;

namespace LetItGrow.Web.Data.Nodes.Validators
{
    /// <summary>
    /// Validates a <see cref="AddNodeToGroup"/> object.
    /// </summary>
    public class AddNodeToGroupValidator : AbstractValidator<AddNodeToGroup>
    {
        public AddNodeToGroupValidator()
        {
            RuleFor(x => x.Id)
                .ValidId();

            RuleFor(x => x.ConcurrencyStamp)
                .ValidConcurrencyStamp();

            RuleFor(x => x.GroupId)
                .ValidId();
        }
    }
}