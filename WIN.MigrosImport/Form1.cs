using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WIN.MigrosImport
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable insDt = new DataTable();
            insDt.Columns.Add("Id", typeof(int));
            insDt.Columns.Add("Name", typeof(string));
            insDt.Columns.Add("Address", typeof(string));
            insDt.Columns.Add("City", typeof(string));
            insDt.Columns.Add("District", typeof(string));
            insDt.Columns.Add("X", typeof(string));
            insDt.Columns.Add("Y", typeof(string));
            insDt.AcceptChanges();

            for (int i = 1; i < 9999; i++)
            {
                
                WebRequest req = HttpWebRequest.Create("http://www.migros.com.tr/magaza/ADANA/" + Convert.ToString(i));
                req.Method = "GET";
                string source;
                using (StreamReader reader = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    source = reader.ReadToEnd();

                    #region Name of Migros
                    string nameOfStore = string.Empty;
                    string[] productDetailTitle_mb10 = source.Split(new string[] { "<h2 class=\"productDetailTitle mb10\">" }, StringSplitOptions.None);
                    nameOfStore = productDetailTitle_mb10[1].Split(new string[] { "</h2>" }, StringSplitOptions.None)[0].TrimStart().TrimEnd();
                    if (nameOfStore != string.Empty)
                    {
                        #region Address
                        string address = string.Empty;
                        string[] productDetailTable = source.Split(new string[] { "<ul class=\"productDetailTable\">" }, StringSplitOptions.None);
                        if (productDetailTable.Length > 0)
                            address = productDetailTable[1].Split(new string[] { "<p>Adres</p>" }, StringSplitOptions.None)[1];
                        address = address.Replace("\r\n", "").TrimStart().Replace("<p>", "").Split(new string[] { "</p>" }, StringSplitOptions.None)[0];
                        #endregion

                        #region İl - İlçe
                        string il = string.Empty;
                        string ilce = string.Empty;
                        string[] productDetailTable_1 = source.Split(new string[] { "<ul class=\"productDetailTable\">" }, StringSplitOptions.None);
                        if (productDetailTable_1.Length > 0)
                        {
                            il = productDetailTable_1[1].Split(new string[] { "<p>İl - İlçe</p>" }, StringSplitOptions.None)[1];
                            ilce = productDetailTable_1[1].Split(new string[] { "<p>İl - İlçe</p>" }, StringSplitOptions.None)[1];
                        }
                        il = il.Replace("\r\n", "").TrimStart().Replace("<p>", "").Split(new string[] { "</p>" }, StringSplitOptions.None)[0];
                        ilce = ilce.Replace("\r\n", "").TrimStart().Replace("<p>", "").Split(new string[] { "</p>" }, StringSplitOptions.None)[0];
                        il = il.Split('-')[0].TrimStart().TrimEnd();
                        ilce = ilce.Split('-')[1].TrimStart().TrimEnd();
                        #endregion

                        #region Telefon
                        string telefon = string.Empty;
                        string[] productDetailTable_2 = source.Split(new string[] { "<ul class=\"productDetailTable\">" }, StringSplitOptions.None);
                        if (productDetailTable_2.Length > 0)
                        {
                            telefon = productDetailTable_2[1].Split(new string[] { "<p>Telefon</p>" }, StringSplitOptions.None)[1];
                        }
                        telefon = telefon.Replace("\r\n", "").TrimStart().Replace("<p>", "").Split(new string[] { "</p>" }, StringSplitOptions.None)[0];
                        telefon = telefon.Replace(" *", "").TrimStart().TrimEnd();
                        #endregion


                        WebRequest req1 = HttpWebRequest.Create("http://www.migros.com.tr/Pages/MagazaHarita.aspx?ID=" + Convert.ToString(i) + "&reloadedOnce=true&KeepThis=true&height=500&width=760");
                        req.Method = "GET";
                        string source1;
                        string xCoordinate = string.Empty;
                        string yCoordinate = string.Empty;
                        using (StreamReader reader1 = new StreamReader(req1.GetResponse().GetResponseStream()))
                        {
                            source1 = reader1.ReadToEnd();

                            string[] pair = source1.Split(new string[] { "load('" }, StringSplitOptions.None);
                            xCoordinate = pair[1].Split(new string[] { "','" }, StringSplitOptions.None)[0];
                            yCoordinate = pair[1].Split(new string[] { "','" }, StringSplitOptions.None)[1];
                        }

                        DataRow insDr = insDt.NewRow();
                        insDr["Id"] = i;
                        insDr["Name"] = nameOfStore;
                        insDr["Address"] = address;
                        insDr["City"] = il;
                        insDr["District"] = ilce;
                        insDr["X"] = xCoordinate;
                        insDr["Y"] = yCoordinate;
                        insDt.Rows.Add(insDr);

                        Console.WriteLine(i.ToString() + "\t\t" + nameOfStore);
                    }
                    else
                    {
                        Console.WriteLine(i.ToString());
                    }
                    #endregion

                   
                }
            }

            gridControl1.DataSource = insDt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gridControl1.ExportToXlsx("C:\\RetailData\\Migros.xlsx");
        }
    }
}
