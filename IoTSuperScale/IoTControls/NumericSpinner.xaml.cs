using System;
using System.Text.RegularExpressions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace IoTSuperScale.IoTControls
{
    public sealed partial class NumericSpinner : UserControl
    {
        int step;

        public string TextValueProperty
        {
            get { return txtValue.Text; }
            set { txtValue.Text = value; }
        }
        public NumericSpinner()
        {
            this.InitializeComponent();
        }
        private void ΒtnUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ignoreMe;
                bool successfullyParsed = int.TryParse(txtValue.Text, out ignoreMe);
                if (successfullyParsed)
                {
                    if (ignoreMe == 0)
                        btnDown.IsHitTestVisible = true;

                    ignoreMe = ignoreMe + 1;
                    txtValue.Text = ignoreMe.ToString();
                }
            }
            catch (Exception)
            {
            }
        }
        private void ΒtnDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ignoreMe;
                bool successfullyParsed = int.TryParse(txtValue.Text, out ignoreMe);
                if (successfullyParsed)
                {
                    if (ignoreMe == 0)
                        btnDown.IsHitTestVisible = false;
                    else
                    {
                        ignoreMe = ignoreMe - 1;
                        txtValue.Text = ignoreMe.ToString();
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            step = Convert.ToInt32(txtValue.Text);
        }

        private void txtValue_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {

                if (!Regex.IsMatch(sender.Text, "^\\d*?\\d*$") && sender.Text != "")
                {
                    int pos = sender.SelectionStart - 1;
                    sender.Text = sender.Text.Remove(pos, 1);
                    sender.SelectionStart = pos;
                }
        }
    }
}
