using LetItGrow.Identity.Application.Models;
using LetItGrow.Identity.Common.Queries;

namespace LetItGrow.Identity.Application.Queries
{
    public record GetApplication : BaseGet<ApplicationModel>
    {
    }
}