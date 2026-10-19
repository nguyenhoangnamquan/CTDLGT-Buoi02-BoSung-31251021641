using System;

// Lớp mô phỏng mảng động DynamicArray với kiểu dữ liệu tổng quát T
public class DynamicArray<T>
{
    // Mảng nội bộ lưu trữ các phần tử thực tế
    private T[] items;

    // Thuộc tính lưu số lượng phần tử thực sự đang có trong mảng
    public int Count { get; private set; }

    // Thuộc tính tính sức chứa tối đa hiện tại dựa trên độ dài mảng nội bộ
    public int Capacity => items.Length;

    // Biến đếm tổng số lần sao chép phần tử phục vụ phân tích độ phức tạp khấu hao
    public int TotalCopies { get; private set; }

    // Phương thức khởi tạo với sức chứa mặc định là 4
    public DynamicArray(int capacity = 4)
    {
        // Kiểm tra ràng buộc dung lượng khởi tạo không được là số âm
        if (capacity < 0)
        {
            // Ném ngoại lệ ArgumentOutOfRangeException nếu dung lượng khởi tạo âm
            throw new ArgumentOutOfRangeException(nameof(capacity), "Suc chua khoi tao khong duoc la so am.");
        }

        // Cấp phát mảng nội bộ với dung lượng ban đầu được chỉ định
        items = new T[capacity];

        // Khởi tạo số lượng phần tử thực tế ban đầu bằng 0
        Count = 0;

        // Khởi tạo tổng số phép sao chép ban đầu bằng 0
        TotalCopies = 0;
    }

    // Phương thức thêm một phần tử mới vào cuối mảng động
    public void Add(T item)
    {
        // Kiểm tra và tự động mở rộng mảng nếu dung lượng hiện tại đã đầy
        EnsureCapacity();

        // Lưu phần tử mới vào vị trí trống tiếp theo ở chỉ số Count
        items[Count] = item;

        // Tăng số lượng phần tử thực tế lên 1 sau khi thêm thành công
        Count++;
    }

    // Phương thức kiểm tra và nhân đôi dung lượng mảng khi bị đầy
    private void EnsureCapacity()
    {
        // Chỉ mở rộng mảng khi số phần tử thực tế đã đạt đến sức chứa tối đa
        if (Count == Capacity)
        {
            // Tính dung lượng mới gấp đôi dung lượng cũ (hoặc bằng 4 nếu dung lượng cũ bằng 0)
            int newCapacity = Capacity == 0 ? 4 : Capacity * 2;

            // Khởi tạo mảng tạm mới có dung lượng gấp đôi để chuyển dữ liệu sang
            T[] newItems = new T[newCapacity];

            // Duyệt qua tất cả các phần tử hiện có để sao chép sang mảng mới
            for (int i = 0; i < Count; i++)
            {
                // Sao chép từng phần tử từ mảng cũ sang mảng mới giữ nguyên vị trí
                newItems[i] = items[i];

                // Tăng biến ghi nhận số lần sao chép phục vụ minh họa phân tích khấu hao
                TotalCopies++;
            }

            // Trỏ mảng nội bộ items sang mảng mới có dung lượng lớn hơn
            items = newItems;
        }
    }

