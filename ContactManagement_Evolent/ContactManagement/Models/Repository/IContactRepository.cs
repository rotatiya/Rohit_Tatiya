using ContactManagemet.Models.Entities;
using System.Collections.Generic;

namespace ContactManagemet.Models.Repository
{
    public interface IContactRepository
    {
        Contact AddContact(Contact _contact);
        Contact UpdateContact(Contact _contact);
        bool DeleteContact(int id);

        Contact GetContact(int id);
        List<Contact> ListContact();
        
    }
}