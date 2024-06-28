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

namespace HM_9_WPF.WorkWindow
{
    /// <summary>
    /// Логика взаимодействия для MyWorkWindow.xaml
    /// </summary>
    public partial class MyWorkWindow : Window
    {
        public MyWorkWindow()
        {
            InitializeComponent();
            txtText.PreviewTextInput += InputTextBoxSplit_PreviewTextInput;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void buttonMinimizeClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void buttonCloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonBackClick(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void InputTextBoxSplit_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Проверка на ввод чисел
            if (IsNumber(e.Text))
            {
                // запрет на ввод чисел
                e.Handled = true;

                TextBlock errorMessageBlock = new TextBlock
                {
                    Text = "Ввод чисел запрещен!",
                    Foreground = new SolidColorBrush(Colors.Red)
                };

                if (IsInputTextBoxEmpty(txtText))
                {
                    wordsListBox.Items.Clear();
                }

                wordsListBox.Items.Add(errorMessageBlock);
            }
        }

        private bool IsNumber(string text)
        {
            foreach (char c in text)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }

        public void OnSplitWordsClick(object sender, RoutedEventArgs e)
        {
            if (IsInputTextBoxEmpty(txtText))
            {
                DisplayErrorMassege("Ввод не может быть пустым!!! ");
                return;
            }

            wordsListBox.Items.Clear();
            reversedLabel.Content = string.Empty;

            //для TextBlock
            //wordsListBox.Inlines.Clear();

            string inputText = txtText.Text;

            string[] words = inputText.Split(' ');

            foreach (string word in words)
            {
                ListBoxItem item = new ListBoxItem
                {
                    Content = word,
                    Foreground = new SolidColorBrush(Colors.Red)
                };

                wordsListBox.Items.Add(item);

                //для TextBlock
                //wordsListBox.Inlines.Add(item);
            }
        }


        public void OnReverseWordsClick(object sender, RoutedEventArgs e)
        {
            if (IsInputTextBoxEmpty(txtText))
            {

                DisplayErrorMassege("Ввод не может быть пустым!!! ");
                return;

            }

            wordsListBox.Items.Clear();

            string inputText = txtText.Text;

            // переворачиваем слова
            string reversedWord = ReverseWord(inputText);

            reversedLabel.Content = reversedWord;

            // Делим слова
            string[] words = reversedWord.Split(' ');

            foreach (string word in words)
            {
                ListBoxItem item = new ListBoxItem
                {
                    Content = word,
                    Foreground = new SolidColorBrush(Colors.Red)
                };

                wordsListBox.Items.Add(item);
            }

        }

        private string ReverseWord(string word)
        {
            char[] charArray = word.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public bool IsInputTextBoxEmpty(TextBox textBox)
        {
            return string.IsNullOrEmpty(textBox.Text);
        }

        private void DisplayErrorMassege(string errorMessage)
        {
            ListBoxItem errorItem = new ListBoxItem
            {
                Content = new TextBlock

                {
                    Text = errorMessage,
                    Foreground = new SolidColorBrush(Colors.Red)
                }

            };

            if (IsInputTextBoxEmpty(txtText))
            {
                wordsListBox.Items.Clear();
            }

            wordsListBox.Items.Add(errorItem);

            //для TextBlock
            //wordsListBox.Inlines.Add(errorItem);
        }

        private void buttonClearClick(object sender, RoutedEventArgs e)
        {

            txtText.Clear();

            wordsListBox.Items.Clear();

            reversedLabel.Content = string.Empty;
        }
    }
}
