namespace ContentServer.Core
{
    public interface ITenantStore
    {
        Task<Tenant> Find(string tenantId);
    }
}
