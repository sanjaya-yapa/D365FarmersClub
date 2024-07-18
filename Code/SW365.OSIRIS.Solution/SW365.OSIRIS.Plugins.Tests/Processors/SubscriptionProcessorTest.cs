using Microsoft.Xrm.Sdk;
using SW365.OSIRIS.Plugins.Tests.DataAccess;

namespace SW365.OSIRIS.Plugins.Tests.Processors
{
    public class SubscriptionProcessorTest : AbstractProcessorTest
    {
        SubscriptionDataAccessTest subscriptionDataAccessTest;
        public SubscriptionProcessorTest(IOrganizationService organizationService) : base(organizationService)
        {
            subscriptionDataAccessTest = new SubscriptionDataAccessTest(organizationService);
        }
    }
}
