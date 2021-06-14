using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace constructionCompanyAPI.Models
{
    public class UpdateConstructionCompanyDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        public string LegalForm { get; set; }
        public string NIP { get; set; }
        public string REGON { get; set; }
        public string KRS { get; set; }
        public DateTime StartDate { get; set; }

    }
}
