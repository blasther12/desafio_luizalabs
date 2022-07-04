using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteLuizaLabs.Infrastruture.CrossCutting.Interfaces
{
    public interface IHttp
    {
        Task<T?> Get<T>(string url);
    }
}
