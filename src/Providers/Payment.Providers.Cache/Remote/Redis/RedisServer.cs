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
        private Uri redisUri;
        public RedisServer(string config)
        {
            if (string.IsNullOrEmpty(config))
                throw new Exception("Redis Config Not Initilize!");
            configurationStrign = config;
            redisUri = new Uri(configurationStrign);
            var userInfo = redisUri.UserInfo.Split(':');
            var redisConnString = $"{redisUri.Host}:{redisUri.Port},allowAdmin=true,password={userInfo[1]}";

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
            _connectionMultiplexer.GetServer(redisUri.Host,redisUri.Port).FlushDatabase(_currentDatabaseId);
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
            return _connectionMultiplexer.GetServer(redisUri.Host, redisUri.Port).Keys().Count();
        }
    }
}
