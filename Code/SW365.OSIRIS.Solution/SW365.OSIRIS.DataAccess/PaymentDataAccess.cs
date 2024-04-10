using Microsoft.Xrm.Sdk;
using SW365.OSIRIS.TableModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SW365.OSIRIS.DataAccess
{
    public class PaymentDataAccess : DataAccessBase<sw365_payment>
    {
        public PaymentDataAccess(IOrganizationService organizationService) : base(organizationService) { }
    }
}
