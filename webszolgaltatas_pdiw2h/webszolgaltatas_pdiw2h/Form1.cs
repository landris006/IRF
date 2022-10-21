using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using webszolgaltatas_pdiw2h.MnbServiceReference;

namespace webszolgaltatas_pdiw2h
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            MNBArfolyamServiceSoapClient client = new MNBArfolyamServiceSoapClient();
            GetExchangeRatesRequestBody requestBody = new GetExchangeRatesRequestBody
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30",
            };

            GetExchangeRatesResponseBody response =  client.GetExchangeRates(requestBody);
        }
    }
}
