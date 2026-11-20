using EduHub.Domain.Entities;

namespace EduHub.Domain.Interfaces;

/// <summary>
/// Interface hợp đồng lưu trữ và truy vấn sự kiện.
/// </summary>
public interface IEventRepository
{
    void Add(Event @event);
    Event? GetById(Guid id);
    IEnumerable<Event> GetByClubId(Guid clubId);
    bool Delete(Guid id);
}
