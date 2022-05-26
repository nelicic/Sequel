﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
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
            var levelpage = (Application.Current.MainWindow as MainWindow).Level;
            levelpage.CurrentLevel = GetLevelAsync(int.Parse((sender as Button).Content.ToString())).Result;
            levelpage.question.Text = levelpage.CurrentLevel.Question;
            levelpage.query.Text = string.Empty;
            levelpage.levelnumber.Text = "Level " + levelpage.CurrentLevel.Id;
            levelpage.answerInput.Text = string.Empty;
            if (levelpage.CurrentLevel.Visible == 0)
            {
                levelpage.checkBtn.Visibility = Visibility.Hidden;
                levelpage.answerLabel.Visibility = Visibility.Hidden;
                levelpage.answerInput.Visibility = Visibility.Hidden;
            }
            else
            {
                levelpage.checkBtn.Visibility = Visibility.Visible;
                levelpage.answerLabel.Visibility = Visibility.Visible;
                levelpage.answerInput.Visibility = Visibility.Visible;
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
    }
}
