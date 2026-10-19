using System;

using System;

class Program
{
    static void Main()
    {
        // Đọc số lượng phần tử n từ bàn phím
        Console.Write("Nhap n:");

        // Đọc dòng nhập n và kiểm tra null hoặc chuỗi rỗng trước khi xử lý
        string inputNLine = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(inputNLine))
        {
            // Báo lỗi và dừng chương trình nếu người dùng nhập null hoặc chuỗi rỗng
            Console.WriteLine("Loi: Gia tri n nhap vao bi null hoac rong!");
            return;
        }

        // Ép kiểu chuỗi sang int và kiểm tra điều kiện n phải là số nguyên dương
        if (!int.TryParse(inputNLine.Trim(), out int n) || n <= 0)
        {
            // Báo lỗi nếu n chứa chữ cái, kí tự đặc biệt hoặc n <= 0
            Console.WriteLine("Loi: n phai la so nguyen duong (n >= 1)!");
            return;
        }

        // Đọc dòng chứa n số nguyên từ bàn phím
        Console.Write("Nhap mang:");

        // Đọc dòng nhập mảng và kiểm tra null hoặc chuỗi rỗng trước khi thực hiện tách chuỗi
        string inputArrayLine = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(inputArrayLine))
        {
            // Báo lỗi và dừng chương trình nếu dữ liệu mảng bị null hoặc rỗng
            Console.WriteLine("Loi: Du lieu mang nhap vao bi null hoac rong!");
            return;
        }

        // Tách chuỗi thành mảng các chuỗi con theo khoảng trắng hoặc phím Tab
        string[] input = inputArrayLine.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        
        // Kiểm tra xem người dùng có nhập đủ n phần tử như đã khai báo hay không
        if (input.Length < n)
        {
            // Báo lỗi nếu số lượng phần tử nhập vào ít hơn n
            Console.WriteLine($"Loi: Chuoi nhap vao khong du {n} phan tu!");
            return;
        }

        // Khởi tạo mảng số nguyên kích thước n để lưu dữ liệu đầu vào
        int[] a = new int[n];
        for (int i = 0; i < n; i++)
        {
            // Chuyển đổi từng chuỗi nhập vào thành số nguyên int và kiểm tra lỗi ép kiểu
            if (!int.TryParse(input[i], out a[i]))
            {
                // Báo lỗi khi gặp phần tử chứa chữ hoặc không thuộc phạm vi int
                Console.WriteLine($"Loi: Phan tu thu {i + 1} ('{input[i]}') khong phai la so nguyen int hợp le!");
                return;
            }
        }

        // Dùng kiểu long để biến tổng không bị tràn số khi cộng dồn tối đa 100.000 số int
        long sum = 0;

        // Khởi tạo min và max bằng phần tử đầu tiên để đảm bảo tính đúng đắn khi toàn số âm hoặc dương
        int min = a[0];
        int max = a[0];

        // Khởi tạo các biến đếm phân loại số phần tử âm, dương và bằng 0
        int countNegative = 0;
        int countPositive = 0;
        int countZero = 0;

        // Duyệt mảng đúng 1 lần duy nhất để tính toán toàn bộ số liệu thống kê trong O(n)
        for (int i = 0; i < n; i++)
        {
            // Cộng dồn giá trị phần tử hiện tại vào biến tổng kiểu long
            sum += a[i];

            // Cập nhật min nếu tìm thấy phần tử nhỏ hơn giá trị min hiện tại
            if (a[i] < min) min = a[i];

            // Cập nhật max nếu tìm thấy phần tử lớn hơn giá trị max hiện tại
            if (a[i] > max) max = a[i];

            // Kiểm tra tính chất dấu của phần tử để tăng biến đếm tương ứng
            if (a[i] < 0)
            {
                // Tăng biến đếm số âm khi giá trị nhỏ hơn 0
                countNegative++;
            }
            else if (a[i] > 0)
            {
                // Tăng biến đếm số dương khi giá trị lớn hơn 0
                countPositive++;
            }
            else
            {
                // Tăng biến đếm số 0 khi giá trị bằng 0
                countZero++;
            }
        }

        // Ép kiểu sum sang double trước phép chia để giữ phần thập phân của trung bình cộng
        double average = (double)sum / n;

        // Khởi tạo hai con trỏ trỏ đến vị trí đầu và vị trí cuối của mảng để đảo tại chỗ
        int left = 0;
        int right = n - 1;

        // Tiến hành đảo mảng tại chỗ không dùng mảng phụ hay hàm thư viện
        while (left < right)
        {
            // Biến trung gian lưu tạm giá trị tại vị trí left trước khi ghi đè
            int temp = a[left];

            // Gán giá trị ở vị trí right sang vị trí left
            a[left] = a[right];

            // Gán giá trị biến tạm vào vị trí right để hoàn tất hoán đổi
            a[right] = temp;

            // Dịch con trỏ left sang phải và right sang trái để xử lý cặp phần tử tiếp theo
            left++;
            right--;
        }

        // In các kết quả thống kê theo yêu cầu đề bài
        Console.WriteLine($"Tong: {sum}");
        Console.WriteLine($"Min: {min}");
        Console.WriteLine($"Max: {max}");
        // Định dạng trung bình hiển thị tối đa 2 chữ số sau dấu thập phân
        Console.WriteLine($"Trung binh: {average:0.##}");
        Console.WriteLine($"So phan tu am: {countNegative}");
        Console.WriteLine($"So phan tu duong: {countPositive}");
        Console.WriteLine($"So phan tu bang 0: {countZero}");

        // In mảng sau khi đã đảo ngược tại chỗ
        Console.Write("Mang sau khi dao: ");
        for (int i = 0; i < n; i++)
        {
            // In các phần tử cách nhau bởi khoảng trắng
            Console.Write(a[i] + (i == n - 1 ? "" : " "));
        }
        Console.WriteLine();
    }
}