namespace constructionCompanyAPI.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Specialization { get; set; }

        public int ConstructionCompanyId { get; set; }
        public virtual ConstructionCompany ConstructionCompany { get; set; }
    }
}