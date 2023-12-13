using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShipStation4Net.Clients.Interfaces
{
    public interface IGetsResourceUrlResponses<T>
    {
        Task<IList<T>> GetResourceResponsesAsync(string resourceUrl);
    }
}
