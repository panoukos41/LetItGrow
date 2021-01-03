using LetItGrow.Microservice.Data.Irrigations.Models;
using MediatR;
using System.Collections.Generic;

namespace LetItGrow.Microservice.Data.Irrigations.Requests
{
    public record SearchIrrigations : IRequest<List<IrrigationModel>>
    {
        // todo: search irrigations
    }
}