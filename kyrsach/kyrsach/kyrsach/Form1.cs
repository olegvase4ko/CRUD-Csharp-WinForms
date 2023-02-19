using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace kyrsach
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;
        public Form1()
        {
            InitializeComponent();
        }

        void ShowProd(string table)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(
                $"SELECT * FROM {table}", sqlConnection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            dataGridView1.RowHeadersWidth = 20;
            //dataGridView1.Columns[0].Width = 15;
            dataGridView1.Columns[0].Width = 25;
           
        }

        void ShowClients()
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(
                "SELECT * FROM Clients", sqlConnection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView2.DataSource = dataSet.Tables[0];
            dataGridView2.RowHeadersWidth = 20;
          // dataGridView2.Columns[0].Width = 25;
           // dataGridView2.Columns[1].Width = 90;
        }

       

        void DelProd()
        {
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            var cmd = new SqlCommand($"DELETE FROM Product WHERE id ={id}", sqlConnection);
            cmd.ExecuteNonQuery();
            ShowProd("Product");
           // MessageBox.Show(id.ToString());
        }
        void ShowSeller()
        {
            SqlDataReader dr = null;
            SqlCommand cmdSel = new SqlCommand("SELECT * FROM Seller",sqlConnection);
            dr = cmdSel.ExecuteReader();
            while (dr.Read())
            {
                textBox11.Text = dr["Name"].ToString();
                textBox12.Text = dr["Tel"].ToString();

            }
            cmdSel.Dispose();
            cmdSel = null;
            dr.Close();
           
        }

        void ShowComboProd()
        {
            SqlDataReader dr2 = null;
            SqlCommand cmdSel = new SqlCommand("SELECT * FROM Product", sqlConnection);
            dr2 = cmdSel.ExecuteReader();
            
            while (dr2.Read())
            {
                comboBox1.Items.Add(dr2["Name"].ToString());
               

            }
            cmdSel.Dispose();
            cmdSel = null;
            dr2.Close();
        }

       // int idSeller = 0;
        void ShowComboClients()
        {
            SqlDataReader dr2 = null;
            SqlCommand cmdSel = new SqlCommand("SELECT * FROM Clients", sqlConnection);
            dr2 = cmdSel.ExecuteReader();

            while (dr2.Read())
            {
                comboBox2.Items.Add(dr2["id"].ToString()+ " : " + dr2["Name"].ToString());


            }
            cmdSel.Dispose();
            cmdSel = null;
            dr2.Close();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            sqlConnection.Open();
            if(sqlConnection.State != ConnectionState.Open)
            {
                MessageBox.Show("DB NOT connected");
            }
            ShowProd("Product");
            ShowClients();
            ShowSeller();
            ShowComboProd();
            ShowComboClients();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand(
                $"INSERT INTO [Product] (Name,Amount,Cost) VALUES (N'{textBox1.Text}','{textBox2.Text}','{textBox3.Text}')",sqlConnection);
            //   MessageBox.Show("Добалена " + command.ExecuteNonQuery().ToString() + " Строка");
            command.ExecuteNonQuery();
            ShowProd("Product");
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DelProd();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
          
                 
             int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            label4.Text = "Идентификатор продукта: " + id.ToString();

            textBox4.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();


               /*         SqlCommand command = new SqlCommand($"SELECT * FROM Product WHERE id={id}", sqlConnection);
                        textBox4.Text = command.ExecuteScalar().ToString();
        */
            // MessageBox.Show(SqlDataAdapter);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            SqlCommand command = new SqlCommand(
                $"UPDATE Product SET Name=N'{textBox4.Text}',Amount=N'{textBox5.Text}',Cost='{textBox6.Text}' WHERE id={id}", sqlConnection);

                  //   MessageBox.Show("Добалена " + command.ExecuteNonQuery().ToString() + " Строка");
            command.ExecuteNonQuery();
            ShowProd("Product");
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void tabPage1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand(
               $"INSERT INTO [Clients] (Name,Tel) VALUES (N'{textBox7.Text}','{textBox8.Text}')", sqlConnection);
            //   MessageBox.Show("Добалена " + command.ExecuteNonQuery().ToString() + " Строка");
            command.ExecuteNonQuery();
            ShowProd("Product");
            textBox7.Text = "";
            textBox8.Text = "";
            ShowClients();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value.ToString());
            var cmd = new SqlCommand($"DELETE FROM Clients WHERE id ={id}", sqlConnection);
            cmd.ExecuteNonQuery();
            ShowClients();
        }

        private void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        {
            int id = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value.ToString());
            label8.Text = "Идентификатор клиента: " + id.ToString();

            textBox9.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            textBox10.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value.ToString());
            SqlCommand command = new SqlCommand(
                $"UPDATE Clients SET Name=N'{textBox9.Text}',Tel='{textBox10.Text}' WHERE id={id}", sqlConnection);

            //   MessageBox.Show("Добалена " + command.ExecuteNonQuery().ToString() + " Строка");
            command.ExecuteNonQuery();
            ShowClients();
            textBox9.Text = "";
            textBox10.Text = "";
            
        }

        private void tabPage2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = comboBox1.SelectedItem.ToString();
          //  MessageBox.Show(selectedState);
            SqlDataReader dr2 = null;
            SqlCommand cmdSel = new SqlCommand($"SELECT * FROM Product WHERE Name=N'{selectedState}'", sqlConnection);
            dr2 = cmdSel.ExecuteReader();

            while (dr2.Read())
            {
                textBox13.Text= dr2["Cost"].ToString();


            }
            cmdSel.Dispose();
            cmdSel = null;
            dr2.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = comboBox2.SelectedItem.ToString();
            int value;
            int.TryParse(string.Join("", selectedState.Where(c => char.IsDigit(c))), out value);
           //  MessageBox.Show(value.ToString());
            SqlDataReader dr2 = null;
            SqlCommand cmdSel = new SqlCommand($"SELECT * FROM Clients WHERE Id='{value}'", sqlConnection);
            dr2 = cmdSel.ExecuteReader();

            while (dr2.Read())
            {
                textBox14.Text = dr2["Tel"].ToString();


            }
            cmdSel.Dispose();
            cmdSel = null;
            dr2.Close();
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            //   MessageBox.Show(textBox13.Text);
            try 
            {
                double a = Convert.ToDouble(textBox13.Text, System.Globalization.CultureInfo.InvariantCulture);
                double b = Convert.ToDouble(textBox15.Text, System.Globalization.CultureInfo.InvariantCulture);
                textBox16.Text = "Нажимая кнопку подтвердить заказ покупатель обязуется оплатить заказ на выбранный товар, на сумму: " + a * b + "Br, " +
                    " а продавец обязуется дотсавить необходимый товар в течении 7 рабочих дней.";
            }
            catch (Exception)
            {
                textBox16.Text = "Нажимая кнопку подтвердить заказ покупатель обязуется оплатить заказ на выбранный товар, на сумму: " + 0 + "Br, " +
                    " а продавец обязуется дотсавить необходимый товар в течении 7 рабочих дней.";
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            String post = textBox11.Text;
            String tovar = comboBox1.Text;
            double prise = 0;
            try
            {
                double a = Convert.ToDouble(textBox13.Text, System.Globalization.CultureInfo.InvariantCulture);
                double b = Convert.ToDouble(textBox15.Text, System.Globalization.CultureInfo.InvariantCulture);
                prise = a * b;
            }
            catch (Exception)
            {
                prise = 0;
            }
            
                String amount = textBox15.Text;
            String pokup = comboBox2.Text;

            dataGridView3.Rows[0].Cells[0].Value = post;
            dataGridView3.Rows[0].Cells[1].Value = tovar;
            dataGridView3.Rows[0].Cells[2].Value = amount;
            dataGridView3.Rows[0].Cells[3].Value = prise;
            dataGridView3.Rows[0].Cells[4].Value = pokup;
           
            tabControl1.SelectedTab = tabControl1.TabPages["TabPage4"];
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bmp = new Bitmap(dataGridView1.Size.Width + 10, dataGridView1.Size.Height + 10);
            dataGridView1.DrawToBitmap(bmp, dataGridView1.Bounds);
            e.Graphics.DrawImage(bmp, 0, 0);
        }

        private void button8_Click(object sender, EventArgs e)
        {
          
            printDocument1.Print();
        }
    }
}
