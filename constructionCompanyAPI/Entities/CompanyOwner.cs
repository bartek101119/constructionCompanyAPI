namespace constructionCompanyAPI.Entities
{
    public class CompanyOwner
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }

        public virtual ConstructionCompany ConstructionCompany { get; set; }
    }
}