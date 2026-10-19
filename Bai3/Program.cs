using System;
using System.Collections.Generic;

// Định nghĩa lớp Student biểu diễn đối tượng sinh viên
public class Student
{
    // Lưu mã duy nhất dùng để nhận diện sinh viên
    public string Id { get; set; } = "";

    // Lưu họ tên dùng cho hiển thị và tìm kiếm
    public string FullName { get; set; } = "";

    // Lưu điểm tổng kết trong đoạn từ 0 đến 10
    public double Score { get; set; }
}

class Program
{
    // Hàm tìm kiếm sinh viên theo Id (dùng duyệt tuyến tính thay vì LINQ)
    static Student FindStudentById(List<Student> students, string id)
    {
        // Duyệt từng phần tử trong danh sách để tìm Id khớp
        for (int i = 0; i < students.Count; i++)
        {
            // So sánh Id hiện tại với Id cần tìm (không phân biệt chữ hoa/thường)
            if (students[i].Id.Equals(id, StringComparison.OrdinalIgnoreCase))
            {
                // Trả về đối tượng sinh viên khi tìm thấy
                return students[i];
            }
        }
        // Trả về null khi không tìm thấy sinh viên nào trùng Id
        return null;
    }

    // Chức năng 1: Thêm sinh viên mới vào danh sách
    static void AddStudent(List<Student> students)
    {
        Console.WriteLine("\n--- THEM SINH VIEN MOI ---");
        Console.Write("Nhap ma sinh vien (ID): ");
        string id = Console.ReadLine()?.Trim() ?? "";

        // Bắt lỗi nếu người dùng nhập chuỗi mã rỗng
        if (string.IsNullOrWhiteSpace(id))
        {
            Console.WriteLine("Loi: Ma sinh vien khong duoc de rong!");
            return;
        }

        // Kiểm tra trùng mã bằng cách duyệt danh sách O(n)
        if (FindStudentById(students, id) != null)
        {
            // Báo lỗi và từ chối thêm mới khi mã sinh viên đã tồn tại
            Console.WriteLine($"Loi: Ma sinh vien '{id}' da ton tai trong he thong!");
            return;
        }

        Console.Write("Nhap ho va ten: ");
        string fullName = Console.ReadLine()?.Trim() ?? "";

        // Bắt lỗi nếu họ tên bị để rỗng
        if (string.IsNullOrWhiteSpace(fullName))
        {
            Console.WriteLine("Loi: Ho ten khong duoc de rong!");
            return;
        }

        Console.Write("Nhap diem tong ket (0 - 10): ");
        string scoreInput = Console.ReadLine()?.Trim() ?? "";

        // Ép kiểu điểm sang double và kiểm tra tính hợp lệ của số
        if (!double.TryParse(scoreInput, out double score) || score < 0 || score > 10)
        {
            // Báo lỗi và từ chối thêm khi điểm nằm ngoài đoạn [0, 10]
            Console.WriteLine("Loi: Diem tong ket phai la so thực trong doan [0, 10]!");
            return;
        }

        // Tạo đối tượng sinh viên mới với các thông tin đã kiểm tra
        Student newStudent = new Student
        {
            Id = id,
            FullName = fullName,
            Score = score
        };

        // Thêm sinh viên mới vào danh sách động
        students.Add(newStudent);
        Console.WriteLine("--> Them sinh vien thanh cong!");
    }

    // Chức năng 2: Cập nhật thông tin sinh viên theo Id
    static void UpdateStudent(List<Student> students)
    {
        Console.WriteLine("\n--- CAP NHAT SINH VIEN ---");
        Console.Write("Nhap ma sinh vien can cap nhat: ");
        string id = Console.ReadLine()?.Trim() ?? "";

        // Tìm sinh viên trong danh sách theo Id
        Student student = FindStudentById(students, id);

        // Báo lỗi khi không tìm thấy sinh viên
        if (student == null)
        {
            Console.WriteLine($"Loi: Khong tim thay sinh vien co ma '{id}'!");
            return;
        }

        Console.WriteLine($"Thong tin hien tai: Ten = {student.FullName}, Diem = {student.Score}");
        Console.Write("Nhap ho ten moi (De rong neu khong doi): ");
        string newName = Console.ReadLine()?.Trim() ?? "";

        // Cập nhật tên mới nếu người dùng nhập dữ liệu
        if (!string.IsNullOrWhiteSpace(newName))
        {
            student.FullName = newName;
        }

        Console.Write("Nhap diem moi (De rong neu khong doi): ");
        string newScoreInput = Console.ReadLine()?.Trim() ?? "";

        // Kiểm tra và cập nhật điểm mới nếu người dùng có nhập
        if (!string.IsNullOrWhiteSpace(newScoreInput))
        {
            if (double.TryParse(newScoreInput, out double newScore) && newScore >= 0 && newScore <= 10)
            {
                // Cập nhật điểm mới hợp lệ
                student.Score = newScore;
            }
            else
            {
                Console.WriteLine("Loi: Diem moi khong hop le (phai tu 0 den 10). Gia tri diem giu nguyen!");
            }
        }

        Console.WriteLine("--> Cap nhat thong tin thanh cong!");
    }

