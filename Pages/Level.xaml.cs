using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFUIKitProfessional.Models;

namespace WPFUIKitProfessional.Pages
{
    public partial class Level : Page
    {
        public Models.Level CurrentLevel { get; set; }
        public Level()
        {
            InitializeComponent();
        }
        private void Button_MouseEnter(object sender, MouseEventArgs e) => Cursor = Cursors.Hand;
        private void Button_MouseLeave(object sender, MouseEventArgs e) => Cursor = Cursors.Arrow;

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
                        checkBtn.Content = "Completed!";
                        checkBtn.IsEnabled = false;
                        answerLabel.Visibility = Visibility.Hidden;
                        answerInput.Visibility = Visibility.Hidden;
                        (App.Current.MainWindow as MainWindow).rdLevels_Click(null,null);
                    }
                }
                else
                {
                    checkBtn.Background = Brushes.Red;
                }
        }
    }
}
