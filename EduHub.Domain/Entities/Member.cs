namespace EduHub.Domain.Entities;

/// <summary>
/// Đại diện cho thành viên sinh viên trong hệ thống EduHub.
/// Lớp đã được đóng gói an toàn theo chuẩn Domain-Driven Design / OOP.
/// </summary>
public class Member
{
    public Guid Id { get; private set; }
    public string Email { get; private set; } = string.Empty;

    // Ngăn chặn việc khởi tạo tự do không qua kiểm tra từ bên ngoài
    private Member() { }

    /// <summary>
    /// Factory method khởi tạo Member hợp lệ.
    /// </summary>
    /// <param name="email">Email của thành viên (không được rỗng và phải chứa ký tự @).</param>
    /// <returns>Đối tượng Member đã được kiểm tra tính toàn vẹn.</returns>
    /// <exception cref="ArgumentException">Ném ra khi email rỗng hoặc không chứa '@'.</exception>
    public static Member Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be empty.", nameof(email));
        }

        if (!email.Contains('@'))
        {
            throw new ArgumentException("Email must contain '@'.", nameof(email));
        }

        return new Member
        {
            Id = Guid.NewGuid(),
            Email = email
        };
    }
}
