using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xrm.Sdk;
using SW365.OSIRIS.TableModel;
using SW365.OSIRIS.DataAccess;
using System.Web;
using SW365.OSIRIS.Processors;
using System.ComponentModel.Design.Serialization;

namespace SW365.OSIRI.Processors
{
    public class SubscriptionProcessor : AbstractProcessor
    {
        SubscriptionTypeDataAccess subscriptionTypeDataAccess;
        SubscriptionDataAccess subscriptionDataAccess;
        PaymentDataAccess paymentDataAccess;
        public SubscriptionProcessor(IOrganizationService organizationService, ITracingService tracingService) : base(organizationService, tracingService)
        {
            subscriptionTypeDataAccess = new SubscriptionTypeDataAccess(organizationService);
            subscriptionDataAccess = new SubscriptionDataAccess(organizationService);
            paymentDataAccess = new PaymentDataAccess(organizationService);
        }

        public Guid CreateSubscription(Account membership)
        {
            // Check for pending existging subscriptions
            var pendingSubscriptions = subscriptionDataAccess.GetPendingSubscriptions(membership.Id);
            if (pendingSubscriptions.Count > 0)
            {
                this._tracingService.Trace($"SubscriptionProcessor | CreateSubscription | There are exising pending subscriptions: {pendingSubscriptions.Count}");
                return Guid.Empty;
            }
            // Get the subscription type
            this._tracingService.Trace($"SubscriptionProcessor | CreateSubscription | Get Subscription Type");
            var subscriptiontype = subscriptionTypeDataAccess.GetEntity(membership.sw365_subscriptiontype.Id);

            // Create the subscription record
            this._tracingService.Trace($"SubscriptionProcessor | CreateSubscription | Create Subscription");
            var subscription = new sw365_subscription();
            subscription.sw365_membership = new EntityReference(Account.EntityLogicalName, membership.Id);
            subscription.sw365_subscriptionstartdate = DateTime.Now;
            subscription.sw365_subscriptionenddate = DateTime.Now.AddYears(1);
            if (membership.sw365_paymentoption == sw365_PaymentOptions.FullPayment) 
            {
                subscription.sw365_subscriptionprice = new Money(subscriptiontype.sw365_subscriptionfee.Value * 12.0m);
            }
            subscription.StatusCode = sw365_subscription_StatusCode.PendingPayment;


            // Create the subscription record
            this._tracingService.Trace($"SubscriptionProcessor | CreateSubscription | Create Subscription");
            return subscriptionDataAccess.CreateEntityReturnId(subscription);
        }
    }
}
