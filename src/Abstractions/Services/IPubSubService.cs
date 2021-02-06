using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetItGrow.Services
{
    public interface IPubSubService
    {
        Task Pub(string channel, string message);

        IObservable<string> Sub(string channel);
    }
}
