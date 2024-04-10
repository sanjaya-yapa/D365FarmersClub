using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Microsoft.Xrm.Sdk;
using SW365.OSIRIS.TableModel;

namespace SW365.OSIRIS.Processors
{
    public abstract class AbstractProcessor
    {
        protected IOrganizationService _organizationService;
        protected ITracingService _tracingService;

        public AbstractProcessor(IOrganizationService organizationService, ITracingService tracingService)
        {
            this._organizationService = organizationService;
            this._tracingService = tracingService;
        }
    }
}
