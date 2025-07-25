namespace Domain.Entities.Enums
{
    public enum NotificationType
    {
        SystemOnly = 0,
        Email = 1,
        Sms = 2,
        EmailAndSystem = 3,
        SmsAndSystem = 4,
        EmailAndSms = 5,
        EmailAndSmsAndSystem = 6,  
    }
}
