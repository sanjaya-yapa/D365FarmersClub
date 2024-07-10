using Microsoft.Xrm.Sdk;
using SW365.OSIRIS.TableModel;
using SW365.OSIRIS.DataAccess;
using SW365.OSIRIS.Processors;
using System;

namespace SW365.OSIRI.Processors
{
    public class PaymentProcessor : AbstractProcessor
    {
        PaymentDataAccess paymentDataAccess;
        public PaymentProcessor(IOrganizationService organizationService, ITracingService tracingService) : base(organizationService, tracingService)
        {
            paymentDataAccess = new PaymentDataAccess(organizationService);
        }

        public void CreatePayment(sw365_subscription subscription)
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

        public void CreatePayment(sw365_subscription subscription, int installments) 
        {
            // Create the payment record
            this._tracingService.Trace($"MembershipProcessor | CreatePayment | Create Payment");
            var payment = new sw365_payment()
            {
                sw365_subscription = new EntityReference(sw365_subscription.EntityLogicalName, subscription.Id),
                sw365_membership = new EntityReference(Account.EntityLogicalName, subscription.sw365_membership.Id),
                sw365_paymentduedate = DateTime.Now.AddDays(14),
                sw365_fee = new Money(subscription.sw365_subscriptionprice.Value /Convert.ToDecimal(installments)),
                StatusCode = sw365_payment_StatusCode.Pending
            };

            // Create the payment record
            this._tracingService.Trace($"MembershipProcessor | CreatePayment | Create Payment");
            for (int i = 0; i < installments; i++)
            {
                paymentDataAccess.CreateEntity(payment);
            }
        }
    }
}
