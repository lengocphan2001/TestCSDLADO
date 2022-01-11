using System;
using System.Data;
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
        private static void checkId()
        {
            try
            {
                string connectionString = "Data Source=PHAN; Initial Catalog = qlsv; Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection cnn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("select MaSV from SinhVien", cnn))
                    {
                        cnn.Open();
                        Console.WriteLine("Nhap ma sinh vien: ");
                        int ma = Convert.ToInt32(Console.ReadLine());
                        bool check = true;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (Convert.ToInt32(reader.GetValue(0)) == ma)
                                {
                                    check = false;
                                    break;
                                }
                            }
                            reader.Close();
                        }
                        if (check)
                        {
                            Console.WriteLine("Nhập ten sinh vien: ");
                            String name = Console.ReadLine();
                            Console.WriteLine("Nhap ngay sinh: ");
                            DateTime day = Convert.ToDateTime(Console.ReadLine());
                            Console.WriteLine("Nhap gioi tinh: ");
                            String gender = Console.ReadLine();
                            using (SqlCommand cmd1 = cnn.CreateCommand())
                            {
                                cmd1.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd1.CommandText = "them_sinhvien";
                                cmd1.Parameters.AddWithValue("@masv", ma);
                                cmd1.Parameters.AddWithValue("@name", name);
                                cmd1.Parameters.AddWithValue("@ngaysinh", day);
                                cmd1.Parameters.AddWithValue("@gioitinh", gender);
                                int a = cmd1.ExecuteNonQuery();
                                if (a > 0) Console.WriteLine("Success!");
                                else Console.WriteLine("Failed!");
                            }
                            
                        }
                        else Console.WriteLine("Ma sinh vien da ton tai");
                    }
                    cnn.Dispose();
                    cnn.Close();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private static void showDataBase1()
        {
            String connectString = "Data Source=PHAN; Initial Catalog = qlsv; Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            try
            {
                using (SqlConnection cnn = new SqlConnection(connectString))
                {
                    using (SqlCommand cmd = new SqlCommand("select * from SinhVien", cnn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable tb = new DataTable("SinhVien");
                            adapter.Fill(tb);
                            if (tb.Rows.Count == 0)
                            {
                                Console.WriteLine("Khong co ban ghi nao!");
                            }
                            else
                            {
                                DataView dv = new DataView(tb);
                                dv.Sort = "sHoTen DESC";
                                dv.RowFilter = "sGioiTinh = 'Nu'";
                                foreach (DataRow cur in dv)
                                {
                                    Console.WriteLine("Le Ngoc Phan");
                                    Console.WriteLine(cur["MaSV"] + "-" + cur["sHoTen"] + "-" + cur["dNgaySinh"] + "-" + cur["sGioiTinh"]);
                                }
                            }
                        }
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
                Console.WriteLine("4. Kiem tra ma sinh vien");
                Console.WriteLine("5. Thoat");
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
                        showDataBase1();
                        break;
                    case 4:
                        checkId();
                        break;
                    default:
                        break;
                }
            } while (choose != 5);
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
