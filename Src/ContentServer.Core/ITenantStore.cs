﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentServer.Core
{
    public interface ITenantStore
    {
        Tenant Find(string tenantId);
    }
}
