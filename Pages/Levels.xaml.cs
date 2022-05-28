using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFUIKitProfessional.Models;

namespace WPFUIKitProfessional.Pages
{
    public partial class Levels : Page
    {
        private ApplicationContext db;
        private Brush Color { get; set; }
        public Levels()
        {
            InitializeComponent();

            db = new ApplicationContext();
            db.Levels.Load();
            DataContext = db.Levels.Local.ToBindingList();
            List<int> levelId = GetLevelIdAsync().Result;
            foreach (int id in levelId)
            {
                Button levelBtn = new Button();

                levelBtn.Background = (Brush)FindResource("SecundaryBackgroundColor");
                levelBtn.Foreground = new SolidColorBrush(Colors.Black);
                levelBtn.Content = id.ToString();
                levelBtn.Name = "Button" + id.ToString();
                levelBtn.Width = levelBtn.Height = 60;
                
                levelBtn.Margin = new Thickness(10);
                levelBtn.Style = FindResource("Authorization") as Style;
                levelBtn.MouseEnter += Btn_MouseEnter;
                levelBtn.MouseLeave += Btn_MouseLeave;
                levelBtn.Click += LevelBtn_Click;

                sidebar.Children.Add(levelBtn);
            }
        }

        public async Task<List<int>> GetLevelIdAsync()
            => await db.Levels.Select(x => x.Id).ToListAsync();
        public async Task<Models.Level> GetLevelAsync(int id)
            => await db.Levels.FirstOrDefaultAsync(x => x.Id == id);

        private void LevelBtn_Click(object sender, RoutedEventArgs e)
        {
            var levelPage = (Application.Current.MainWindow as MainWindow).Level;
            levelPage.CurrentLevel = GetLevelAsync(int.Parse((sender as Button).Content.ToString())).Result;
            levelPage.question.Text = levelPage.CurrentLevel.Question;
            levelPage.query.Text = string.Empty;
            Uri imageUri = new Uri(levelPage.CurrentLevel.ERDiagram, UriKind.Absolute);
            levelPage.img.Source = new BitmapImage(imageUri);
            levelPage.levelnumber.Text = "Level " + levelPage.CurrentLevel.Id;
            levelPage.answerInput.Text = string.Empty;
            levelPage.answerLabel.Foreground = Brushes.Black;
            levelPage.answerLabel.Text = "Input answer:";

            if ((sender as Button).Background.ToString() == "#FF7CFC00")
            {
                if (levelPage.CurrentLevel.Visible == 0)
                    LevelVisible0Completed(levelPage);
                else
                    LevelVisible1Completed(levelPage);
            }
            else
            {
                if (levelPage.CurrentLevel.Visible == 0)
                    LevelVisible0NotCompleted(levelPage);
                else
                    LevelVisible1NotCompleted(levelPage);
            }

            levelFrameContent.Navigate((App.Current.MainWindow as MainWindow).Level);
        }

        private void Btn_MouseEnter(object sender, MouseEventArgs e)    
        {
            Color = (sender as Button).Background;
            if ((sender as Button).Background == Brushes.Green)
                (sender as Button).Background = Brushes.LawnGreen;
            else
                (sender as Button).Background = Brushes.White;
            Cursor = Cursors.Hand;
        }
        private void Btn_MouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = Color;
            Cursor = Cursors.Arrow;
        }

        private void LevelVisible1Completed(Level levelPage)
        {
            levelPage.execute.Background = (Brush)FindResource("PrimaryBackgroundColor");
            levelPage.checkBtn.Background = Brushes.LawnGreen;

            levelPage.execute.Content = "Execute";
            levelPage.checkBtn.Content = "Completed";
            levelPage.answerInput.Text = levelPage.CurrentLevel.Answer;

            levelPage.execute.IsEnabled = true;
            levelPage.checkBtn.IsEnabled = false;
            levelPage.answerInput.IsReadOnly = true;

            levelPage.checkBtn.Visibility = Visibility.Visible;
            levelPage.answerLabel.Visibility = Visibility.Visible;
            levelPage.answerInput.Visibility = Visibility.Visible;
        }

        private void LevelVisible0Completed(Level levelPage)
        {
            levelPage.execute.Background = Brushes.LawnGreen;
            levelPage.execute.Content = "Completed";
            levelPage.execute.IsEnabled = false;
            levelPage.query.Text = levelPage.CurrentLevel.SQLanswer;

            levelPage.checkBtn.Visibility = Visibility.Hidden;
            levelPage.answerInput.Visibility = Visibility.Hidden;
            levelPage.answerLabel.Visibility = Visibility.Hidden;
        }
        private void LevelVisible1NotCompleted(Level levelPage)
        {
            levelPage.checkBtn.Background = (Brush)FindResource("PrimaryBackgroundColor");
            levelPage.execute.Background = (Brush)FindResource("PrimaryBackgroundColor");
            levelPage.execute.Content = "Execute";
            levelPage.checkBtn.Content = "Check";

            levelPage.checkBtn.IsEnabled = true;
            levelPage.execute.IsEnabled = true;
            levelPage.answerInput.IsReadOnly = false;

            levelPage.checkBtn.Visibility = Visibility.Visible;
            levelPage.answerLabel.Visibility = Visibility.Visible;
            levelPage.answerInput.Visibility = Visibility.Visible;
        }
        private void LevelVisible0NotCompleted(Level levelPage)
        {
            levelPage.execute.Background = (Brush)FindResource("PrimaryBackgroundColor");
            levelPage.execute.Content = "Execute";

            levelPage.execute.IsEnabled = true;

            levelPage.checkBtn.Visibility = Visibility.Hidden;
            levelPage.answerLabel.Visibility = Visibility.Hidden;
            levelPage.answerInput.Visibility = Visibility.Hidden;
        }
    }
}
