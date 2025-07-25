namespace Domain.Entities.Enums;

public enum OrderStatus
{
    Beklemede = 0,
    Isleniyor = 1,
    KargoyaVerildi = 2,
    TeslimEdildi = 3,
    IptalEdildi = 4,
    IadeEdildi = 5,
    Tamamlandi = 6,
}