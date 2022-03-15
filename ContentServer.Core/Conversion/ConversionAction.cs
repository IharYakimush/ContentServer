namespace ContentServer.Core.Conversion
{
    public abstract class ConversionAction
    {
        public abstract string Name { get; }
        public abstract IReadOnlyCollection<ParamDefinition> SupportedParams { get; }
        public abstract IReadOnlySet<string> InputFormats { get; }
        public virtual int MinInputCount { get; } = 1;
        public virtual int MaxInputCount { get; } = 1;
        public abstract string OutputFormat(string inputFormat, IReadOnlyDictionary<ParamDefinition, string> actualParams);
    }
}
