using System;
using System.Collections.Generic;
using System.Text;
using SW365.OSIRIS.TableModel;
using Microsoft.Xrm.Sdk;

namespace SW365.OSIRIS.DataAccess
{
    public class SubscriptionTypeDataAccess : DataAccessBase<sw365_subscriptiontype>
    {
        public SubscriptionTypeDataAccess(IOrganizationService organizationService) : base(organizationService) { }
    }
}
