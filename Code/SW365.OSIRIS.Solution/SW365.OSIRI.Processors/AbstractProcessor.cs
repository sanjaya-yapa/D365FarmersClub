using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xrm.Sdk;
using SW365.OSIRIS.TableModel;

namespace SW365.OSIRIS.Processors
{
    public abstract class AbstractProcessor
    {
        protected IOrganizationService _organizationService;
        protected CrmServiceContext _serviceContext;

        public AbstractProcessor(IOrganizationService organizationService)
        {
            this._organizationService = organizationService;
            this._serviceContext = new CrmServiceContext(organizationService);
        }
    }
}
