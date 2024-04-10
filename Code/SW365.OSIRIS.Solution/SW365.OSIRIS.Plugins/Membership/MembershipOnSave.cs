using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using SW365.OSIRIS.Processors;
using SW365.OSIRIS.TableModel;

namespace SW365.OSIRIS.Plugins
{
    public class MembershipOnSave : PluginBase
    {
        ITracingService tracingService;

        public MembershipOnSave() : base(typeof(MembershipOnSave))
        {
        }

        protected override void ExecuteCrmPlugin(LocalPluginContext localcontext)
        {
            if(localcontext == null)
            {
                throw new ArgumentNullException("ERROR: MembershipOnSave - localContext is null");
            }
            tracingService = localcontext.TracingService;
            IPluginExecutionContext pluginExecutionContext = localcontext.PluginExecutionContext;

            tracingService.Trace($"MembershipOnSave | ExecuteCrmPlugin | Get Post Image");
            Account membership = (pluginExecutionContext.PostEntityImages != null && pluginExecutionContext.PostEntityImages.Contains("PostImage")) 
                                    ? pluginExecutionContext.PostEntityImages["PostImage"].ToEntity<Account>() : null;

            if (membership != null && membership.sw365_membershipstatus.Value == sw365_MembershipStatus.ApplicationApproved) 
            {
                tracingService.Trace($"MembershipOnSave | ExecuteCrmPlugin | Calling Generate Subscription");
                MembershipProcessor membershipProcessor = new MembershipProcessor(localcontext.OrganizationService, tracingService);
                membershipProcessor.GenerateSubscription(membership);
            }

        }
    }
}
