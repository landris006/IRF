using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using webszolgaltatas_pdiw2h.Entities;
using webszolgaltatas_pdiw2h.MnbServiceReference;

namespace webszolgaltatas_pdiw2h
{
    public partial class Form1 : Form
    {
        BindingList<RateData> rates = new BindingList<RateData>();

        public Form1()
        {
            InitializeComponent();
            RefreshData();
        }

        private void RefreshData()
        {
            rates.Clear();
            ProcessXML(Request());
            Visualize();
        }

        private GetExchangeRatesResponseBody Request()
        {
            MNBArfolyamServiceSoapClient client = new MNBArfolyamServiceSoapClient();
            GetExchangeRatesRequestBody requestBody = new GetExchangeRatesRequestBody
            {
                currencyNames = comboBox1?.SelectedItem?.ToString() ?? "EUR",
                startDate = dateTimePicker1.Value.ToString(),
                endDate = dateTimePicker2.Value.ToString(),
            };

            return client.GetExchangeRates(requestBody);
        }

        private void ProcessXML(GetExchangeRatesResponseBody response)
        {
            XmlDocument xml = new XmlDocument();

            xml.LoadXml(response.GetExchangeRatesResult);

            foreach (XmlElement element in xml.DocumentElement)
            {
                XmlElement childElement = ((XmlElement)element.ChildNodes[0]);
                decimal unit = Convert.ToDecimal(childElement.GetAttribute("unit"));
                decimal value = Convert.ToDecimal(childElement.InnerText);

                RateData newRateData = new RateData {
                    Date = Convert.ToDateTime(element.GetAttribute("date")),
                    Currency = childElement.GetAttribute("curr"),
                    Value = unit != 0 ? value / unit : value,
                };

                dataGridView1.DataSource = rates;
                rates.Add(newRateData);
            }
        }

        private void Visualize()
        {
            Series series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            chartRateData.Legends[0].Enabled = false;

            ChartArea chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;

            chartRateData.DataSource = rates;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
