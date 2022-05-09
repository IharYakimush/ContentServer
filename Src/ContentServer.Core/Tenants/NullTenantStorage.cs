namespace ContentServer.Core.Tenants
{
    public class NullTenantStorage : ITenantStorage
    {
        public static readonly NullTenantStorage Instance = new NullTenantStorage();
        public Task<Tenant> FindAsync(string tenantId)
        {
            return Task.FromResult(new Tenant() { Id = tenantId, Name = tenantId });
        }
    }
}
