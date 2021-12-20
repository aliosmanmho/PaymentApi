using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Bussinies.Base.Repositories
{
    public class BaseRepo
    {
        protected ILogger<BaseRepo> _logger;
        public BaseRepo(ILogger<BaseRepo> logger)
        {
            _logger = logger;
        }
    }
}
