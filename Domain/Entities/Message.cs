using Domain.Common.Contracts;

namespace Domain.Entities;

public class Message :  AuditableEntity
{
    public Message(string messageContent, DateTime dateOfIncidence, string farmLocation, string farmerPhoneNumber)
    {
        MessageContent = messageContent;
        DateOfIncidence = dateOfIncidence;
        FarmLocation = farmLocation;
        FarmerPhoneNumber = farmerPhoneNumber;
    }

    public string MessageContent {get; private set;}
    public DateTime DateOfIncidence{get; private set;}
    public string FarmLocation {get; private set;}
    public string FarmerPhoneNumber {get; private set;}

    public Message()
    {
        
    }
    
}