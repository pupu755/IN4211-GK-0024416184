namespace EduHub.Domain.Entities;

/// <summary>
/// Đại diện cho sự kiện của câu lạc bộ.
/// </summary>
public class Event
{
    public Guid Id { get; private set; }
    public Guid ClubId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public DateTime Date { get; private set; }
    public int Capacity { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Event() { }

    /// <summary>
    /// Factory method khởi tạo đối tượng Event hợp lệ.
    /// </summary>
    /// <param name="clubId">Mã câu lạc bộ tổ chức.</param>
    /// <param name="title">Tiêu đề sự kiện (không được rỗng).</param>
    /// <param name="date">Thời gian diễn ra sự kiện.</param>
    /// <param name="capacity">Sức chứa (phải lớn hơn 0).</param>
    /// <returns>Đối tượng Event hợp lệ.</returns>
    /// <exception cref="ArgumentException">Ném ra khi tiêu đề rỗng hoặc sức chứa nhỏ hơn hoặc bằng 0.</exception>
    public static Event Create(Guid clubId, string title, DateTime date, int capacity)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Tiêu đề không được để trống.", nameof(title));
        }

        if (capacity <= 0)
        {
            throw new ArgumentException("Sức chứa phải lớn hơn 0.", nameof(capacity));
        }

        return new Event
        {
            Id = Guid.NewGuid(),
            ClubId = clubId,
            Title = title.Trim(),
            Date = date,
            Capacity = capacity,
            CreatedAt = DateTime.UtcNow
        };
    }
}
