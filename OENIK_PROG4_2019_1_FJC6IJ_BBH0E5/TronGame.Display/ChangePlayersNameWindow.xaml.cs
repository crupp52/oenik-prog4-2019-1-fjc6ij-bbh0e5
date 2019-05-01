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
using System.Windows.Shapes;

namespace TronGame.Display
{
    /// <summary>
    /// Interaction logic for ChangePlayersNameWindow.xaml
    /// </summary>
    public partial class ChangePlayersNameWindow : Window
    {
        private GameControl control;

        public ChangePlayersNameWindow(GameControl gameControl)
        {
            InitializeComponent();

            control = gameControl;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name1 = this.player1name.Text;
            string name2 = this.player2name.Text;

            if (name1 != string.Empty && name2 != string.Empty)
            {
                this.control.ChangePlayersName(name1, name2);
                this.Close();
            }
        }
    }
}
