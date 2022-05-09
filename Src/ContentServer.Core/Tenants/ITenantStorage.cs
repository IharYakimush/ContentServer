namespace ContentServer.Core.Tenants
{
    public interface ITenantStorage
    {
        Task<bool> TryFindAsync(string tenantId, out Tenant? result);
    }
}
