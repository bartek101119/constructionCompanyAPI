﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace constructionCompanyAPI.Models
{
    public class CreateConstructionCompanyDto
    {
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        public string LegalForm { get; set; }
        public string NIP { get; set; }
        public string REGON { get; set; }
        public string KRS { get; set; }
        public DateTime StartDate { get; set; }
        [EmailAddress]
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
        public string Voivodeship { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        [Required]
        [MaxLength(40)]
        public string FullName { get; set; }
    }
}
