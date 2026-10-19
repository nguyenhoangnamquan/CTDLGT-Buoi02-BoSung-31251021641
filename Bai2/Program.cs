using System;

class Program
{
    // Phương thức chèn giá trị value vào vị trí index trong mảng data
    static void Insert(int[] data, ref int count, int index, int value)
    {
        // Kiểm tra xem chỉ số index có nằm trong phạm vi cho phép [0, count] hay không
        if (index < 0 || index > count)
        {
            // Phát sinh ngoại lệ ArgumentOutOfRangeException nếu chỉ số chèn không hợp lệ
            throw new ArgumentOutOfRangeException(nameof(index), "Index nam ngoai pham vi hop le [0, count].");
        }

        // Kiểm tra mảng còn ít nhất một ô trống để thực hiện chèn hay không
        if (count >= data.Length)
        {
            // Phát sinh ngoại lệ InvalidOperationException khi mảng đã đầy dung lượng
            throw new InvalidOperationException("Mang da day, khong the chen them phan tu.");
        }

        // Dịch chuyển các phần tử từ vị trí (count - 1) lùi về index từ phải sang trái để tạo vị trí trống
        for (int i = count - 1; i >= index; i--)
        {
            // Sao chép giá trị từ ô hiện tại sang ô kế tiếp phía sau để không làm mất dữ liệu
            data[i + 1] = data[i];
        }

        // Đặt giá trị mới value vào khoảng trống vừa tạo tại chỉ số index
        data[index] = value;

        // Tăng số lượng phần tử hợp lệ lên 1 sau khi đã chèn thành công
        count++;
    }

    // Phương thức xóa phần tử tại vị trí index trong mảng data
    static void RemoveAt(int[] data, ref int count, int index)
    {
        // Kiểm tra xem chỉ số index có nằm trong phạm vi cho phép [0, count - 1] hay không
        if (index < 0 || index >= count)
        {
            // Phát sinh ngoại lệ ArgumentOutOfRangeException nếu chỉ số xóa không hợp lệ
            throw new ArgumentOutOfRangeException(nameof(index), "Index nam ngoai pham vi hop le [0, count - 1].");
        }

        // Dịch chuyển các phần tử từ vị trí (index + 1) đến (count - 1) từ trái sang phải để đè lên ô bị xóa
        for (int i = index; i < count - 1; i++)
        {
            // Lấy giá trị của ô phía sau đè lên giá trị của ô phía trước
            data[i] = data[i + 1];
        }

        // Giảm số lượng phần tử hợp lệ đi 1 sau khi đã lấp đầy ô bị xóa
        count--;
    }

    // Hàm in các phần tử hợp lệ trong mảng để kiểm tra kết quả
    static void PrintArray(string label, int[] data, int count)
    {
        // In nhãn mô tả ca kiểm thử
        Console.Write(label + ": [");

        // Duyệt qua tất cả các phần tử hợp lệ từ chỉ số 0 đến count - 1
        for (int i = 0; i < count; i++)
        {
            // In từng phần tử hợp lệ, thêm dấu phẩy ngăn cách giữa các số
            Console.Write(data[i] + (i == count - 1 ? "" : ", "));
        }

        // In đóng ngoặc mảng và xuống dòng
        Console.WriteLine($"] (count = {count}, capacity = {data.Length})");
    }

    static void Main()
    {

        // Khởi tạo mảng có sức chứa 10 ô, ban đầu chứa 5 phần tử hợp lệ
        int[] data = new int[10] { 4, 7, 2, 9, 5, 0, 0, 0, 0, 0 };
        int count = 5;

        PrintArray("Ban dau", data, count);

        // Ca 1: Chèn 99 tại index 2
        Insert(data, ref count, 2, 99);
        PrintArray("Sau khi Insert(2, 99)", data, count);

        // Ca 2: Xóa phần tử tại index 1
        RemoveAt(data, ref count, 1);
        PrintArray("Sau khi RemoveAt(1)", data, count);

        // Ca 3: Chèn tại đầu (index = 0)
        Insert(data, ref count, 0, 100);
        PrintArray("Sau khi Insert voi index = 0", data, count);

        // Ca 4: Chèn tại cuối (index = count)
        Insert(data, ref count, count, 88);
        PrintArray("Sau khi Insert voi index = count", data, count);

        // Ca 5: Xóa tại đầu (index = 0)
        RemoveAt(data, ref count, 0);
        PrintArray("Sau khi RemoveAt voi index = 0", data, count);

        // Ca 6: Xóa tại cuối (index = count - 1)
        RemoveAt(data, ref count, count - 1);
        PrintArray("Sau khi RemoveAt voi index = count - 1", data, count);
        
        // Ca 7: Thử chèn với index âm
        try
        {
            Insert(data, ref count, -1, 50);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            // Bat ngoai le chi so am khi chen
            Console.WriteLine("Loi Insert index am: " + ex.Message);
        }

        // Ca 8: Thử xóa với index == count
        try
        {
            RemoveAt(data, ref count, count);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            // Bat ngoai le chi so bang count khi xoa
            Console.WriteLine("Loi RemoveAt index == count: " + ex.Message);
        }

        // Ca 9: Thử chèn khi mảng đã đầy
        int[] fullArray = new int[3] { 1, 2, 3 };
        int fullCount = 3;
        try
        {
            Insert(fullArray, ref fullCount, 1, 99);
        }
        catch (InvalidOperationException ex)
        {
            // Bat ngoai le khi chen vao mang day
            Console.WriteLine("Loi Insert khi mang day: " + ex.Message);
        }
    }
}