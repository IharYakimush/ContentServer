namespace ContentServer.Core.Tenants
{
    public class InMemoryTenantStorage : ITenantStorage
    {
        private readonly Dictionary<string, Tenant> _items = new();

        public Task<bool> TryFindAsync(string tenantId, out Tenant? result)
        {
            result = null;

            if (this._items.ContainsKey(tenantId))
            {
                result = this._items[tenantId];
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
