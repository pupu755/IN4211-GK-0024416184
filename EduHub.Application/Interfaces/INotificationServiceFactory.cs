namespace EduHub.Application.Interfaces;

/// <summary>
/// Factory interface chọn dịch vụ thông báo tương ứng theo tên kênh.
/// </summary>
public interface INotificationServiceFactory
{
    INotificationService Create(string channel);
}
