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
            _tracingService.Trace($"MembershipProcessor | Create Payment | Craete Full Payment");
            var payment = createPaymentObject(subscription, subscription.sw365_subscriptionprice, DateTime.Now.AddDays(14) ,1);
            paymentDataAccess.CreateEntity(payment);
        }

        public void CreatePayment(sw365_subscription subscription, int installments) 
        {
            _tracingService.Trace($"MembershipProcessor | CreatePayment | Create Monthly Payment");
            decimal installmentAmount = subscription.sw365_subscriptionprice.Value / installments;
            DateTime nextPaymentDate = GetNextMonthFirstDay(DateTime.Now).AddDays(7);
            for (int i = 0; i < installments; i++)
            {
                var payment = createPaymentObject(subscription, new Money(installmentAmount), nextPaymentDate, installments);
                paymentDataAccess.CreateEntity(payment);
                nextPaymentDate = GetNextMonthFirstDay(nextPaymentDate.AddMonths(1));
            }
        }


        private sw365_payment createPaymentObject(sw365_subscription subscription, Money amount, DateTime dueDate, int installments) 
        {
            return new sw365_payment()
            {
                sw365_subscription = new EntityReference(sw365_subscription.EntityLogicalName, subscription.Id),
                sw365_membership = new EntityReference(Account.EntityLogicalName, subscription.sw365_membership.Id),
                sw365_paymentduedate = dueDate,
                sw365_fee = amount, 
                StatusCode = sw365_payment_StatusCode.Pending
            };
        }

        private DateTime GetNextMonthFirstDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1).AddMonths(1);
        }
    }
}
