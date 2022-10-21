using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            ProcessXML(Request());
        }

        private GetExchangeRatesResponseBody Request()
        {
            MNBArfolyamServiceSoapClient client = new MNBArfolyamServiceSoapClient();
            GetExchangeRatesRequestBody requestBody = new GetExchangeRatesRequestBody
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30",
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
                decimal value = Convert.ToDecimal(childElement.GetAttribute("InnerText"));

                RateData newRateData = new RateData {
                    Date = Convert.ToDateTime(element.GetAttribute("date")),
                    Currency = childElement.GetAttribute("curr"),
                    Value = unit != 0 ? value / unit : value,
                };

                rates.Add(newRateData);
            }
        }
    }
}
