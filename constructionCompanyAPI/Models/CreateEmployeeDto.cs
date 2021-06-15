using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace constructionCompanyAPI.Models
{
    public class CreateEmployeeDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Specialization { get; set; }

        public int ConstructionCompanyId { get; set; }
    }
}
