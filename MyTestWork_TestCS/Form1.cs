using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTestWork_TestCS
{
    
    public partial class Form1 : Form
    {
        int count;
        public Form1()
        {
            InitializeComponent();
            DataBase(" Date, Organization, Town, Country, Manager, Kol, Sum ");
        }

        private void AddColumns() {
            
            if (checkBox1.Checked)
            {
                DataGridViewTextBoxColumn dgvDate;
                dgvDate = new DataGridViewTextBoxColumn();
                dgvDate.Name = "Data";
                dgvDate.HeaderText = "Date";
                dataGridView1.Columns.Add(dgvDate);
            }
            if (checkBox2.Checked)
            {
                DataGridViewTextBoxColumn dgvOrganization;
                dgvOrganization = new DataGridViewTextBoxColumn();
                dgvOrganization.Name = "Organization";
                dgvOrganization.HeaderText = "Organization";
                dataGridView1.Columns.Add(dgvOrganization);
            }
            if (checkBox3.Checked)
            {
                DataGridViewTextBoxColumn dgvTown;
                dgvTown = new DataGridViewTextBoxColumn();
                dgvTown.Name = "Town";
                dgvTown.HeaderText = "Town";
                dataGridView1.Columns.Add(dgvTown);
            }
            if (checkBox4.Checked)
            {
                DataGridViewTextBoxColumn dgvCountry;
                dgvCountry = new DataGridViewTextBoxColumn();
                dgvCountry.Name = "Country";
                dgvCountry.HeaderText = "Country";
                dataGridView1.Columns.Add(dgvCountry);
            }
            if (checkBox5.Checked)
            {
                DataGridViewTextBoxColumn dgvManader;
                dgvManader = new DataGridViewTextBoxColumn();
                dgvManader.Name = "Manader";
                dgvManader.HeaderText = "Manader";
                dataGridView1.Columns.Add(dgvManader);
            }
            DataGridViewTextBoxColumn dgvKol, dgvSum;
            dgvKol = new DataGridViewTextBoxColumn();
            dgvKol.Name = "Kol";
            dgvKol.HeaderText = "Kol";
            dgvSum = new DataGridViewTextBoxColumn();
            dgvSum.Name = "Sum";
            dgvSum.HeaderText = "Sum";
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { dgvKol, dgvSum });

        }

        private void DataBase(string STR)
        {
            AddColumns();
            string connectString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\DataBase\MyTestWork_TestCS\MyTestWork_TestCS\MyTestWork_TestCS.mdf;Integrated Security=True";
            SqlConnection myConnection = new SqlConnection(connectString);
            myConnection.Open();
            string query = "SELECT "+ STR + "FROM DMZ";
            SqlCommand Command = new SqlCommand(query, myConnection);
            SqlDataReader Reader = Command.ExecuteReader();
            List<string[]> Data = new List<string[]>();
            while (Reader.Read())
            {
                Data.Add(new string[dataGridView1.ColumnCount]);
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    Data[Data.Count - 1][i] = Reader[i].ToString();
                }
            }
            Reader.Close();
            myConnection.Close();
            foreach (string[] s in Data)
                dataGridView1.Rows.Add(s);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            string Date, Organization, Town, Country, Manager;
            Date = "";
            Organization = "";
            Town = "";
            Country = "";
            Manager = "";
            if ( checkBox1.Checked )
             {
                 Date = "Date";
             }
            
            if (checkBox2.Checked)
            {
                Organization = "Organization";
            }

            if (checkBox3.Checked)
            {
                Town = "Town";
            }

            if (checkBox4.Checked)
            {
                Country = "Country";
            }

            if (checkBox5.Checked)
            {
                Manager = "Manager";
            }

            if (checkBox5.Checked && checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false)
            {
                DataBaseM();
                return;
            }

            string[] Array = { Date, Organization, Town, Country, Manager, "Kol, Sum " };
            Array = Array.Where(x => x != "").ToArray();
            string one = string.Join(", ", Array);
            DataBase(one);
        }

        private void DataBaseM()
        {
            AddColumns();
            string connectString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\DataBase\MyTestWork_TestCS\MyTestWork_TestCS\MyTestWork_TestCS.mdf;Integrated Security=True";
            SqlConnection myConnection = new SqlConnection(connectString);
            myConnection.Open();
            string query = "SELECT Manager, SUM(KOL), SUM(Sum) FROM DMZ GROUP BY Manager";
            SqlCommand Command = new SqlCommand(query, myConnection);
            SqlDataReader Reader = Command.ExecuteReader();
            List<string[]> Data = new List<string[]>();
            count = 3;
            while (Reader.Read())
            {
                Data.Add(new string[count]);
                for (int i = 0; i < count ; i++)
                {
                    Data[Data.Count - 1][i] = Reader[i].ToString();
                }
            }
            Reader.Close();
            myConnection.Close();
            foreach (string[] s in Data)
                dataGridView1.Rows.Add(s);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            checkBox5.Checked = true;
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            checkBox4.Checked = true;
            DataBase(" Date, Organization, Town, Country, Manager, Kol, Sum ");
        }
    }
}
