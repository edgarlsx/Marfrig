using Microsoft.IdentityModel.Protocols;
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
    public partial class frmFind : Form
    {
        public frmFind()
        {
            InitializeComponent();
            LoadTheme(); 
            cbo();
            Grid();
        }

        List<dtGrid> _dtGrid = new List<dtGrid>();
        private void LoadTheme()
        {
            foreach (Control btns in this.Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    btn.BackColor = ThemeColor.PrimaryColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
                }
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            var lst = _dtGrid
                        .Where(c => c.idCompraGado == int.Parse(txtId.Text))
                        .Where(c => c.Nome == comboBox1.SelectedItem.ToString())
                        .Where(c => c.Data_Entrega >= DateTime.Parse(dtEntregaIni.Text) && c.Data_Entrega <= DateTime.Parse(dtEntregaFim.Text)).ToList();

            dataGridView1.DataSource = lst;
            dataGridView1.Refresh();
        }

        private void cbo()
        {
            var pecuarista = JsonConvert.DeserializeObject<List<Pecuarista>>(GetData("Pecuarista"));

            comboBox1.Items.Clear();
            foreach (var p in pecuarista)
            {
                comboBox1.Items.Add(p.Nome);
                comboBox1.Items.IndexOf(p.Id);
            }
        }

        private void Grid()
        {
            var cGado = JsonConvert.DeserializeObject<List<GetAll>>(GetData("CompraGado"));

            var lst = (from c in cGado
                        select new dtGrid
                        {
                            idCompraGado = c.IdCompraGado,
                            Nome = c.Nome,
                            Data_Entrega = c.DataEntrega,
                            Valor_Total = c.Quantidade * c.Preco
                        });
            _dtGrid = lst.ToList();
            dataGridView1.DataSource = _dtGrid;
            dataGridView1.Refresh();
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
