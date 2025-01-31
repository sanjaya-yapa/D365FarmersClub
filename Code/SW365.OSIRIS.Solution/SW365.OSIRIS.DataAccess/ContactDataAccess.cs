using Microsoft.Xrm.Sdk;
using SW365.OSIRIS.TableModel;
using System.Linq;

namespace SW365.OSIRIS.DataAccess
{
    public class ContactDataAccess: DataAccessBase<Contact>
    {
        public ContactDataAccess(IOrganizationService organisationService):base(organisationService){}

        public Contact GetContactByEmail(string email) 
        {
            return _serviceContext.ContactSet.Where(c => c.EMailAddress1 == email).FirstOrDefault();
        }
    }
}