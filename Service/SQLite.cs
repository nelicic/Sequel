using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFUIKitProfessional.Service
{
    public class SQLite
    {
        public SQLiteConnection sqlconn;
        public SQLiteCommand sqlCmd;
        public DataTable dataTable = new DataTable();
        public readonly DataSet ds = new DataSet();
        public SQLiteDataAdapter dbSqlite = new SQLiteDataAdapter();

        public SQLite(string path)
        {
            ConnectionStringSettings c = ConfigurationManager.ConnectionStrings[path];
            sqlconn = new SQLiteConnection(c.ToString());
        }

        public void Query(string query)
        {
            sqlconn.Open();
            sqlCmd = sqlconn.CreateCommand();
            string CommandText = Simplify(query);
            dbSqlite = new SQLiteDataAdapter(CommandText, sqlconn);
            ds.Reset();
        }

        public string Simplify(string text)
        {
            while (text.Contains("\t"))
                text = text.Replace("\t", " ");
            while (text.Contains("\n") || text.Contains("\r"))
                text = text.Replace("\n", " ").Replace("\r", " ");
            while (text.Contains("  "))
                text = text.Replace("  ", " ");
            return text;
        }

        public void Execute(DataGrid dataGrid, TextBlock answerLabel)
        {
            try
            {
                dbSqlite.Fill(ds);
                dataTable = ds.Tables[0];
                dataGrid.ItemsSource = dataTable.AsDataView();
            }
            catch (Exception)
            {
                answerLabel.Visibility = Visibility.Visible;
                answerLabel.Foreground = Brushes.Red;
                answerLabel.Text = "Query Error";
                dataGrid.ItemsSource = null;
            }
            sqlconn.Close();
        }
    }
}
