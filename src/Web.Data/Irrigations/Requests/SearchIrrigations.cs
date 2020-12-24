using LetItGrow.Web.Data.Irrigations.Models;
using MediatR;
using System.Collections.Generic;

namespace LetItGrow.Web.Data.Irrigations.Requests
{
    public record SearchIrrigations : IRequest<List<IrrigationModel>>
    {
        // todo: search irrigations
    }
}