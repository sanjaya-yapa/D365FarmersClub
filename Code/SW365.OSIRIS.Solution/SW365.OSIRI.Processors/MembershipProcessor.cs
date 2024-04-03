using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xrm.Sdk;
using SW365.OSIRIS.TableModel;
using SW365.OSIRIS.DataAccess;


namespace SW365.OSIRIS.Processors
{
    public class MembershipProcessor : AbstractProcessor
    {
        MembershipDataAccess membershipDataAccess;
        SubscriptionTypeDataAccess subscriptionTypeDataAccess;
        SubscriptionDataAccess subscriptionDataAccess;
        public MembershipProcessor(IOrganizationService organizationService) : base(organizationService) 
        {
            membershipDataAccess = new MembershipDataAccess(organizationService);
            subscriptionTypeDataAccess = new SubscriptionTypeDataAccess(organizationService);
            subscriptionDataAccess = new SubscriptionDataAccess(organizationService);
        }

        public void GenerateSubscription(Account membership) 
        {
            // Get the subscription type
            var subscriptiontype = subscriptionTypeDataAccess.GetEntity(membership.sw365_subscriptiontype.Id);

            // Create the subscription record
            var subscription = new sw365_subscription()
            {
                sw365_membership = new EntityReference(Account.EntityLogicalName, membership.Id),
                sw365_subscriptionstartdate = DateTime.Now,
                sw365_subscriptionenddate = DateTime.Now.AddYears(1),
                sw365_subscriptionprice = new Money(subscriptiontype.sw365_subscriptionfee.Value * 12.0m)
            };

            // Create the payment record
            subscriptionDataAccess.CreateEntity(subscription);
        }
    }
}
