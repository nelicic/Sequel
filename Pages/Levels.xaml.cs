using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFUIKitProfessional.Models;

namespace WPFUIKitProfessional.Pages
{
    public partial class Levels : Page
    {
        ApplicationContext db;
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
            var levelpage = (App.Current.MainWindow as MainWindow).Level;
            levelpage.CurrentLevel = GetLevelAsync(int.Parse((sender as Button).Content.ToString())).Result;
            levelpage.question.Text = levelpage.CurrentLevel.Question;
            levelpage.query.Text = string.Empty;
            levelpage.levelnumber.Text = "Level " + levelpage.CurrentLevel.Id;
            levelFrameContent.Navigate((App.Current.MainWindow as MainWindow).Level);
        }

        private void Btn_MouseEnter(object sender, MouseEventArgs e) => Cursor = Cursors.Hand;
        private void Btn_MouseLeave(object sender, MouseEventArgs e) => Cursor = Cursors.Arrow;
    }
}
