using System;
using System.Data.SqlClient;

namespace TestCSDLADO
{
    class Program
    {
        static void addToDataBase()
        {
            string connectionString = "Data Source=PHAN; Initial Catalog = qlsv; Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                int MaSV = 0;
                string gender = "";
                string name = "";
                Console.WriteLine("Nhập mã sinh viên: ");
                MaSV = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Nhập tên sinh viên: ");
                name = (Console.ReadLine());
                Console.WriteLine("Nhap ngay sinh: ");
                DateTime day = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("Nhap gioi tinh: ");
                gender = Console.ReadLine();
                var query1 = "insert into SinhVien values(" + MaSV + ", N'" + name + "', '" + day + "', N'" + gender + "')";
                Console.WriteLine(query1);
                SqlCommand command1 = new SqlCommand(query1, cnn);
                int a = command1.ExecuteNonQuery();
                if (a > 0) Console.WriteLine("Success!");
                else Console.WriteLine("Failed!");
                cnn.Close();
            }
        }
        static void Main(string[] args)
        {
            int choose = 0;
            do
            {
                Console.WriteLine("1. Thêm sinh viên");
                Console.WriteLine("2. Xóa nhân viên theo mã");
                Console.WriteLine("3. Hiện danh sách sinh viên");
                Console.WriteLine("4. Thoát");
                Console.WriteLine("Moi chon chuc nang: ");
                choose = Convert.ToInt32(Console.ReadLine());
                switch (choose)
                {
                    case 1:
                        addToDataBase();
                        break;
                    case 2:
                        deleteFromDataBase();
                        break;
                    case 3:
                        showDataBase();
                        break;
                    default:
                        break;
                }
            } while (choose != 4);
        }
        private static void showDataBase()
        {
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            string connectionString = "Data Source=PHAN; Initial Catalog = qlsv; Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            String sql = "select * from SinhVien";
            sqlCnn = new SqlConnection(connectionString);
            try
            {
                sqlCnn.Open();
                sqlCmd = new SqlCommand(sql, sqlCnn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                while (sqlReader.Read())
                {
                    Console.WriteLine(sqlReader.GetValue(0) + " - " + sqlReader.GetValue(1) + " - " + Convert.ToDateTime(sqlReader.GetValue(2)) + " - " +sqlReader.GetValue(3));
                }
                sqlReader.Close();
                sqlCmd.Dispose();
                sqlCnn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void deleteFromDataBase()
        {
            SqlConnection sqlCnn;
            SqlCommand sqlCmd;
            string connectionString = "Data Source=PHAN; Initial Catalog = qlsv; Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            Console.WriteLine("Nhap ma sinh vien can xoa: ");
            String id = Console.ReadLine();
            String sql = "delete from SinhVien where MaSV = " + id;
            sqlCnn = new SqlConnection(connectionString);
            try
            {
                sqlCnn.Open();
                sqlCmd = new SqlCommand(sql, sqlCnn);
                int a = sqlCmd.ExecuteNonQuery();
                if (a > 0) Console.WriteLine("Delete Success!");
                else Console.WriteLine("Failed!");
                sqlCmd.Dispose();
                sqlCnn.Close();                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
