using System;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using SW365.OSIRIS.Plugins.Tests.DataAccess;
using SW365.OSIRIS.Plugins.Tests.Processors;

namespace SW365.OSIRIS.Plugins.Tests
{
    public class Program
    {
        static string organizationUri = "https://fcm-dev0624.crm6.dynamics.com/";
        static string clientId = "b72c8cea-ba88-4d12-90fc-5b333466a824";
        static string clientSecret = "C6_8Q~Vt_iAKom56QJ.RQIngckPeyTfkZ5qK-c7Z";
        public static void Main(string[] args)
        {
            IOrganizationService service = GetOrganizationService();
            var whoAmIresponse = service.Execute(new WhoAmIRequest());
            Console.WriteLine("Logged in user GUID: " + whoAmIresponse.Results["UserId"]);

            TetsGeneratingPayments(service);

            Console.ReadLine();
        }

        private static IOrganizationService GetOrganizationService()
        {
            try
            {
                var connection = new CrmServiceClient($@"AuthType=ClientSecret;url={organizationUri};ClientId={clientId};ClientSecret={clientSecret}");
                return connection.OrganizationWebProxyClient != null ? connection.OrganizationWebProxyClient : (IOrganizationService)connection.OrganizationServiceProxy;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while connecting to CRM " + ex.Message);
                Console.ReadKey();
                return null;
            }
        }

        private static void TetsGeneratingPayments(IOrganizationService organizationService) 
        {
            var subscriptionDataAccessTest = new SubscriptionDataAccessTest(organizationService);
            var paymentProcessorTest = new PaymentProcessorTest(organizationService);
            // Get Subscription
            var subscription = subscriptionDataAccessTest.GetSubscriptionById(new Guid("d3868c8b-3044-ef11-a316-002248e33fd3"));

            // Generate Payment
            paymentProcessorTest.CreatePayment(subscription, 12);
        }
    }
}
