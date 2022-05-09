namespace ContentServer.Core
{
    public class NullTenantStorage : ITenantStorage
    {
        public static readonly NullTenantStorage Instance = new NullTenantStorage();
        public Task<Tenant> Find(string tenantId)
        {
            return Task.FromResult(new Tenant() { Id = tenantId, Name = tenantId });
        }
    }
}
