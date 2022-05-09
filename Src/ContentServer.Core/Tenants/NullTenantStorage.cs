namespace ContentServer.Core.Tenants
{
    public class NullTenantStorage : ITenantStorage
    {
        public static readonly NullTenantStorage Instance = new NullTenantStorage();

        public Task<bool> TryFindAsync(string tenantId, out Tenant? result)
        {
            result = new Tenant() { Id = tenantId, Name = tenantId };
            return Task.FromResult(true);
        }
    }
}
