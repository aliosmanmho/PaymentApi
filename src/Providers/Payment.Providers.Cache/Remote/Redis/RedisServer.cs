using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Providers.Cache.Remote.Redis
{
    public class RedisServer : IRemoteCacher
    {
        private ConnectionMultiplexer _connectionMultiplexer;
        private IDatabase _database;
        private string configurationStrign;
        private int _currentDatabaseId = 1;
        private RedisConfig _config;
        public RedisServer(RedisConfig config)
        {
            if (default(RedisConfig) == config)
                throw new Exception("Redis Config Not Initilize!");
            _config = config;

            var redisConnString = $"{_config.Host}:{_config.Port},allowAdmin=true,password={_config.Password}";

            _connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnString);
            _database = _connectionMultiplexer.GetDatabase(_currentDatabaseId);
        }
        public IDatabase Database => _database;

        public void Add(string key, string data)
        {
            _database.StringSet(key, data);
        }

        public bool Any(string key)
        {
            return _database.KeyExists(key);
        }

        public void Clear()
        {
            _connectionMultiplexer.GetServer(_config.Host,int.Parse(_config.Port)).FlushDatabase(_currentDatabaseId);
        }

        public string Get(string key)
        {
            if (Any(key))
            {
                return _database.StringGet(key);
            }
            return string.Empty;
        }

        public bool Remove(string key)
        {
            return _database.KeyDelete(key);
        }

        public int Count()
        {
            return _connectionMultiplexer.GetServer(_config.Host, int.Parse(_config.Port)).Keys().Count();
        }
    }
}
