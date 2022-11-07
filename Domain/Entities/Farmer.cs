using Domain.Common.Contracts;

namespace Domain.Entities;

public class Farmer : AuditableEntity
{
    public string Name { get; private set; }
    public string CountryCode { get; private set; }
    public string CountryPhoneCode{get; private set;}
    public string PhoneNumber { get; private set; }
    public string Password { get; private set; }
    public string Language { get; private set; }
    public ICollection<Farm> Farms { get; private set; }
    public Farmer(string name, string phoneNum, string language, 
    string countryPhoneCode, string countryCode, string createdBy,string modifiedBy)
    {
        Name = name;
        PhoneNumber = phoneNum;
        Farms = new HashSet<Farm>();
        Language = language;
        CountryPhoneCode = countryPhoneCode;
        CountryCode = countryCode;
        CreatedBy = createdBy;
        LastModifiedBy = modifiedBy;
    }

    public Farmer ChangePhoneNumber(string phonrNumber)
    {
        if (phonrNumber is not null && !phonrNumber.Equals(this.PhoneNumber)) PhoneNumber = phonrNumber;
        return this;
    }
    public Farmer()
    {
        
    }
}
