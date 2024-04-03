using System;
using System.Collections.Generic;
using System.Text;
using SW365.OSIRIS.TableModel;
using Microsoft.Xrm.Sdk;

namespace SW365.OSIRIS.DataAccess
{
    public class SubscriptionDataAccess : DataAccessBase<sw365_subscription>
    {
        public SubscriptionDataAccess(IOrganizationService organizationService) : base(organizationService) { }
    }
}
