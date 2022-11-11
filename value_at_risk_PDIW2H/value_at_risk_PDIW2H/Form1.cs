using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using value_at_risk_PDIW2H.Entities;

namespace value_at_risk_PDIW2H
{
    public partial class Form1 : Form
    {
        PortfolioEntities context = new PortfolioEntities();
        List<Tick> ticks;

        List<PortfolioItem> portfolioList = new List<PortfolioItem>();

        public Form1()
        {
            InitializeComponent();
            ticks = context.Tick.ToList();
            dataGridView1.DataSource = ticks;

            CreatePortfolio();

            List<decimal> Nyereségek = new List<decimal>();
            int intervalum = 30;
            DateTime kezdőDátum = (from x in ticks select x.TradingDay).Min();
            DateTime záróDátum = new DateTime(2016, 12, 30);
            TimeSpan z = záróDátum - kezdőDátum;

            for (int i = 0; i < z.Days - intervalum; i++)
            {
                decimal ny = GetPortfolioValue(kezdőDátum.AddDays(i + intervalum))
                           - GetPortfolioValue(kezdőDátum.AddDays(i));

                Nyereségek.Add(ny);
                Console.WriteLine(i + " " + ny);
            }

            var nyereségekRendezve = (from x in Nyereségek
                                      orderby x
                                      select x)
                                        .ToList();

            WriteToFile(nyereségekRendezve);

            MessageBox.Show(nyereségekRendezve[nyereségekRendezve.Count() / 5].ToString());
        }

        private void CreatePortfolio()
        {
            portfolioList.Add(new PortfolioItem() { Index = "OTP", Volume = 10 });
            portfolioList.Add(new PortfolioItem() { Index = "ZWACK", Volume = 10 });
            portfolioList.Add(new PortfolioItem() { Index = "ELMU", Volume = 10 });

            dataGridView2.DataSource = portfolioList;
        }

        private decimal GetPortfolioValue(DateTime date)
        {
            decimal value = 0;

            foreach (var item in portfolioList)
            {
                var last = (from x in ticks
                            where item.Index == x.Index.Trim()
                               && date <= x.TradingDay
                            select x)
                            .First();

                value += (decimal)last.Price * item.Volume;
            }

            return value;
        }

        private void WriteToFile(List<decimal> earnings)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            StreamWriter sw = new StreamWriter(ofd.FileName, false, Encoding.UTF8);

            sw.WriteLine("Időszak;Nyereség");

            int period = 1;
            foreach (decimal value in earnings)
            {
                sw.WriteLine($"{period};{value}");
                period++;
            }
        }
    }
}
