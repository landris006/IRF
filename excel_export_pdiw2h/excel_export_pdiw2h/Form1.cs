using System;
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
            CreateExcel();

            this.Visible = false;
        }

        private void loadData()
        {
            flats = realEstateEntities.Flat.ToList();
        }

        private void CreateExcel()
        {
            try
            {
                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(Missing.Value);
                xlSheet = xlWorkBook.ActiveSheet;

                CreateTable();

                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format($"Error: {ex.Message}, /nLine: {ex.Source}");
                MessageBox.Show(errorMessage, "Error");

                xlWorkBook.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWorkBook = null;
                xlApp = null;
            }
        }

        private void CreateTable()
        {
            string[] headers = new string[]
            {
                    "Kód",
                    "Eladó",
                    "Oldal",
                    "Kerület",
                    "Lift",
                    "Szobák száma",
                    "Alapterület (m2)",
                    "Ár (mFt)",
                    "Négyzetméter ár (Ft/m2)"
            };

            for (int i = 0; i < headers.Length; i++)
            {
                xlSheet.Cells[1, i + 1] = headers[i];
            }

            object[,] values = new object[flats.Count, headers.Length];


            for (int i = 0; i < flats.Count; i++)
            {
                Flat flat = flats[i];

                const int MFT_TO_FT = 1000000;

                values[i, 0] = flat.Code;
                values[i, 1] = flat.Vendor;
                values[i, 2] = flat.Side;
                values[i, 3] = flat.District;
                values[i, 4] = flat.Elevator ? "Van" : "Nincs";
                values[i, 5] = flat.NumberOfRooms;
                values[i, 6] = flat.FloorArea;
                values[i, 7] = flat.Price;
                values[i, 8] = $"={GetCell(i + 2, 8)} / {GetCell(i + 2, 7)} * {MFT_TO_FT}";

                xlSheet.get_Range(
                                GetCell(2, 1),
                                GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values;
            }
        }

        private string GetCell(int x, int y)
        {
            string ExcelCoordinate = "";
            int dividend = y;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                dividend = (int)((dividend - modulo) / 26);
            }
            ExcelCoordinate += x.ToString();

            return ExcelCoordinate;
        }
    }
}
