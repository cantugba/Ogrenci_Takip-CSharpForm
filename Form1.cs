using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace OgrenciTakipProjesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection con;
        OleDbCommand cmd;
        //string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source ="+Application.StartupPath +" /OgrenciTakipProjesi/ogrenciler.accdb";
        string connectionString = @"Provider = Microsoft.ACE.OLEDB.12.0; Data Source =C:\Users\asus.LAPTOP-GTMR0A7M\source\repos\OgrenciTakipProjesi\bin\Debug\ogrenciler.accdb";
        int ogrenciId = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            ogrenciLoad();
        }

        void add()
        {
            try
            {
                con = new OleDbConnection(connectionString);
                cmd = new OleDbCommand("insert into ogrenci (adi, soyadi, cinsiyeti, sinifi, numarası) values(@adi, @soyadi, @cinsiyeti, @sinifi, @numarası)", con);
                con.Open();
                cmd.Parameters.Add("@adi",OleDbType.VarChar).Value = txtAdi.Text;
                cmd.Parameters.Add("@soyadi", OleDbType.VarChar).Value = txtSoyadi.Text;
 

                string cinsiyet = "";
                if (radioButtonErkek.Checked)
                {
                    cinsiyet = radioButtonErkek.Text;
                }
                else if (radioButtonKiz.Checked)
                {
                    cinsiyet = radioButtonErkek.Text;
                }

                cmd.Parameters.Add("@cinsiyeti", OleDbType.VarChar).Value = cinsiyet;
                cmd.Parameters.Add("@sinifi", OleDbType.VarChar).Value = comboSinif.Text;
                cmd.Parameters.Add("@numarası",OleDbType.Integer).Value = txtNumara.Text;

                cmd.ExecuteNonQuery();
            }
            catch (OleDbException ex)
            {

                throw ex;
            }
            finally
            {
                con.Close();
            }

            ogrenciLoad(); // her veri eklendiğinde datagrid viewi  güncelleme
        }

        void addWithValue()
        {
            try
            {
                con = new OleDbConnection(connectionString);
                cmd = new OleDbCommand("insert into ogrenci(adi,soyadi,cinsiyeti,sinifi,numarası) values(@adi,@soyadi,@cinsiyeti,@sinifi,@numarası)", con);
                con.Open();

                cmd.Parameters.AddWithValue("@adi", txtAdi.Text);
                cmd.Parameters.AddWithValue("@soyadi", txtSoyadi.Text);

                string cinsiyet = "";
                if (radioButtonErkek.Checked)
                {
                    cinsiyet = "Erkek";
                }else if (radioButtonKiz.Checked)
                {
                    cinsiyet = "Kız";
                }
                cmd.Parameters.AddWithValue("@cinsiyeti", cinsiyet);
                cmd.Parameters.AddWithValue("@sinifi", comboSinif.Text);
                cmd.Parameters.AddWithValue("@numarasi", txtNumara.Text);

                cmd.ExecuteNonQuery();

            }
            catch (OleDbException ex)
            {

                throw ex;
            }
            finally
            {
                con.Close();
            }

        }

        void ogrenciLoad()
        {
            try
            {
                con = new OleDbConnection(connectionString);
                cmd = new OleDbCommand("select * from ogrenci", con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGrid.DataSource = dt;
                //id kolonunu gizle
                dataGrid.Columns["id"].Visible = false;

            }
            catch (OleDbException ex)
            {

                throw ex;
            }
        }

        void update()
        {
            try
            {
                con = new OleDbConnection(connectionString);
                cmd = new OleDbCommand("update ogrenci set adi = @adi, soyadi = @soyadi, cinsiyeti = @cinsiyeti, sinifi = @sinifi, numarası = @numarası where id = @id", con);
                con.Open();
                cmd.Parameters.Add("@adi", OleDbType.VarChar).Value = txtAdi.Text;
                cmd.Parameters.Add("@soyadi", OleDbType.VarChar).Value = txtAdi.Text;
                string cinsiyet = "";
                if (radioButtonErkek.Checked)
                {
                    cinsiyet = radioButtonErkek.Text;
                }
                else if (radioButtonKiz.Checked)
                {
                    cinsiyet = radioButtonKiz.Text;
                }

                cmd.Parameters.Add("@cinsiyeti", OleDbType.VarChar).Value = cinsiyet;
                cmd.Parameters.Add("@sinifi", OleDbType.VarChar).Value = comboSinif.Text;
                cmd.Parameters.Add("@numarası", OleDbType.Integer).Value = txtNumara.Text;
                cmd.Parameters.Add("@id", OleDbType.Integer).Value = ogrenciId;
                cmd.ExecuteNonQuery();
            }
            catch (OleDbException ex)
            {

                throw ex;
            }
            finally
            {
                con.Close();
            }
            ogrenciLoad();
        }

        void delete()
        {
            try
            {
                con = new OleDbConnection(connectionString);
                cmd = new OleDbCommand("delete from ogrenci where id = @id", con);
                con.Open();
                cmd.Parameters.Add("@id", OleDbType.Integer).Value = ogrenciId;
                cmd.ExecuteNonQuery();


            }
            catch (OleDbException ex)
            {

                throw ex;
            }
            finally
            {
                con.Close();
            }
            ogrenciLoad();
        }

        void clean()
        {
            /*ogrenciId = 0;
            txtAdi.Text = "";
            txtSoyadi.Text = "";
            radioButtonErkek.Checked = false;
            radioButtonKiz.Checked = false;
            comboSinif.Text = "";
            txtNumara.Text = "";*/

            //dinamik

            foreach (var item in tableLayoutPanel1.Controls)
            {
                if (item is TextBox)
                {
                    ((TextBox)item).Text = "";
                }

                if (item is RadioButton)
                {
                    ((RadioButton)item).Checked = false;
                }

                if (item is ComboBox)
                {
                    ((ComboBox)item).Text = "";
                }

            }
        }

        private void buttonEkle_Click(object sender, EventArgs e)
        {
            if (ogrenciId == 0)
            {
                //addWithValue(); // tipibelirtilmemiş nesne  alır tür,tip güvenirliği açısından problem -> tür convert edilebilir ama diğer yol daha makul
                add();//parametreler veri tabanına set edilmeden önce türleri belirtiliyor.tip güüvenliği açısından daha iyi
            }
            else
            {
                MessageBox.Show("Seçili öğrenci var");
            }
            
        }

        private void dataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(dataGrid.Rows[e.RowIndex].Cells["id"].Value.ToString());
            //txtAdi.Text = dataGrid.Rows[e.RowIndex].Cells["adi"].Value.ToString();
            //txtSoyadi.Text = dataGrid.Rows[e.RowIndex].Cells["soyadi"].Value.ToString();
            //comboSinif.Text = dataGrid.Rows[e.RowIndex].Cells["sinifi"].Value.ToString();

            if (e.RowIndex > -1)
            {
                ogrenciId = Convert.ToInt32(dataGrid.Rows[e.RowIndex].Cells["id"].Value.ToString());

                try
                {
                    con = new OleDbConnection(connectionString);
                    cmd = new OleDbCommand("select * from ogrenci where id = @id", con);
                    cmd.Parameters.Add("@id", OleDbType.Integer).Value = ogrenciId;
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    radioButtonErkek.Checked = false;
                    radioButtonKiz.Checked = false;
                    foreach (DataRow row in dt.Rows)
                    {
                        txtAdi.Text = row["adi"].ToString();
                        txtSoyadi.Text = row["soyadi"].ToString();
                        string cinsiyet = row["cinsiyeti"].ToString();
                        if (cinsiyet == "Erkek")
                        {
                            radioButtonErkek.Checked = true;
                        }else if(cinsiyet == "Kız")
                        {
                            radioButtonKiz.Checked = true;
                        }

                        comboSinif.Text = row["sinifi"].ToString();
                        txtNumara.Text = row["numarası"].ToString();
                    }

                }
                catch (OleDbException ex)
                {

                    throw ex;
                }
            }
        }

        private void buttonGuncelle_Click(object sender, EventArgs e)
        {
            if (ogrenciId > 0)
            {
                update();
            }
            else
            {
                MessageBox.Show("Öğrenci Seçiniz..");

            }
        }

        private void buttonSil_Click(object sender, EventArgs e)
        {
            if (ogrenciId > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Seçili öğrenciyi silmek istediğinize emin misiniz?","Öğrenci Sil",MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    delete();
                    clean();
                }
                else
                {
                    MessageBox.Show("İşlem iptal edildi..");
                }
                
            }
            else
            {
                MessageBox.Show("Öğrenci Seçiniz..");
            }
        }

        private void buttonYeniKayit_Click(object sender, EventArgs e)
        {
            clean();

        }
    }
}
