using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xrm.Sdk;
using SW365.OSIRIS.TableModel;
using SW365.OSIRIS.DataAccess;
using System.Web;
using SW365.OSIRI.Processors;


namespace SW365.OSIRIS.Processors
{
    public class MembershipProcessor : AbstractProcessor
    {
        SubscriptionTypeDataAccess subscriptionTypeDataAccess;
        SubscriptionDataAccess subscriptionDataAccess;
        PaymentDataAccess paymentDataAccess;
        SubscriptionProcessor subscriptionProcessor;
        public MembershipProcessor(IOrganizationService organizationService, ITracingService tracingService) : base(organizationService, tracingService) 
        {
            subscriptionTypeDataAccess = new SubscriptionTypeDataAccess(organizationService);
            subscriptionDataAccess = new SubscriptionDataAccess(organizationService);
            paymentDataAccess = new PaymentDataAccess(organizationService);
            subscriptionProcessor = new SubscriptionProcessor(organizationService, tracingService);
        }

        public void GenerateSubscriptionAndPayments(Account membership) 
        {
            // Generate the subscription
            this._tracingService.Trace($"MembershipProcessor | GenerateSubscriptionAndPayments | Generate Subscription");
            var subscriptionId = subscriptionProcessor.CreateSubscription(membership);
            if (subscriptionId != null)
            {
                this._tracingService.Trace($"MembershipProcessor | GenerateSubscriptionAndPayments | subscriptionid: {subscriptionId}");
            }



            // Generate the payment
            this._tracingService.Trace($"MembershipProcessor | GenerateSubscriptionAndPayments | Generate Payment");
            
            
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
            paymentDataAccess.CreateEntity(payment);
        }
    }
}
