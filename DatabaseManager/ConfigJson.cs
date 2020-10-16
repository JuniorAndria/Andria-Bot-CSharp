using Newtonsoft.Json;

namespace DatabaseManager
{
    public struct ConfigJson
    {
        [JsonProperty("arquivoBanco")]
        public string ArquivoBanco { get; private set; }
    }
}
