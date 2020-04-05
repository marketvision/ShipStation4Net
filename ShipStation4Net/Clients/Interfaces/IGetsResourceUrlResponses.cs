using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShipStation4Net.Clients.Interfaces
{
    internal interface IGetsResourceUrlResponses<T>
    {
        Task<IList<T>> GetResourceResponse(string resourceUrl);
    }
}
