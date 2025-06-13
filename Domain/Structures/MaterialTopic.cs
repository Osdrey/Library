namespace Library.Domain.Structures
{
    public readonly struct MaterialTopic
    {
        public string Topic { get; }

        public MaterialTopic(string materialTopic)
        {
            Topic = materialTopic;
        }
        public override string ToString() => Topic;
        public static MaterialTopic Mathematics => new("Mathematics");
        public static MaterialTopic Medicine => new("Medicine");
        public static MaterialTopic Engineering => new("Engineering");
        public static MaterialTopic Law => new("Law");
        public static MaterialTopic Sociology => new("Sociology");
        public static MaterialTopic Philosophy => new("Philosophy");
        public static MaterialTopic BusinessAndEconomics => new("BusinessAndEconomics");
        public static MaterialTopic History => new("History");
        public static MaterialTopic FromString(string topic)
        {
            return topic switch
            {
                "Mathematics" => Mathematics,
                "Medicine" => Medicine,
                "Engineering" => Engineering,
                "Law" => Law,
                "Sociology" => Sociology,
                "Philosophy" => Philosophy,
                "BusinessAndEconomics" => BusinessAndEconomics,
                "History" => History,
                _=> throw new ArgumentException($"MaterialTopic inválido: {topic}")
            };
        }
        public static bool TryParse(string topic, out MaterialTopic materialTopic)
        {
            materialTopic = default;
            try
            {
                materialTopic = FromString(topic);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static IReadOnlyList<MaterialTopic> GetAll() => new List<MaterialTopic>
        {
            Mathematics,
            Medicine,
            Engineering,
            Law,
            Sociology,
            Philosophy,
            BusinessAndEconomics,
            History
        };
    }
}
