using Microsoft.Xrm.Sdk;

namespace SW365.OSIRIS.Plugins.Tests.Processors
{
    public abstract class AbstractProcessorTest
    {
        protected IOrganizationService _organizationService;
        protected ITracingService _tracingService;

        public AbstractProcessorTest(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }
    }
}
