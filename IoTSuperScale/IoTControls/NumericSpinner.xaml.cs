using System;
using System.Text.RegularExpressions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

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
            int temp = Convert.ToInt32(txtValue.Text);
            if (temp == 0)
                btnDown.IsHitTestVisible = true;
            temp = temp + 1;
            txtValue.Text = temp.ToString();
        }
        private void ΒtnDown_Click(object sender, RoutedEventArgs e)
        {
            int temp = Convert.ToInt32(txtValue.Text);
            if (temp == 1)
                btnDown.IsHitTestVisible = false;
            temp = temp - 1;
            txtValue.Text = temp.ToString();
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
