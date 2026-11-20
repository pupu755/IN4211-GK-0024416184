# BÀI KIỂM TRA GIỮA HỌC PHẦN — IN4211 (ĐỀ SỐ 02)
**Học phần:** Phát triển phần mềm hướng đối tượng  
**Mã học phần:** IN4211  
**Khoa:** Công nghệ và Kỹ thuật — Trường Đại học Đồng Tháp  
**Giảng viên:** ThS. Nguyễn Tuấn Kiệt  

---

## 👨‍🎓 Thông tin sinh viên
- **Họ và tên:** Bùi Trung Tín
- **Mã số sinh viên (MSSV):** 0024416184
- **Tài khoản GitHub:** [pupu755](https://github.com/pupu755)
- **Kho bài nộp:** [IN4211-GK-0024416184](https://github.com/pupu755/IN4211-GK-0024416184)
- **Nhánh làm bài:** `midterm/0024416184` & `main`

---

## 🏛️ Cấu trúc giải pháp EduHub (Clean Architecture)

Dự án được tổ chức theo mô hình **Clean Architecture** (Dependency Rule - phụ thuộc một chiều hướng vào lõi):

```text
EduHub/
├── EduHub.Domain/            # Lõi trung tâm: Entity (Member, Event, Registration), Enum, Interface Repository
├── EduHub.Application/       # Tầng nghiệp vụ (Use cases): EventService, Interface Notification, Factory Interface
├── EduHub.Infrastructure/    # Tầng ngoại vi hạ tầng: InMemoryEventRepository, Email/Sms Notification, Factory
├── EduHub.API/               # Tầng Presentation: ASP.NET Core Web API, EventsController, Swagger UI, Program.cs
├── EduHub.Tests/             # Unit tests (xUnit, Moq) cho Entity, Service và Controller
├── MinhChung/                # Ảnh chụp kết quả kiểm thử Swagger UI (Thành công 201 & Lỗi 400)
├── TRA-LOI.md                # Tệp trả lời chi tiết tất cả các câu hỏi lý thuyết (Câu 1a, 1b, 2a, 3a, 4b, 5a, 5b, 5d)
└── EduHub.sln                # Solution file quản lý 5 project
```

---

## 📋 Tóm tắt nội dung thực hiện (5 câu)

### 🔹 Câu 1: Lập trình hướng đối tượng (OOP)
- **1a:** Nêu định nghĩa và giải thích 4 trụ cột OOP (Đóng gói, Kế thừa, Đa hình, Trừu tượng) kèm chỉ dẫn tệp/lớp minh họa trong bài.
- **1b:** Phân tích 2 vấn đề đóng gói của lớp `Member` cũ và cài đặt lại lớp `Member` chuẩn OOP trong `EduHub.Domain/Entities` với factory method `Create(...)` kiểm tra email không rỗng và có ký tự `@`.

### 🔹 Câu 2: Mô hình hoá và cài đặt Entity Registration
- **2a:** Sơ đồ trạng thái `stateDiagram-v2` (Mermaid) thể hiện chuyển dịch giữa `Pending`, `Confirmed`, `Cancelled`.
- **2b:** Cài đặt enum `RegistrationStatus` và entity `Registration` với các quy tắc nghiệp vụ bất biến.
- **2c:** Bộ 5 unit test xUnit cho `Registration`, bao gồm ca kiểm thử bắt buộc: phiếu đã `Cancel` khi gọi tiếp `Confirm` sẽ ném `InvalidOperationException` và giữ nguyên trạng thái `Cancelled`.

### 🔹 Câu 3: Nguyên lý SOLID và Design Patterns
- **3a:** Chỉ rõ các vi phạm SRP, OCP, DIP kèm số dòng cụ thể của `EventService` ban đầu.
- **3b:** Tái cấu trúc theo đúng các tầng Clean Architecture:
  + Entity `Event` có `Create(...)` kiểm tra tiêu đề và sức chứa > 0.
  + Interface `IEventRepository` và cài đặt `InMemoryEventRepository`.
  + Interface `INotificationService`, các dịch vụ `EmailNotificationService`, `SmsNotificationService` và `NotificationServiceFactory`.
  + `EventService` nhận phụ thuộc qua constructor, cấu hình DI rõ ràng trong `Program.cs`.
- **3c:** Unit test sử dụng Moq giả lập `IEventRepository` và `INotificationService`, xác minh `Add` và `Send` được gọi chính xác 1 lần khi tạo sự kiện hợp lệ.

### 🔹 Câu 4: Git và GitHub
- **4b:** Phân biệt cơ chế `git merge` và `git rebase`, giải thích Quy tắc vàng của Rebase (không rebase trên nhánh public/shared).
- Lịch sử Git tuân thủ quy chuẩn **Conventional Commits** (`feat:`, `refactor:`, `test:`, `docs:`, `fix:`...).

### 🔹 Câu 5: Clean Architecture và REST API
- **5a:** Sơ đồ 4 lớp và vị trí 5 thành phần (`Event`, `IEventRepository`, `EventService`, `InMemoryEventRepository`, `EventsController`).
- **5b:** Giải thích nguyên nhân `Domain` không được phụ thuộc vào `Infrastructure`.
- **5c:** Cài đặt `EventsController` đủ 4 endpoint chuẩn RESTful:
  + `GET /api/clubs/{clubId}/events` (200 OK)
  + `GET /api/events/{id}` (200 OK / 404 NotFound)
  + `POST /api/clubs/{clubId}/events` (201 Created / 400 BadRequest)
  + `DELETE /api/events/{id}` (204 NoContent / 404 NotFound)
- **5d:** Giải thích nguyên nhân cơ chế vòng đời `AddScoped` gây mất dữ liệu với `List<Event>` instance thông thường và 2 giải pháp triệt để (`AddSingleton` hoặc danh sách `static` có lock).

---

## 🚀 Hướng dẫn biên dịch và kiểm thử

### 1. Biên dịch toàn bộ Solution:
```bash
dotnet build
```
*(Kết quả: Build thành công 5/5 project, 0 Warning, 0 Error)*

### 2. Chạy toàn bộ Unit Tests:
```bash
dotnet test
```
*(Kết quả: **13/13 tests Passed (100%)**)*

### 3. Khởi chạy Web API và mở Swagger UI:
```bash
dotnet run --project EduHub.API
```
Truy cập giao diện Swagger tại: **`http://localhost:5149/swagger`**

---

## 📸 Minh chứng kiểm thử Swagger
Các hình ảnh kiểm thử thực tế được lưu tại thư mục [`MinhChung/`](./MinhChung):
- **Thành công (HTTP 201 Created):** `MinhChung/swagger-post-success.png`
- **Lỗi validation (HTTP 400 Bad Request):** `MinhChung/swagger-post-error.png`
