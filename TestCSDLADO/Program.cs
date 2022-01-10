using System;
using System.Data.SqlClient;

namespace TestCSDLADO
{
    class Program
    {
        private static void deleteWithStoreProcedure()
        {
            try
            {
                string connectionString = "Data Source=PHAN; Initial Catalog = qlsv; Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandText = "xoa_sinhvien";
                        Console.WriteLine("Nhap ma sinh vien muon xoa: ");
                        int ma = Convert.ToInt32(Console.ReadLine());
                        cmd.Parameters.AddWithValue("@masv", ma);
                        cnn.Open();
                        int ans = cmd.ExecuteNonQuery();
                        if (ans > 0) Console.WriteLine("Successfully!");
                        else Console.WriteLine("Failed");
                        cnn.Close();
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private static void addWithProcedure()
        {
            string connectionString = "Data Source=PHAN; Initial Catalog = qlsv; Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            try
            {
                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandText = "them_sinhvien";
                        Console.WriteLine("Nhập ma sinh vien: ");
                        int MaSV = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Nhập ten sinh vien: ");
                        String name = Console.ReadLine();
                        Console.WriteLine("Nhap ngay sinh: ");
                        DateTime day = Convert.ToDateTime(Console.ReadLine());
                        Console.WriteLine("Nhap gioi tinh: ");
                        String gender = Console.ReadLine();
                        cmd.Parameters.AddWithValue("@masv", MaSV);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@ngaysinh", day);
                        cmd.Parameters.AddWithValue("@gioitinh", gender);
                        cnn.Open();
                        int a = cmd.ExecuteNonQuery();
                        if (a > 0) Console.WriteLine("Success!");
                        else Console.WriteLine("Failed!");
                        cnn.Close();
                    }
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
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
                Console.WriteLine("Xin chao Le Ngoc Phan");
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
                Console.WriteLine("1. Them sinh vien");
                Console.WriteLine("2. Xoa nhan vien theo ma");
                Console.WriteLine("3. Hien danh sach sinh vien");
                Console.WriteLine("4. Thoat");
                Console.WriteLine("Moi chon chuc nang: ");
                choose = Convert.ToInt32(Console.ReadLine());
                switch (choose)
                {
                    case 1:
                        addWithProcedure();
                        break;
                    case 2:
                        deleteWithStoreProcedure();
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
            try
            {
                string connectionString = "Data Source=PHAN; Initial Catalog = qlsv; Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                String sql = "select * from SinhVien";
                using (SqlConnection sqlCnn = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand(sql, sqlCnn))
                    {
                        sqlCnn.Open();
                        using (SqlDataReader sqlReader = sqlCmd.ExecuteReader())
                        {
                            if (!sqlReader.HasRows)
                            {
                                Console.WriteLine("Khong co ban ghi nao!");
                            }
                            else
                            {
                                Console.WriteLine("----Danh sach sinh vien----");
                                while (sqlReader.Read())
                                {
                                    Console.WriteLine(sqlReader.GetValue(0) + " - " + sqlReader.GetValue(1) + " - " + Convert.ToDateTime(sqlReader.GetValue(2)) + " - " + sqlReader.GetValue(3));
                                }
                            }
                            sqlReader.Close();
                        }
                    }
                    sqlCnn.Dispose();
                    sqlCnn.Close();
                }
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
