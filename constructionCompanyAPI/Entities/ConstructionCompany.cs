using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace constructionCompanyAPI.Entities
{
    public class ConstructionCompany
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LegalForm { get; set; }
        public string NIP { get; set; }
        public string REGON { get; set; }
        public string KRS { get; set; }
        public DateTime StartDate { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
        public int? CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }

        public int AddressId { get; set; }
        public virtual Address Address { get; set; }

        public int CompanyOwnerId { get; set; }
        public virtual CompanyOwner CompanyOwner { get; set; }

        public virtual List<Employee> Employees { get; set; }
    }
}
