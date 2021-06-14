using LetItGrow.Microservice.Irrigation.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Services
{
    public interface INodeIrrigationsQueue
    {
        public void Queue(CreateIrrigation request);
    }
}
