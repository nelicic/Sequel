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
            List<int> levelId = GetLevelId().Result;
            foreach (int id in levelId)
            {
                Button levelBtn = new Button();

                levelBtn.Content = id.ToString();
                levelBtn.Name = "Button" + id.ToString();
                levelBtn.Width = levelBtn.Height = 50;
                levelBtn.Margin = new Thickness(10);
                levelBtn.Style = FindResource("Authorization") as Style;
                levelBtn.MouseEnter += Btn_MouseEnter;
                levelBtn.MouseLeave += Btn_MouseLeave;
                levelBtn.Click += LevelBtn_Click;

                sidebar.Children.Add(levelBtn);
            }
        }

        public async Task<List<int>> GetLevelId()
        {
            return await db.Levels.Select(x => x.Id).ToListAsync();
        }

        private void LevelBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Level " + (sender as Button).Content);
        }

        private void Btn_MouseEnter(object sender, MouseEventArgs e) => Cursor = Cursors.Hand;
        private void Btn_MouseLeave(object sender, MouseEventArgs e) => Cursor = Cursors.Arrow;
    }
}
