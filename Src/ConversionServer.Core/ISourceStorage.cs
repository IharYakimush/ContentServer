namespace ConversionServer.Core
{
    public interface ISourceStorage
    {
        IReadOnlyDictionary<string, FileDefinition> GetDefinitions(string tenantId, IEnumerable<string> ids);
    }
}
