using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;
namespace btl
{
    public partial class Dethitracnghiem : Form
    {
        
       
        
        string str = @"Data Source=VECTOR\SQLEXPRESS;Initial Catalog=sqlPMTTN;Integrated Security=True;Encrypt=False";
        SqlConnection connection;
        SqlDataReader docdulieu;
        public Dethitracnghiem()
        {
            InitializeComponent();
         
        }
        private void LoadComboBoxData()
        {
            try
            {
                connection = new SqlConnection(str);
                connection.Open();

                string query = "SELECT MonID, TenMon FROM MonHoc"; 
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                comboBox1.DataSource = table; 
                comboBox1.DisplayMember = "TenMon"; 
                comboBox1.ValueMember = "MonID"; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading ComboBox data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

      

        private void Dethitracnghiem_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();
        }

      

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue != null)
            {
                string selectedValue = comboBox1.SelectedValue.ToString();
                LoadDataGridView(selectedValue);
            }
        }

        private void LoadDataGridView(string monId)
        {
            try
            {
                connection = new SqlConnection(str);
                connection.Open();

                string query = "SELECT * FROM CauHoi WHERE MonID = @MonID"; // Adjust based on your table structure
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MonID", monId);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                dataGridView1.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading DataGridView: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
         
            if (string.IsNullOrWhiteSpace(txtid.Text) || string.IsNullOrWhiteSpace(txtcauhoi.Text) ||
                string.IsNullOrWhiteSpace(txtA.Text) || string.IsNullOrWhiteSpace(txtB.Text) ||
                string.IsNullOrWhiteSpace(txtC.Text) || string.IsNullOrWhiteSpace(txtD.Text) ||
                string.IsNullOrWhiteSpace(txtdapan.Text))
            {
                MessageBox.Show("Please fill in all fields before adding a question.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                connection = new SqlConnection(str);
                connection.Open();
                
                string query = "INSERT INTO CauHoi (CauHoiID, MonID, NoiDungCauHoi, DapAnA, DapAnB, DapAnC, DapAnD, DapAnDung) " +
                               "VALUES (@CauHoiID, @MonID, @NoiDungCauHoi, @DapAnA, @DapAnB, @DapAnC, @DapAnD, @DapAnDung)";
                
                SqlCommand command = new SqlCommand(query, connection);
               
                command.CommandText = "INSERT INTO CauHoi " +
               "VALUES('" + txtid.Text.Trim() + "','" + txtidmon.Text.Trim() + "','" + comboBox1.SelectedValue + "','" 
               + txtcauhoi.Text.Trim() + "','" + txtA.Text.Trim() + "','" + txtB.Text.Trim()
               + txtC.Text.Trim() + "','" + txtD.Text.Trim() + "','" + txtdapan.Text.Trim() +


               "')";
                command.ExecuteNonQuery();

                MessageBox.Show("Question added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataGridView(comboBox1.SelectedValue.ToString()); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding question: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

    }
}
