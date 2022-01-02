using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Providers.Cache.Remote
{
    public interface IRemoteCacher
    {
        void Add(string key,string data);
        bool Any(string key);
        string Get(string key);
        bool Remove(string key);
        void Clear();

    }
}
