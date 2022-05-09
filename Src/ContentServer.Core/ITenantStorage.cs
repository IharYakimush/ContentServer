namespace ContentServer.Core
{
    public interface ITenantStorage
    {
        Task<Tenant> Find(string tenantId);
    }
}
