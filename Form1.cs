using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace De01
{
    public partial class AddStudentForm : Form
    {
        public Student NewStudent { get; private set; }
        public AddStudentForm()
        {
            InitializeComponent();
        }

        public class Student
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string Grade { get; set; }
        }
        private void LoadSinhVien()
        {
            dgvSinhVien.Rows.Clear();

            conn = new SqlConnection(connString);
            string query = "SELECT * FROM Sinhvien";
            da = new SqlDataAdapter(query, conn);
            dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                int index = dgvSinhVien.Rows.Add();
                dgvSinhVien.Rows[index].Cells["MaSV"].Value = row["MaSV"].ToString();
                dgvSinhVien.Rows[index].Cells["HotenSV"].Value = row["HotenSV"].ToString();
                dgvSinhVien.Rows[index].Cells["NgaySinh"].Value = Convert.ToDateTime(row["NgaySinh"]).ToString("dd/MM/yyyy");
                dgvSinhVien.Rows[index].Cells["MaLop"].Value = row["MaLop"].ToString();
            }
        }
        SqlConnection conn;
        SqlDataAdapter da;
        DataTable dt;
        SqlCommand cmd;
        string connString = @"Data Source=.;Initial Catalog=QuanLySV;Integrated Security=True";
        private void frmSinhvien_Load(object sender, EventArgs e)
        {
            LoadLop();
            LoadSinhVien();
        }
        private void LoadLop()
        {
            conn = new SqlConnection(connString);
            string query = "SELECT * FROM Lop";
            da = new SqlDataAdapter(query, conn);
            dt = new DataTable();
            da.Fill(dt);

            cboLop.DataSource = dt;
            cboLop.DisplayMember = "TenLop";
            cboLop.ValueMember = "MaLop";
        }

        private void btThem_Click(object sender, EventArgs e)
        {
            using (var inputForm = new AddStudentForm())
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    // Lấy sinh viên mới từ AddStudentForm thông qua thuộc tính NewStudent
                    var newStudent = inputForm.NewStudent;

                    // Thêm sinh viên vào DataGridView
                    dgvSinhVien.Rows.Add(newStudent.Name, newStudent.Age, newStudent.Grade);
                }
            }
        }
    }
}

