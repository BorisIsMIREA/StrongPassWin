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

namespace Generation
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

       

        private string Password(int[] P, int[] N)
        {

        char[] Kod = {'a', 'b', 'c','d','e','f','j','h','i','j','1','2','3','k','l','m','n','o','p','q','r','s','t',
            'u','v','w','4','6','5','x','z','y','A','B','C','D','I','F','G','H','I','J','7','8','9','K','L','M','N','O','P',
            'Q','R','S','T','U','V','@','W','X',')','Y','(','Z'};

        string pas = "";
            int X;
            for (int i = 0; i < 3; i++)
            {
                X = N[i] * 10 + P[0];
                if (X > 63)
                    X -= 38;
                pas += Kod[X];
                X = P[0] * 4 + N[i];
                pas += Kod[X];
            }
            for (int i = 3; i < 6; i++)
            {
                X = N[i] * 10 + P[1];
                if (X > 63)
                    X -= 41;
                pas += Kod[X];
                X = P[1] * 10 + N[i];
                if (X > 63)
                    X -= 63;
                pas += Kod[X];
            }
            for (int i = 6; i < 9; i++)
            {
                X = N[i] * 10 + P[2];
                if (X > 63)
                    X -= 45;
                pas += Kod[X];
                X = P[2] * 2 + N[i] * 3;
                pas += Kod[X];
            }
            for (int i = 9; i < 11; i++)
            {
                X = N[i] * 10 + P[3] + 1;
                if (X > 63)
                    X -= 50;
                pas += Kod[X];
                X = P[3] * 10 + N[i];
                if (X > 63)
                    X -= 44;
                pas += Kod[X];
            }

            return pas;
        }                  //алгоритм создания пароля

        private void Click(object sender, RoutedEventArgs e)
        {
            int[] P = new int[4];
            int[] N = new int[11];
            if (pin.Text=="1111"|| pin.Text == "0000" || pin.Text == "1234" || pin.Text == "4321" || pinP.Password == "1111" || pinP.Password == "0000" || pinP.Password == "1234" || pinP.Password == "4321")
            {
                MessageBox.Show("Ваш PIN крайне не надежен!", "Будте осторожны!",   MessageBoxButton.OK,  MessageBoxImage.Exclamation);
            }
            int Pin;
            string PinC;
            if (check.IsChecked == true)
            {
                if (chekP.IsChecked == true)
                    PinC = pinP.Password;
                else
                    PinC = pin.Text;
                if (PinC.Length == 4)
                {
                    if (int.TryParse(PinC, out Pin))
                    {
                        
                        for (int i = 3; i>=0; i--)
                        {
                            P[i] = Pin % 10;
                            Pin /= 10;
                        }
                    }
                    else
                    {
                        MessageBox.Show("PIN должен состоять из цифр", "Ошибка ввода PIN!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("PIN должен состоять из 4-х цифр", "Ошибка ввода PIN!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }       //ввод pin
            else
            {
                MessageBox.Show("Без PIN ваш пароль очень легко заполучить!", "Будте осторожны!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Pin = 1111;
            }                             

            string num = number.Text;
            if (number.Text.Length == 11)
            {
                int x;
                for (int i = 0; i < 11; i++)
                {
                    if (int.TryParse(num[i].ToString(), out x))
                    {
                        N[i] = x;
                    }
                    else
                    {
                        MessageBox.Show("Номер должен состоять из цифр", "Ошибка ввода PIN!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }       //ввод num
            else
            {
                MessageBox.Show("Номер должен быть введен так: 89012223344", "Неверный формат номера!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            pas.Text = (Password(P, N));
            pas.Visibility = Visibility.Visible;
            Copy.Visibility = Visibility.Visible;
            label.Visibility = Visibility.Collapsed;
            number.Visibility = Visibility.Collapsed;
            check.Visibility = Visibility.Collapsed;
            Pin_label.Visibility = Visibility.Collapsed;
            pin.Visibility = Visibility.Collapsed;
            Button.Visibility = Visibility.Collapsed;
            pinP.Visibility = Visibility.Collapsed;
            chekP.Visibility = Visibility.Collapsed;

        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(pas.Text);
            Copy.Visibility = Visibility.Collapsed;
            label.Visibility = Visibility.Visible;
            number.Visibility = Visibility.Visible;
            check.Visibility = Visibility.Visible;
            Pin_label.Visibility = Visibility.Visible;
            pin.Visibility = Visibility.Hidden;
            Button.Visibility = Visibility.Visible;
            pas.Visibility = Visibility.Collapsed;
            pas.Clear();
            number.Clear();
            pin.Clear();
            pinP.Clear();
            pinP.Visibility = Visibility.Visible;
            check.IsChecked = true;
            chekP.Visibility = Visibility.Visible;
        }

        private void ChecedP(object sender, RoutedEventArgs e)
        {
            pinP.Password = pin.Text;
            pinP.Visibility = Visibility.Visible;
            pin.Visibility = Visibility.Hidden;
        }

        private void UncheckedP(object sender, RoutedEventArgs e)
        {
            pin.Text = pinP.Password;           
            pinP.Visibility = Visibility.Hidden;
            pin.Visibility = Visibility.Visible;
        }

        private void Unchecked(object sender, RoutedEventArgs e)
        {
            Pin_label.Visibility = Visibility.Hidden;
            pin.Visibility = Visibility.Hidden;
            pinP.Visibility = Visibility.Hidden;
            chekP.Visibility = Visibility.Hidden;
        }   //без пин

        private void Checked(object sender, RoutedEventArgs e)
        {
            Pin_label.Visibility = Visibility.Visible;
            pin.Visibility = Visibility.Hidden;
            if (chekP != null)
            {
                chekP.Visibility = Visibility.Visible;
                chekP.IsChecked = true;
            }
            if (pinP != null)
                pinP.Visibility = Visibility.Visible;
            
        }     //с пин
    }
}
