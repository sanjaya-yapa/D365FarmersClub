using System;
using System.Collections.Generic;
using System.Linq;
using SW365.OSIRIS.TableModel;
using Microsoft.Xrm.Sdk;


namespace SW365.OSIRIS.Plugins.Tests.DataAccess
{
    public class SubscriptionDataAccessTest : DataAccessBaseTest<sw365_subscription>
    {
        public SubscriptionDataAccessTest(IOrganizationService organizationService) : base(organizationService) { }

        public IList<sw365_subscription> GetPendingSubscriptions(Guid membershipId)
        {
            return _serviceContext.sw365_subscriptionSet.Where(s => s.sw365_membership.Id == membershipId)
                                                        .Where(s => s.StatusCode == sw365_subscription_StatusCode.PendingPayment).ToList();
        }

        public sw365_subscription GetSubscriptionById(Guid subscriptionId) {
            return _serviceContext.sw365_subscriptionSet.Where(s => s.sw365_subscriptionId == subscriptionId).FirstOrDefault();
        }
    }
}
