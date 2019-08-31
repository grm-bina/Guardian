using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GuardianProj.Core.Interfaces
{
    interface IDataSuplier
    {
        Task<T> GetAsync<T>(string baseUrl, Dictionary<string, string> parameters);

    }
}
