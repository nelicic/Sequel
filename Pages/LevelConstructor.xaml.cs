using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFUIKitProfessional.Models;

namespace WPFUIKitProfessional.Pages
{
    public partial class LevelConstructor : Page
    {
        public LevelConstructor()
        {
            InitializeComponent();
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e) => Cursor = Cursors.Hand;
        private void Button_MouseLeave(object sender, MouseEventArgs e) => Cursor = Cursors.Arrow;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            error.Visibility = Visibility.Hidden;
            if (task.Text != string.Empty && answer.Text != string.Empty)
            {
                Models.Level level = new Models.Level()
                {
                    Question = task.Text,
                    Path = ((TextBlock)((ComboBoxItem)database.SelectedItem).Content).Text,
                    ERDiagram = ((TextBlock)((ComboBoxItem)database.SelectedItem).Content).Text == "PlayList" ? "/PlayLists.png" : "/University.png",
                    Visible = ((TextBlock)((ComboBoxItem)type.SelectedItem).Content).Text == "Type 1" ? 1 : 0,
                };

                if (((TextBlock)((ComboBoxItem)type.SelectedItem).Content).Text == "Type 1")
                    level.Answer = answer.Text;
                else
                    level.SQLanswer = answer.Text;

                ApplicationContext db = new ApplicationContext();
                db.Levels.Load();
                DataContext = db.Levels.Local.ToBindingList();
                db.Levels.Add(level);
                db.SaveChanges();

                (Application.Current.MainWindow as MainWindow).Levels = new Levels();
            }
            else
                error.Visibility = Visibility.Visible;
        }
    }
}
