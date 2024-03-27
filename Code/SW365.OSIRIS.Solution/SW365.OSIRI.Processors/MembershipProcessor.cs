using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xrm.Sdk;
using SW365.OSIRIS.TableModel;


namespace SW365.OSIRIS.Processors
{
    public class MembershipProcessor : AbstractProcessor
    {
        public MembershipProcessor(IOrganizationService organizationService) : base(organizationService) { }

        public void GenerateSubscription(Account membership) 
        {
            // Get the subscription type

            // Create the subscription record

            // Create the payment record
        }
    }
}
