using Microsoft.Reporting.WinForms;
using Newtonsoft.Json;
using SI.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SI.Forms
{
    public partial class FrmPrint : Form
    {
        public FrmPrint()
        {
            InitializeComponent();
        }

        private void FrmPrint_Load(object sender, EventArgs e)
        {
            getDataReport();
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

        private void getDataReport()
        {
            var cGado = JsonConvert.DeserializeObject<IEnumerable<GetAll>>(GetData("CompraGado"));

            DataGrid dtg = new DataGrid();

            dtg.DataSource = cGado.ToList();

            ReportDataSource rptDataSource = new ReportDataSource();


            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", cGado));

            this.reportViewer1.RefreshReport();
        }


        private string GetData(string UriData)
        {
            var Uri = ConfigurationManager.AppSettings["Uri"] + UriData;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Uri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                var response = client.GetAsync(Uri).Result;
                var strGet = string.Empty;
                if (response.IsSuccessStatusCode)
                {
                    strGet = response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    strGet = "Error";
                }

                return strGet;
            }
        }
    }
}