    // Chức năng 3: Xóa sinh viên theo Id
    static void DeleteStudent(List<Student> students)
    {
        Console.WriteLine("\n--- XOA SINH VIEN ---");
        Console.Write("Nhap ma sinh vien can xoa: ");
        string id = Console.ReadLine()?.Trim() ?? "";

        int targetIndex = -1;

        // Duyệt tìm vị trí chỉ số của sinh viên cần xóa trong danh sách
        for (int i = 0; i < students.Count; i++)
        {
            if (students[i].Id.Equals(id, StringComparison.OrdinalIgnoreCase))
            {
                targetIndex = i;
                break;
            }
        }

        // Thông báo lỗi nếu không tìm thấy sinh viên
        if (targetIndex == -1)
        {
            Console.WriteLine($"Loi: Khong tim thay sinh vien co ma '{id}' de xoa!");
            return;
        }

        // Thực hiện xóa phần tử tại vị trí targetIndex và dịch các phần tử phía sau sang trái
        students.RemoveAt(targetIndex);
        Console.WriteLine($"--> Da xoa thanh cong sinh vien co ma '{id}'.");
    }

    // Chức năng 4: Tìm kiếm sinh viên theo một phần họ tên (Không dùng LINQ)
    static void SearchByName(List<Student> students)
    {
        Console.WriteLine("\n--- TIM KIEM SINH VIEN THEO TEN ---");
        Console.Write("Nhap tu khoa ho ten can tim: ");
        string keyword = Console.ReadLine()?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(keyword))
        {
            Console.WriteLine("Loi: Tu khoa tim kiem khong duoc de rong!");
            return;
        }

        bool found = false;
        Console.WriteLine("\nKet qua tim kiem:");

        // Duyệt tuần tự qua danh sách để tìm họ tên chứa từ khóa
        for (int i = 0; i < students.Count; i++)
        {
            // So sánh không phân biệt chữ hoa chữ thường bằng ToLower()
            if (students[i].FullName.ToLower().Contains(keyword.ToLower()))
            {
                // In trực tiếp thông tin sinh viên tìm thấy lên màn hình
                Console.WriteLine($"[ID: {students[i].Id}] Ten: {students[i].FullName} | Diem: {students[i].Score}");
                found = true;
            }
        }

        // Thông báo nếu không tìm thấy sinh viên nào khớp từ khóa
        if (!found)
        {
            Console.WriteLine($"Khong tim thay sinh vien nao chua tu khoa '{keyword}'.");
        }
    }

    // Chức năng 5: Hiển thị danh sách, tính điểm trung bình và thống kê sinh viên đạt
    static void DisplayAndStat(List<Student> students)
    {
        Console.WriteLine("\n--- DANH SACH VA THONG KE SINH VIEN ---");

        // Xử lý trường hợp danh sách rỗng để tránh lỗi chia cho 0 khi tính trung bình
        if (students.Count == 0)
        {
            Console.WriteLine("Danh sach sinh vien hien dang rong!");
            return;
        }

        double sumScore = 0;
        int passCount = 0;

        Console.WriteLine($"{"ID",-10} | {"HO VA TEN",-25} | {"DIEM",-6}");
        Console.WriteLine(new string('-', 47));

        // Duyệt mảng đúng 1 lần O(n) để vừa in danh sách vừa cộng dồn điểm và đếm
        for (int i = 0; i < students.Count; i++)
        {
            // In thông tin từng sinh viên căn chỉnh theo cột
            Console.WriteLine($"{students[i].Id,-10} | {students[i].FullName,-25} | {students[i].Score,-6:F2}");

            // Cộng dồn điểm tổng kết vào biến tổng
            sumScore += students[i].Score;

            // Kiểm tra điều kiện sinh viên đạt điểm từ 5.0 trở lên
            if (students[i].Score >= 5.0)
            {
                // Tăng biến đếm sinh viên đạt
                passCount++;
            }
        }

        // Tính điểm trung bình cộng của cả lớp
        double avgScore = sumScore / students.Count;

        Console.WriteLine(new string('-', 47));
        Console.WriteLine($"Tong so sinh vien: {students.Count}");
        Console.WriteLine($"Diem trung binh ca lop: {avgScore:F2}");
        Console.WriteLine($"So sinh vien dat (Diem >= 5.0): {passCount}");
    }

    static void Main()
    {
        // Khởi tạo danh sách động để lưu trữ dữ liệu các sinh viên
        List<Student> studentList = new List<Student>();

        // Vòng lặp hiển thị menu điều khiển chương trình cho đến khi thoát
        while (true)
        {
            Console.WriteLine("\n================ QUAN LY SINH VIEN ================");
            Console.WriteLine("1. Them sinh vien moi");
            Console.WriteLine("2. Cap nhat ho ten/diem theo ma");
            Console.WriteLine("3. Xoa sinh vien theo ma");
            Console.WriteLine("4. Tim kiem sinh vien theo ho ten");
            Console.WriteLine("5. Hien thi danh sach va thong ke");
            Console.WriteLine("6. Thoat chuong trinh");
            Console.Write("Chon chuc nang (1-6): ");

            string choice = Console.ReadLine()?.Trim() ?? "";

            // Điều hướng xử lý theo lựa chọn của người dùng
            switch (choice)
            {
                case "1":
                    AddStudent(studentList);
                    break;
                case "2":
                    UpdateStudent(studentList);
                    break;
                case "3":
                    DeleteStudent(studentList);
                    break;
                case "4":
                    SearchByName(studentList);
                    break;
                case "5":
                    DisplayAndStat(studentList);
                    break;
                case "6":
                    Console.WriteLine("Thoat chuong trinh");
                    return;
                default:
                    Console.WriteLine("Loi: Luong chon khong hop le, vui long chon tu 1 den 6!");
                    break;
            }
        }
    }
}