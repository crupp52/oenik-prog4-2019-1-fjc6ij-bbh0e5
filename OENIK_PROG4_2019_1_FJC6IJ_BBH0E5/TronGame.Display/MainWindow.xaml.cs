namespace TronGame.Display
{
    using System;
    using System.Collections.Generic;
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
    using TronGame.BusinessLogic;
    using TronGame.Repository;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            IRepository repository = new GameRepository();
            repository.Player1.Name = "Teszt Béla";
            repository.Player2.Name = "Teszt Elek";

            IBusinessLogic logic = new GameLogic();
            logic.SaveGamestate();
            logic.LoadGamestate("save20190413222910.xml");
        }
    }
}
