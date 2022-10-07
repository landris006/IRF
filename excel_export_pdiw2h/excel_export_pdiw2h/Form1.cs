﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace excel_export_pdiw2h
{
    public partial class Form1 : Form
    {
        private List<Flat> flats;
        private RealEstateEntities realEstateEntities = new RealEstateEntities();
        private Excel.Application xlApp;
        private Excel.Workbook xlWorkBook;
        private Excel.Worksheet xlSheet;

        public Form1()
        {
            InitializeComponent();
            loadData();

            createExcel();
        }

        private void loadData()
        {
            flats = realEstateEntities.Flat.ToList();
        }

        private void createExcel()
        {
            try
            {
                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(Missing.Value);
                xlSheet = xlWorkBook.ActiveSheet;

                // createTable();

                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format($"Error: {ex.Message}, /nLine: {ex.Source}");
                MessageBox.Show(errorMessage, "Error");

                xlWorkBook.Close();
                xlApp.Quit();
                xlWorkBook = null;
                xlApp = null;
            }
        }
    }
}