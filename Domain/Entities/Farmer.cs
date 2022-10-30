using Domain.Common.Contracts;

namespace Domain.Entities;

public class Farmer : AuditableEntity
{
    public string Name { get; private set; }
    public string CountryCode { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Language { get; private set; }
    public ICollection<Farm> Farms { get; private set; }
    public Farmer(string name, string phoneNum, string language)
    {
        Name = name;
        PhoneNumber = phoneNum;
        Farms = new HashSet<Farm>();
        Language = language;
    }
    public Farmer()
    {
        
    }
}
