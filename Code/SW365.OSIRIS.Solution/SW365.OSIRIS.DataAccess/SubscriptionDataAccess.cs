using System;
using System.Collections.Generic;
using System.Text;
using SW365.OSIRIS.TableModel;
using Microsoft.Xrm.Sdk;
using System.Linq;

namespace SW365.OSIRIS.DataAccess
{
    public class SubscriptionDataAccess : DataAccessBase<sw365_subscription>
    {
        public SubscriptionDataAccess(IOrganizationService organizationService) : base(organizationService) { }

        public IList<sw365_subscription> GetPendingSubscriptions(Guid membershipId)
        {
            return _serviceContext.sw365_subscriptionSet.Where(s => s.sw365_membership.Id == membershipId)
                                                        .Where(s => s.StatusCode == sw365_subscription_StatusCode.PendingPayment).ToList();
        }
    }
}
