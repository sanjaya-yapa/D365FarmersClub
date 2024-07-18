using System;
using SW365.OSIRIS.TableModel;
using SW365.OSIRIS.Plugins.Tests.DataAccess;
using Microsoft.Xrm.Sdk;

namespace SW365.OSIRIS.Plugins.Tests.Processors
{
    public class PaymentProcessorTest : AbstractProcessorTest
    {
        PaymentDataAccessTest paymentDataAccess;
        public PaymentProcessorTest(IOrganizationService organizationService) : base(organizationService)
        {
            paymentDataAccess = new PaymentDataAccessTest(organizationService);
        }

        public void CreatePayment(sw365_subscription subscription)
        {
            // Create the payment record
            Console.WriteLine($"MembershipProcessor | CreatePayment | Create Full Payment");
            var payment = new sw365_payment()
            {
                sw365_subscription = new EntityReference(sw365_subscription.EntityLogicalName, subscription.Id),
                sw365_membership = new EntityReference(Account.EntityLogicalName, subscription.sw365_membership.Id),
                sw365_paymentduedate = DateTime.Now.AddDays(14),
                sw365_fee = subscription.sw365_subscriptionprice,
                StatusCode = sw365_payment_StatusCode.Pending
            };

            // Create the payment record
            paymentDataAccess.CreateEntity(payment);
        }

        public void CreatePayment(sw365_subscription subscription, int installments)
        {
            // Create the payment record
            Console.WriteLine($"MembershipProcessor | CreatePayment | Create Monthly Payment");

            Console.WriteLine($"MembershipProcessor | CreatePayment | subscription id: {subscription.Id}");
            Console.WriteLine($"MembershipProcessor | CreatePayment | membership id: {subscription.sw365_membership.Id}");
            Console.WriteLine($"MembershipProcessor | CreatePayment | membership id:{subscription.sw365_subscriptionprice.Value}");
            var payment = new sw365_payment()
            {
                sw365_subscription = new EntityReference(sw365_subscription.EntityLogicalName, subscription.Id),
                sw365_membership = new EntityReference(Account.EntityLogicalName, subscription.sw365_membership.Id),
                sw365_paymentduedate = DateTime.Now.AddDays(14),
                sw365_fee = new Money(subscription.sw365_subscriptionprice.Value / Convert.ToDecimal(installments)),
                StatusCode = sw365_payment_StatusCode.Pending
            };

            // Create the payment record
            for (int i = 0; i < installments; i++)
            {
                paymentDataAccess.CreateEntity(payment);
            }
        }
    }
}
