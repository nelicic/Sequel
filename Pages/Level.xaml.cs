using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WPFUIKitProfessional.Models;

namespace WPFUIKitProfessional.Pages
{
    public partial class Level : Page
    {
        public Models.Level CurrentLevel { get; set; }

        private SQLiteConnection sqlconn;
        private DataTable dataTable = new DataTable();
        private readonly DataSet ds = new DataSet();
        private SQLiteDataAdapter dbSqlite = new SQLiteDataAdapter();

        public Level()
        {
            InitializeComponent();
        }
        private void Button_MouseEnter(object sender, MouseEventArgs e) => Cursor = Cursors.Hand;
        private void Button_MouseLeave(object sender, MouseEventArgs e) => Cursor = Cursors.Arrow;
        private void erDiagram_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as TextBlock).Background = Brushes.White;
            (sender as TextBlock).Foreground = (Brush)FindResource("PrimaryTextColor");
            popupImg.IsOpen = true;
        }
        private void erDiagram_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as TextBlock).Background = (Brush)FindResource("PrimaryBackgroundColor");
            (sender as TextBlock).Foreground = Brushes.White;
            popupImg.IsOpen = false;
        }
        private void SetConnection(string path)
        {
            sqlconn = new SQLiteConnection(ConfigurationManager.ConnectionStrings[path].ConnectionString);
        }

        private async void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            answerLabel.Foreground = Brushes.Black;
            answerLabel.Text = "Input answer:";

            SetConnection(CurrentLevel.Path);
            sqlconn.Open();
            string CommandText = Simplify(query.Text);

            dbSqlite = new SQLiteDataAdapter(CommandText, sqlconn);
            ds.Reset();
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
            }

            if (CurrentLevel.Visible == 0)
            {
                if (CommandText.ToUpper() == CurrentLevel.SQLanswer.ToUpper())
                {
                    var db = new ApplicationContext();
                    db.CompletedLevels.Load();
                    DataContext = db.CompletedLevels.Local.ToBindingList();
                    List<CompletedLevel> listOfLevels = await db.CompletedLevels.ToListAsync();
                    if (listOfLevels.Where(x => x.UserId == (App.Current.MainWindow as MainWindow).CurrentUser.Id && x.LevelId == CurrentLevel.Id && x.Passed == 1).ToList().Count == 0)
                    {
                        db.CompletedLevels.Add(new CompletedLevel((App.Current.MainWindow as MainWindow).CurrentUser.Id, CurrentLevel.Id, 1));
                        db.SaveChanges();
                    }

                    execute.Background = Brushes.LawnGreen;
                    execute.Content = "Completed";
                    execute.IsEnabled = false;
                    answerLabel.Visibility = Visibility.Hidden;
                    (App.Current.MainWindow as MainWindow).rdLevels_Click(null, null);
                }
                else
                {
                    execute.Background = Brushes.Red;
                }
            }  

            sqlconn.Close();
        }

        private async void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentLevel.Visible == 1)
                if (CurrentLevel.Answer == answerInput.Text)
                {
                    var db = new ApplicationContext();
                    db.CompletedLevels.Load();
                    DataContext = db.CompletedLevels.Local.ToBindingList();
                    List<CompletedLevel> listOfLevels = await db.CompletedLevels.ToListAsync();
                    if (listOfLevels.Where(x => x.UserId == (App.Current.MainWindow as MainWindow).CurrentUser.Id && x.LevelId == CurrentLevel.Id && x.Passed == 1).ToList().Count == 0)
                    {
                        db.CompletedLevels.Add(new CompletedLevel((App.Current.MainWindow as MainWindow).CurrentUser.Id, CurrentLevel.Id, 1));
                        db.SaveChanges();
                        checkBtn.Background = Brushes.LawnGreen;
                        checkBtn.Content = "Completed";
                        checkBtn.IsEnabled = false;
                        answerInput.IsReadOnly = true;
                        (App.Current.MainWindow as MainWindow).rdLevels_Click(null, null);
                    }
                }
                else
                {
                    checkBtn.Background = Brushes.Red;
                }
        }

        private string Simplify(string text)
        {
            while (text.Contains("  "))
                text = text.Replace("  ", " ");
            return text;
        }
    }
}
