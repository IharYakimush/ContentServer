namespace ContentServer.Core.Tenants
{
    public interface ITenantStorage
    {
        Task<Tenant> FindAsync(string tenantId);
    }
}
