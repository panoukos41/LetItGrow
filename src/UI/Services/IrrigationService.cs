using LetItGrow.UI.Services.Internal;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetItGrow.UI.Services
{
    public class IrrigationService : HubServiceBase, IIrrigationService
    {
        public IrrigationService(HubConnection hub) : base(hub)
        {
        }
    }
}
