using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xrm.Sdk;
using SW365.OSIRIS.TableModel;
using SW365.OSIRIS.DataAccess;
using System.Web;


namespace SW365.OSIRIS.Processors
{
    public class MembershipProcessor : AbstractProcessor
    {
        SubscriptionTypeDataAccess subscriptionTypeDataAccess;
        SubscriptionDataAccess subscriptionDataAccess;
        public MembershipProcessor(IOrganizationService organizationService, ITracingService tracingService) : base(organizationService, tracingService) 
        {
            subscriptionTypeDataAccess = new SubscriptionTypeDataAccess(organizationService);
            subscriptionDataAccess = new SubscriptionDataAccess(organizationService);
        }

        public void GenerateSubscriptionAndPayments(Account membership) 
        {
            
        }

        private void CreateSubscription(Account membership) 
        {
            // Check for pending existging subscriptions
            var pendingSubscriptions = subscriptionDataAccess.GetPendingSubscriptions(membership.Id);
            if (pendingSubscriptions.Count > 0)
            {
                this._tracingService.Trace($"MembershipProcessor | CreateSubscription | There are exising pending subscriptions: { pendingSubscriptions.Count}");
                return;
            }
            // Get the subscription type
            this._tracingService.Trace($"MembershipProcessor | CreateSubscription | Get Subscription Type");
            var subscriptiontype = subscriptionTypeDataAccess.GetEntity(membership.sw365_subscriptiontype.Id);

            // Create the subscription record
            this._tracingService.Trace($"MembershipProcessor | CreateSubscription | Create Subscription");
            var subscription = new sw365_subscription()
            {
                sw365_membership = new EntityReference(Account.EntityLogicalName, membership.Id),
                sw365_subscriptionstartdate = DateTime.Now,
                sw365_subscriptionenddate = DateTime.Now.AddYears(1),
                sw365_subscriptionprice = new Money(subscriptiontype.sw365_subscriptionfee.Value * 12.0m),
                StatusCode = sw365_subscription_StatusCode.PendingPayment
            };

            // Create the subscription record
            this._tracingService.Trace($"MembershipProcessor | CreateSubscription | Create Subscription");
            subscriptionDataAccess.CreateEntity(subscription);
        }

        private void createPayment(sw365_subscription subscription)
        {
            // Create the payment record
            this._tracingService.Trace($"MembershipProcessor | CreatePayment | Create Payment");
            var payment = new sw365_payment()
            {
                sw365_subscription = new EntityReference(sw365_subscription.EntityLogicalName, subscription.Id),
                sw365_membership = new EntityReference(Account.EntityLogicalName, subscription.sw365_membership.Id),
                sw365_paymentduedate = DateTime.Now.AddDays(14),
                sw365_fee = subscription.sw365_subscriptionprice,
                StatusCode = sw365_payment_StatusCode.Pending
            };

            // Create the payment record
            this._tracingService.Trace($"MembershipProcessor | CreatePayment | Create Payment");
            
        }
    }
}
