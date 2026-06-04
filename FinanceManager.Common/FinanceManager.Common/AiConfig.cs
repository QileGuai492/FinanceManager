using FinanceManager.Common.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FinanceManager.Common
{
    public class AiConfig
    {
        public string Endpoint { get; set; } = "https://api.deepseek.com/chat/completions";
        public string ApiKey { get; set; } = "";
        public string Model { get; set; } = "deepseek-chat";

        private static readonly string _path = Path.Combine(
            Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
            "ai_config.json");

        public static AiConfig Load()
        {
            if (!File.Exists(_path))
                return new AiConfig();
            var json = File.ReadAllText(_path);
            var config = JsonConvert.DeserializeObject<AiConfig>(json) ?? new AiConfig();
            // API Key 解密
            if (!string.IsNullOrEmpty(config.ApiKey))
                config.ApiKey = EncryptionHelper.Decrypt(config.ApiKey);
            return config;
        }

        public void Save()
        {
            // API Key 加密后存储
            var encrypted = string.IsNullOrEmpty(ApiKey) ? "" : EncryptionHelper.Encrypt(ApiKey);
            var toSave = new { Endpoint, ApiKey = encrypted, Model };
            var json = JsonConvert.SerializeObject(toSave, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_path, json);
        }
    }
}
