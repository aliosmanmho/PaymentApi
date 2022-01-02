using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Providers.Cache.Remote.Redis
{
    public class RedisServer : IRemoteCacher
    {
        private ConnectionMultiplexer _connectionMultiplexer;
        private IDatabase _database;
        private string configurationStrign;
        private int _currentDatabaseId = 0;
        private Uri redisUri;
        //private ConfigurationOptions sentinelconfiguration;
        public RedisServer()
        {
            //sentinelconfiguration = new()
            //{
            //    AllowAdmin = true,
            //    ServiceName = "redismaster",
            //    KeepAlive = 86400,
            //    CommandMap = CommandMap.Create(new HashSet<string>
            //    { "GET", "SET", "EXISTS", "SETEX", "SELECT" }, available: true),
            //    AbortOnConnectFail = false,
            //    ConnectRetry = 5,
            //    Ssl = true,
            //    ConnectTimeout = 5000,
            //    SslProtocols = System.Security.Authentication.SslProtocols.Tls12| System.Security.Authentication.SslProtocols.Tls11 | System.Security.Authentication.SslProtocols.Tls,
            //    SyncTimeout = 5000,
            //    ReconnectRetryPolicy = new LinearRetry(5000),
            //    EndPoints = { { "ec2-34-251-250-166.eu-west-1.compute.amazonaws.com", 9440 } },
            //    Password = "pfcd30796e1f37333aac0132b22bd8f276a1da58c1e4807b63f6d09149f600670"
            //};
            //var redisConnString = $"{redisUri.Host}:{redisUri.Port},abortConnect=False,ssl=True,sslprotocols=tls12,resolveDns=True,password={userInfo[1]}";

            configurationStrign = "redis://:pfcd30796e1f37333aac0132b22bd8f276a1da58c1e4807b63f6d09149f600670@ec2-34-251-250-166.eu-west-1.compute.amazonaws.com:9440";
            redisUri = new Uri(configurationStrign);
            var userInfo = redisUri.UserInfo.Split(':');
            var redisConnString = $"{redisUri.Host}:{redisUri.Port},password={userInfo[1]}";
            Console.WriteLine($"constructed redisConnString: {redisConnString}");

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
    }
}
