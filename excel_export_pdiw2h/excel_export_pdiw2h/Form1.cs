using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace excel_export_pdiw2h
{
    public partial class Form1 : Form
    {
        private List<Flat> flats;
        private RealEstateEntities realEstateEntities = new RealEstateEntities();

        public Form1()
        {
            InitializeComponent();
        }

        private void loadData()
        {
            flats = realEstateEntities.Flat.ToList();
        }
    }
}