    // Bộ truy cập indexer cho phép đọc/ghi phần tử theo chỉ số index
    public T this[int index]
    {
        get
        {
            // Kiểm tra chỉ số index có nằm trong phạm vi hợp lệ từ 0 đến Count - 1 không
            if (index < 0 || index >= Count)
            {
                // Phát sinh ngoại lệ nếu truy cập ngoài phạm vi phần tử hợp lệ
                throw new ArgumentOutOfRangeException(nameof(index), "Chi so index nam ngoai pham vi [0, Count - 1].");
            }

            // Trả về giá trị của phần tử tại chỉ số index hợp lệ
            return items[index];
        }
        set
        {
            // Kiểm tra chỉ số index có nằm trong phạm vi hợp lệ từ 0 đến Count - 1 không
            if (index < 0 || index >= Count)
            {
                // Phát sinh ngoại lệ nếu gán giá trị ngoài phạm vi phần tử hợp lệ
                throw new ArgumentOutOfRangeException(nameof(index), "Chi so index nam ngoai pham vi [0, Count - 1].");
            }

            // Cập nhật giá trị mới cho phần tử tại chỉ số index
            items[index] = value;
        }
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== CHUONG TRINH KIEM THU BAI 4: MANG DONG VA CAPACITY ===\n");

        // Khởi tạo mảng động kiểu int với Capacity mặc định bằng 4
        DynamicArray<int> arr = new DynamicArray<int>();

        Console.WriteLine("--- 1. THEM 10 PHAN TU (1 DEN 10) VA THEO DOI CAPACITY ---");
        // Thực hiện thêm lần lượt các số từ 1 đến 10 vào mảng động
        for (int i = 1; i <= 10; i++)
        {
            // Lưu dung lượng trước khi thêm để phát hiện thời điểm nhân đôi
            int oldCapacity = arr.Capacity;

            // Thêm phần tử i vào mảng động
            arr.Add(i);

            // Kiểm tra xem dung lượng có vừa được mở rộng hay không
            if (arr.Capacity > oldCapacity)
            {
                // In thông báo khi dung lượng tăng (ví dụ từ 4 lên 8, từ 8 lên 16)
                Console.WriteLine($"[MO RONG] Capacity tang tu {oldCapacity} -> {arr.Capacity}! Da sao chep {arr.Count - 1} phan tu.");
            }

            // In trạng thái Count và Capacity sau mỗi lần thêm phần tử
            Console.WriteLine($"Da them {i,2}: Count = {arr.Count,2}, Capacity = {arr.Capacity,2}");
        }

        Console.WriteLine($"\nTong so luong phep sao chep khi them 10 phan tu: {arr.TotalCopies}");

        Console.WriteLine("\n--- 2. KIEM TRA DU LIEU GIU NGUYEN THU TU TRONG MANG ---");
        Console.Write("Cac phan tu trong mang: ");
        // Duyệt qua indexer để kiểm tra thứ tự các phần tử
        for (int i = 0; i < arr.Count; i++)
        {
            // Đọc phần tử qua indexer và in ra màn hình
            Console.Write(arr[i] + (i == arr.Count - 1 ? "" : ", "));
        }
        Console.WriteLine();

        Console.WriteLine("\n--- 3. DOC VA CAP NHAT PHAN TU QUA INDEXER ---");
        // Đọc phần tử tại index 2
        Console.WriteLine($"Gia tri tai index 2 ban dau: {arr[2]}");

        // Cập nhật giá trị tại index 2 thành 999
        arr[2] = 999;
        // Đọc lại để kiểm tra giá trị đã được cập nhật thành công
        Console.WriteLine($"Gia tri tai index 2 sau khi cap nhat arr[2] = 999: {arr[2]}");

        Console.WriteLine("\n--- 4. KIEM THU CAC TRUONG HOP NGOAI LE (LOI INDEX) ---");

        // Thử truy cập index âm (-1)
        try
        {
            // Đọc phần tử tại chỉ số âm
            int val = arr[-1];
        }
        catch (ArgumentOutOfRangeException ex)
        {
            // Bắt ngoại lệ chỉ số âm
            Console.WriteLine("Loi index = -1: " + ex.Message);
        }

        // Thử truy cập index bằng Count (bang 10)
        try
        {
            // Đọc phần tử tại vị trí index == Count
            int val = arr[arr.Count];
        }
        catch (ArgumentOutOfRangeException ex)
        {
            // Bắt ngoại lệ chỉ số bằng Count
            Console.WriteLine("Loi index = Count (10): " + ex.Message);
        }

        // Thử khởi tạo mảng với capacity âm
        try
        {
            // Khởi tạo DynamicArray với sức chứa -5
            DynamicArray<int> invalidArr = new DynamicArray<int>(-5);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            // Bắt ngoại lệ dung lượng âm
            Console.WriteLine("Loi Capacity khoi tao am (-5): " + ex.Message);
        }
    }
}