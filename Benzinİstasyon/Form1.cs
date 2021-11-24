using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Benzinİstasyon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // Depo bilgileri için
        double D_benzin95 = 0, D_benzin97 = 0, D_dizel = 0, D_eurodizel = 0, D_lpg = 0;

        // Eklenen miktarlar için
        double E_benzin95 = 0, E_benzin97 = 0, E_dizel = 0, E_eurodizel = 0, E_lpg = 0;

        // Fiyatlar için
        double F_benzin95 = 0, F_benzin97 = 0, F_dizel = 0, F_eurodizel = 0, F_lpg = 0;

        // Satılan miktarlar için
        double S_benzin95 = 0, S_benzin97 = 0, S_dizel = 0, S_eurodizel = 0, S_lpg = 0;

        // satış sayfasında ki combobox'ın yakıt türünü seçip numericUpDown'ları aktif pasif yapmak için yazdığımız kod bloğu
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text=="Benzin (95)")
            {
                numericUpDown1.Enabled = true;
                numericUpDown2.Enabled = false;
                numericUpDown3.Enabled = false;
                numericUpDown4.Enabled = false;
                numericUpDown5.Enabled = false;
            }
            if (comboBox1.Text == "Benzin (97)")
            {
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = true;
                numericUpDown3.Enabled = false;
                numericUpDown4.Enabled = false;
                numericUpDown5.Enabled = false;
            }
            if (comboBox1.Text == "Dizel")
            {
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;
                numericUpDown3.Enabled = true;
                numericUpDown4.Enabled = false;
                numericUpDown5.Enabled = false;
            }
            if (comboBox1.Text == "Euro Dizel)")
            {
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;
                numericUpDown3.Enabled = false;
                numericUpDown4.Enabled = true;
                numericUpDown5.Enabled = false;
            }
            if (comboBox1.Text == "LPG")
            {
                numericUpDown1.Enabled = false;
                numericUpDown2.Enabled = false;
                numericUpDown3.Enabled = false;
                numericUpDown4.Enabled = false;
                numericUpDown5.Enabled = true;
            }

            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
            numericUpDown4.Value = 0;
            numericUpDown5.Value = 0;
            label36.Text = "__________";
        }

        // satış sayfasında seçtiğimiz yakıt türüne ve fiyatına göre satış yapmamızı sağlayan  ve kalan depo bilgilerini güncellediğimiz kod bloğu.
        private void btn_satis_Click(object sender, EventArgs e)
        {
            S_benzin95 = double.Parse(numericUpDown1.Value.ToString());
            S_benzin97 = double.Parse(numericUpDown2.Value.ToString());
            S_dizel = double.Parse(numericUpDown3.Value.ToString());
            S_eurodizel = double.Parse(numericUpDown4.Value.ToString());
            S_lpg = double.Parse(numericUpDown5.Value.ToString());

            if (numericUpDown1.Enabled==true)
            {
                D_benzin95 = D_benzin95 - S_benzin95;
                label36.Text = Convert.ToString(S_benzin95 * F_benzin95);
                label7.Text = "TL";
            }
           else if (numericUpDown2.Enabled == true)
            {
                D_benzin97 = D_benzin97 - S_benzin97;
                label36.Text = Convert.ToString(S_benzin97 * F_benzin97);
                label7.Text = "TL";
            }
           else if (numericUpDown3.Enabled == true)
            {
                D_dizel = D_dizel - S_dizel;
                label36.Text = Convert.ToString(S_dizel * F_dizel);
                label7.Text = "TL";
            }
           else if (numericUpDown4.Enabled == true)
            {
                D_eurodizel = D_eurodizel - S_eurodizel;
                label36.Text = Convert.ToString(S_eurodizel * F_eurodizel);
                label7.Text = "TL";
            }
           else if (numericUpDown5.Enabled == true)
            {
                D_lpg = D_lpg - S_lpg;
                label36.Text = Convert.ToString(S_lpg * F_lpg);
                label7.Text = "TL";
            }

            depo_bilgileri[0] = Convert.ToString(D_benzin95);
            depo_bilgileri[1] = Convert.ToString(D_benzin97);
            depo_bilgileri[2] = Convert.ToString(D_dizel);
            depo_bilgileri[3] = Convert.ToString(D_eurodizel);
            depo_bilgileri[4] = Convert.ToString(D_lpg);

            System.IO.File.WriteAllLines(Application.StartupPath + "\\depo.txt",depo_bilgileri);
            txt_depo_oku();
            txt_depo_yaz();
            progressBar_guncelle();
            numericupdown_value();

            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
            numericUpDown4.Value = 0;
            numericUpDown5.Value = 0;
        }
        string[] depo_bilgileri;
        string[] fiyat_bilgileri;

        // depo bilgileri sayfasında ki depoumuzu güncellediğimiz kod bloğu.
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                E_benzin95 = Convert.ToDouble(txt_depo_b95.Text);
                if (1000 < D_benzin95 + E_benzin95 || E_benzin95 <= 0)
                    txt_depo_b95.Text = "HATA!";
                else depo_bilgileri[0] = Convert.ToString(D_benzin95 + E_benzin95);
                   
            }
            catch (Exception)
            {

                txt_depo_b95.Text = "HATA!";
            }
            try
            {
                E_benzin97 = Convert.ToDouble(txt_depo_b97.Text);
                if (1000 < D_benzin97 + E_benzin97 || E_benzin97 <= 0)
                    txt_depo_b97.Text = "HATA!";
                else depo_bilgileri[1] = Convert.ToString(D_benzin97 + E_benzin97);

            }
            catch (Exception)
            {

                txt_depo_b97.Text = "HATA!";
            }
            try
            {
                E_dizel = Convert.ToDouble(txt_depo_dz.Text);
                if (1000 < D_dizel + E_dizel || E_dizel <= 0)
                    txt_depo_dz.Text = "HATA!";
                else depo_bilgileri[2] = Convert.ToString(D_dizel + E_dizel);

            }
            catch (Exception)
            {

                txt_depo_dz.Text = "HATA!";
            }
            try
            {
                E_eurodizel = Convert.ToDouble(txt_depo_edz.Text);
                if (1000 < D_eurodizel + E_eurodizel || E_eurodizel <= 0)
                    txt_depo_edz.Text = "HATA!";
                else depo_bilgileri[3] = Convert.ToString(D_eurodizel + E_eurodizel);

            }
            catch (Exception)
            {

                txt_depo_edz.Text = "HATA!";
            }
            try
            {
                E_lpg = Convert.ToDouble(txt_depo_lpg.Text);
                if (1000 < D_lpg + E_lpg || E_lpg <= 0)
                    txt_depo_lpg.Text = "HATA!";
                else depo_bilgileri[4] = Convert.ToString(D_lpg + E_lpg);

            }
            catch (Exception)
            {

                txt_depo_lpg.Text = "HATA!";
            }
            
            System.IO.File.WriteAllLines(Application.StartupPath + "\\depo.txt", depo_bilgileri);
            txt_depo_oku();
            txt_depo_yaz();
            progressBar_guncelle();
            numericupdown_value();
        }

        // fiyat bilgileri sayfasında ki fiyatlara yaptığımız zam, indirim kod bloğu.
               private void btn_fiyat_guncelle_Click(object sender, EventArgs e)
        {
            try
            {
                F_benzin95 = F_benzin95 + (F_benzin95 * Convert.ToDouble(txt_fiyat_b95.Text)/100);
                fiyat_bilgileri[0] = Convert.ToString(F_benzin95);
            }
            catch (Exception)
            {

                txt_fiyat_b95.Text = "HATA!";
            }
            try
            {
                F_benzin97 = F_benzin97 + (F_benzin97 * Convert.ToDouble(txt_fiyat_b97.Text) / 100);
                fiyat_bilgileri[1] = Convert.ToString(F_benzin97);
            }
            catch (Exception)
            {

                txt_fiyat_b97.Text = "HATA!";
            }
            try
            {
                F_dizel = F_dizel + (F_dizel * Convert.ToDouble(txt_fiyat_dz.Text) / 100);
                fiyat_bilgileri[2] = Convert.ToString(F_dizel);
            }
            catch (Exception)
            {

                txt_fiyat_dz.Text = "HATA!";
            }
            try
            {
                F_eurodizel = F_eurodizel + (F_eurodizel * Convert.ToDouble(txt_fiyat_edz.Text) / 100);
                fiyat_bilgileri[3] = Convert.ToString(F_eurodizel);
            }
            catch (Exception)
            {

                txt_fiyat_edz.Text = "HATA!";
            }
            try
            {
                F_lpg = F_lpg + (F_lpg * Convert.ToDouble(txt_fiyat_lpg.Text) / 100);
                fiyat_bilgileri[4] = Convert.ToString(F_lpg);
            }
            catch (Exception)
            {

                txt_fiyat_lpg.Text = "HATA!";
            }
            System.IO.File.WriteAllLines(Application.StartupPath + "\\fiyat.txt", fiyat_bilgileri);
            txt_fiyat_oku();
            txt_fiyat_yaz();
        }

        // depo bilgilerini hafızaya aldığımız kod bloğu.
        private void txt_depo_oku()
        {
          depo_bilgileri = System.IO.File.ReadAllLines(Application.StartupPath + "\\depo.txt");
            D_benzin95 = Convert.ToDouble(depo_bilgileri[0]);
            D_benzin97 = Convert.ToDouble(depo_bilgileri[1]);
            D_dizel = Convert.ToDouble(depo_bilgileri[2]);
            D_eurodizel = Convert.ToDouble(depo_bilgileri[3]);
            D_lpg = Convert.ToDouble(depo_bilgileri[4]);
        }

        // depo bilgilerini yazdırdığımız aldığımız kod bloğu.
        private void txt_depo_yaz()
        {
            lblDB95.Text = D_benzin95.ToString("N");
            lblDB97.Text = D_benzin97.ToString("N");
            lblDDizel.Text = D_dizel.ToString("N");
            lblDEDizel.Text = D_eurodizel.ToString("N");
            lblDlpg.Text = D_lpg.ToString("N");
        }

        // fiyat bilgilerini hafızaya aldığımız kod bloğu.
        private void txt_fiyat_oku()
        {
            fiyat_bilgileri = System.IO.File.ReadAllLines(Application.StartupPath + "\\fiyat.txt");
            F_benzin95 = Convert.ToDouble(fiyat_bilgileri[0]);
            F_benzin97 = Convert.ToDouble(fiyat_bilgileri[1]);
            F_dizel = Convert.ToDouble(fiyat_bilgileri[2]);
            F_eurodizel = Convert.ToDouble(fiyat_bilgileri[3]);
            F_lpg = Convert.ToDouble(fiyat_bilgileri[4]);
        }

        // fiyat bilgilerini  yazdırdığımız kod bloğu.
        private void txt_fiyat_yaz()
        {
            lblFB95.Text = F_benzin95.ToString("N");
            lblFB97.Text = F_benzin97.ToString("N");
            lblFDizel.Text = F_dizel.ToString("N");
            lblFEDizel.Text = F_eurodizel.ToString("N");
            lblFLpg.Text = F_lpg.ToString("N");
        }

        // progressBar'ı depoda ki yakıta göre  güncellediğimiz kod bloğu.
        private void progressBar_guncelle()
        {
            progressBar1.Value = Convert.ToInt16(D_benzin95);
            progressBar2.Value = Convert.ToInt16(D_benzin97);
            progressBar3.Value = Convert.ToInt16(D_dizel);
            progressBar4.Value = Convert.ToInt16(D_eurodizel);
            progressBar5.Value = Convert.ToInt16(D_lpg);
        }

        // numericUpDown'un depoda ki yakıttan daha fazla satışı olmaması için engellediğimiz kod bloğu.
        private void numericupdown_value()
        {
            numericUpDown1.Maximum = decimal.Parse(D_benzin95.ToString());
            numericUpDown2.Maximum = decimal.Parse(D_benzin97.ToString());
            numericUpDown3.Maximum = decimal.Parse(D_dizel.ToString());
            numericUpDown4.Maximum = decimal.Parse(D_eurodizel.ToString());
            numericUpDown5.Maximum = decimal.Parse(D_lpg.ToString());
        }

        // form ekranı geldiğinde çalışması gereken bilgilerimizi , metodlarımızı çağırdımız alan.
        private void Form1_Load(object sender, EventArgs e)
        {
            progressBar1.Maximum = 1000;
            progressBar2.Maximum = 1000;
            progressBar3.Maximum = 1000;
            progressBar4.Maximum = 1000;
            progressBar5.Maximum = 1000;
            txt_depo_oku();
            txt_depo_yaz();
            txt_fiyat_oku();
            txt_fiyat_yaz();
            progressBar_guncelle();
            numericupdown_value();

            string[] yakit_turleri = { "Benzin (95)", "Benzin (97)", "Dizel", "Euro Dizel", "LPG" };
            comboBox1.Items.AddRange(yakit_turleri);

            numericUpDown1.Enabled = false;
            numericUpDown2.Enabled = false;
            numericUpDown3.Enabled = false;
            numericUpDown4.Enabled = false;
            numericUpDown5.Enabled = false;

            numericUpDown1.DecimalPlaces = 2;
            numericUpDown2.DecimalPlaces = 2;
            numericUpDown3.DecimalPlaces = 2;
            numericUpDown4.DecimalPlaces = 2;
            numericUpDown5.DecimalPlaces = 2;

            numericUpDown1.Increment = 0.1M;
            numericUpDown2.Increment = 0.1M;
            numericUpDown3.Increment = 0.1M;
            numericUpDown4.Increment = 0.1M;
            numericUpDown5.Increment = 0.1M;

            numericUpDown1.ReadOnly = true;
            numericUpDown2.ReadOnly = true;
            numericUpDown3.ReadOnly = true;
            numericUpDown4.ReadOnly = true;
            numericUpDown5.ReadOnly = true;
        }
    }
}
