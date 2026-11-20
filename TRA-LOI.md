# BÀI LÀM ĐỀ SỐ 02 — HỌC PHẦN IN4211
**Sinh viên:** Bùi Trung Tín  
**MSSV:** 0024416184  
**Lớp học phần:** Phát triển phần mềm hướng đối tượng (IN4211)  
**Giảng viên:** ThS. Nguyễn Tuấn Kiệt  

---

## Câu 1a
Bốn trụ cột của lập trình hướng đối tượng (OOP):

1. **Đóng gói (Encapsulation):**
   - *Giải thích:* Gom nhóm dữ liệu (thuộc tính) và hành vi (phương thức) vào trong cùng một đơn vị (lớp), đồng thời ẩn giấu trạng thái nội bộ bằng các phạm vi truy cập (`private`, `protected`), chỉ cho phép truy cập hay thay đổi thông qua các phương thức nghiệp vụ hợp lệ nhằm bảo vệ tính toàn vẹn dữ liệu.
   - *Minh hoạ trong bài:* Lớp `Member` trong tệp `EduHub.Domain/Entities/Member.cs` (và `Registration` trong `EduHub.Domain/Entities/Registration.cs`), các thuộc tính có `private set`, constructor `private` và trạng thái chỉ được khởi tạo qua factory method `Create(...)` có kiểm tra hợp lệ.

2. **Kế thừa (Inheritance):**
   - *Giải thích:* Cho phép một lớp dẫn xuất (lớp con) tái sử dụng lại các đặc tính, thuộc tính và hành vi của lớp cơ sở (lớp cha), thiết lập mối quan hệ "is-a" và giúp giảm thiểu việc lặp lại mã nguồn.
   - *Minh hoạ trong bài:* Lớp `EventsController` kế thừa lớp cha `ControllerBase` trong tệp `EduHub.API/Controllers/EventsController.cs` để sử dụng lại các phương thức tiện ích HTTP như `Ok()`, `NotFound()`, `CreatedAtAction()`.

3. **Đa hình (Polymorphism):**
   - *Giải thích:* Khả năng các đối tượng thuộc các lớp khác nhau có thể phản hồi cùng một lời gọi phương thức theo những cách riêng biệt thông qua việc hiện thực hoá interface hoặc ghi đè phương thức.
   - *Minh hoạ trong bài:* Hai lớp `EmailNotificationService` (tệp `EduHub.Infrastructure/Services/EmailNotificationService.cs`) và `SmsNotificationService` (tệp `EduHub.Infrastructure/Services/SmsNotificationService.cs`) cùng cài đặt interface `INotificationService`. Lớp `EventService` chỉ cần gọi `_notificationService.Send(...)`, tại thời điểm chạy phương thức tương ứng của từng kênh sẽ được kích hoạt đa hình.

4. **Trừu tượng (Abstraction):**
   - *Giải thích:* Tập trung vào việc thể hiện các hành vi và hợp đồng cốt lõi của đối tượng ra bên ngoài, giấu đi các chi tiết cài đặt kỹ thuật phức tạp bên dưới, thường được định nghĩa qua interface hoặc abstract class.
   - *Minh hoạ trong bài:* Interface `IEventRepository` trong tệp `EduHub.Domain/Interfaces/IEventRepository.cs` (hoặc `INotificationService` trong `EduHub.Application/Interfaces/INotificationService.cs`), định nghĩa các thao tác lưu trữ mà không phụ thuộc vào chi tiết cài đặt là lưu trữ trong bộ nhớ (InMemory) hay cơ sở dữ liệu SQL.

## Câu 1b
Hai vấn đề về đóng gói của lớp `Member` trong đoạn mã đề bài:
1. **Sử dụng public setter (`public Guid Id { get; set; }`, `public string Email { get; set; }`):** Cho phép bất kỳ đoạn mã nào bên ngoài đối tượng cũng có thể trực tiếp sửa đổi dữ liệu tùy tiện mà không qua cơ chế kiểm tra (validation), phá vỡ tính toàn vẹn của dữ liệu (ví dụ: dòng 9 gán `m.Email = "abc"` không hợp lệ vẫn bị lưu).
2. **Cung cấp constructor mặc định public không tham số (`public Member() { }`):** Cho phép khởi tạo đối tượng `Member` ở trạng thái rỗng hoặc không hợp lệ (Id là `Guid.Empty`, Email mang giá trị rỗng/null) mà không bắt buộc phải truyền vào các thông tin hợp lệ ngay khi tạo.
