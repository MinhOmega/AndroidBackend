namespace SWHRMS.Api.V1.Models.Auth
{
    public class GetProfileModel
    {
            public string Fullname { get; set; }
        
            public bool IsChangePassword { get; set; }
            public string AccountType { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string DateOfBirth { get; set; }
            public string Gender { get; set; }
            public string PhoneNumber { get; set; }
            public string EmailAddress { get; set; }
            public string CardNumber { get; set; }
            
            public int TotalPoints { get; set; }
           
            public bool IsReceiverEmail { get; set; }
            public bool IsReceiverOption { get; set; }
        
            
         
    }
}
