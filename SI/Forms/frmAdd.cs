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
    public partial class frmAdd : Form
    {
        public frmAdd()
        {
            InitializeComponent();
             LoadTheme();
        }

        List<dtAnimal> _dtGrid = new List<dtAnimal>();
        private void frmAdd_Load(object sender, EventArgs e)
        {
            cbo();
            Grid();
        }

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
                       select new dtAnimal
                       {
                           IdAnimal = c.IdAnimal,
                           Quantidade = c.Quantidade,
                           Preco = c.Preco,
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
