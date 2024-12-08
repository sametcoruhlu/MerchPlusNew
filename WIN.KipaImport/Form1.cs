using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WIN.KipaImport
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string content = textBox1.Text;
            string[] shops = content.Split('#');

            DataTable insDt = new DataTable();
            insDt.Columns.Add("Name", typeof(string));
            insDt.Columns.Add("Address", typeof(string));
            insDt.Columns.Add("PhoneNumber", typeof(string));
            insDt.Columns.Add("City", typeof(string));
            insDt.Columns.Add("District", typeof(string));
            insDt.Columns.Add("CoordinateX", typeof(string));
            insDt.Columns.Add("CoordinateY", typeof(string));
            insDt.AcceptChanges();

            foreach (string shop in shops)
            {
                string[] lines = shop.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                DataRow insDr = insDt.NewRow();
                insDr["Name"] = lines[1];
                insDr["Address"] = lines[2];
                insDr["PhoneNumber"] = lines[3];
                insDr["City"] = lines[4].Split('/')[0];
                insDr["District"] = lines[4].Split('/')[1];
                insDr["CoordinateX"] = lines[5].Split(',')[0];
                insDr["CoordinateY"] = lines[5].Split(',')[1];
                insDt.Rows.Add(insDr);
            }

            gridControl1.DataSource = insDt;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            gridControl1.ExportToXlsx("C:\\RetailData\\MetroData.xlsx");
        }
    }
}
