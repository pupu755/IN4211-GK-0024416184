using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;

namespace EduHub.Infrastructure.Repositories;

/// <summary>
/// Cài đặt IEventRepository lưu trữ sự kiện trong bộ nhớ.
/// Sử dụng danh sách static và khoá luồng để bảo toàn dữ liệu xuyên suốt các HTTP request.
/// </summary>
public class InMemoryEventRepository : IEventRepository
{
    private static readonly List<Event> _events = new();
    private static readonly object _lock = new();

    public void Add(Event @event)
    {
        ArgumentNullException.ThrowIfNull(@event);

        lock (_lock)
        {
            _events.Add(@event);
        }
    }

    public Event? GetById(Guid id)
    {
        lock (_lock)
        {
            return _events.FirstOrDefault(e => e.Id == id);
        }
    }

    public IEnumerable<Event> GetByClubId(Guid clubId)
    {
        lock (_lock)
        {
            return _events.Where(e => e.ClubId == clubId).ToList();
        }
    }

    public bool Delete(Guid id)
    {
        lock (_lock)
        {
            var existing = _events.FirstOrDefault(e => e.Id == id);
            if (existing is not null)
            {
                _events.Remove(existing);
                return true;
            }
            return false;
        }
    }
}
