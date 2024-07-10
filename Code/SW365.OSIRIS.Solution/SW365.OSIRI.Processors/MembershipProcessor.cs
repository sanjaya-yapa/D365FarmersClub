using Microsoft.Xrm.Sdk;
using SW365.OSIRIS.TableModel;
using SW365.OSIRIS.DataAccess;
using SW365.OSIRI.Processors;
using System;


namespace SW365.OSIRIS.Processors
{
    public class MembershipProcessor : AbstractProcessor
    {
        SubscriptionDataAccess subscriptionDataAccess;
        SubscriptionProcessor subscriptionProcessor;
        PaymentProcessor paymentProcessor;
        public MembershipProcessor(IOrganizationService organizationService, ITracingService tracingService) : base(organizationService, tracingService) 
        {
            subscriptionDataAccess = new SubscriptionDataAccess(organizationService);
            subscriptionProcessor = new SubscriptionProcessor(organizationService, tracingService);
            paymentProcessor = new PaymentProcessor(organizationService, tracingService);
        }

        public void GenerateSubscriptionAndPayments(Account membership) 
        {
            // Generate the subscription
            this._tracingService.Trace($"MembershipProcessor | GenerateSubscriptionAndPayments | Generate Subscription and Payments");
            Guid? subscriptionId = CreateAndTraceSubscription(membership);
            if (subscriptionId.HasValue) 
                ProcessPaymentBaseOptions(membership, subscriptionId.Value);
            this._tracingService.Trace($"MembershipProcessor | GenerateSubscriptionAndPayments | Process completed successfully");

        }

        private Guid? CreateAndTraceSubscription(Account membership) 
        {
            this._tracingService.Trace($"MembershipProcessor | CreateAndTraceSubscription | Creating Subscription");
            Guid? subscriptionId = subscriptionProcessor.CreateSubscription(membership);

            if(subscriptionId.HasValue)
                this._tracingService.Trace($"MembershipProcessor | CreateAndTraceSubscription | Subscription created successfully: {subscriptionId}");
            else
                this._tracingService.Trace($"MembershipProcessor | CreateAndTraceSubscription | Subscription creation failed.");
            return subscriptionId;
        }

        private void ProcessPaymentBaseOptions(Account membership, Guid subscriptionId) 
        {
            var subscription = subscriptionDataAccess.GetEntity(subscriptionId);
            if (subscription == null) 
            {
                _tracingService.Trace($"MembershipProcessor | ProcessPaymentBaseOptions | Failed to retrieve subscription for id: {subscriptionId}");
                return;
            }

            switch (membership.sw365_paymentoption) 
            {
                case sw365_PaymentOptions.FullPayment:
                    _tracingService.Trace($"MembershipProcessor | ProcessPaymentBaseOptions | Generate Payment record for fullpayment");
                    paymentProcessor.CreatePayment(subscription);
                    break;
                case sw365_PaymentOptions.MonthlyPayment:
                    _tracingService.Trace($"MembershipProcessor | ProcessPaymentBaseOptions | Generate Payment record for monthly installments");
                    paymentProcessor.CreatePayment(subscription, 12);
                    break;
            }
        }
       
    }
}
