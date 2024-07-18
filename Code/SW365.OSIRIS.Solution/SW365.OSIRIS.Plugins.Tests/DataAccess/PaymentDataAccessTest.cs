using Microsoft.Xrm.Sdk;
using SW365.OSIRIS.TableModel;

namespace SW365.OSIRIS.Plugins.Tests.DataAccess
{
    public class PaymentDataAccessTest : DataAccessBaseTest<sw365_payment>
    {
        public PaymentDataAccessTest(IOrganizationService organizationService) : base(organizationService)
        {
        }
    }
}
