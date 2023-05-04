using System;
using System.Windows.Forms;
using System.Xml;

namespace Foto
{
    public partial class Form1 : Form
    {
        Settings settings;
        int quantity = 1;
        decimal price = 0;
        string name;
        public Form1()
        {
            InitializeComponent();
            try
            {
                settings = new Settings();
                InitializeSettings();
            }
            catch (XmlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Exit();
            }
        }

        private void Exit()
        {
            if (Application.MessageLoop)
            {
                // WinForms app
                Application.Exit();
            }
            else
            {
                // Console app
                Environment.Exit(1);
            }
        }

        private void InitializeSettings()
        {
            textBoxPrice9.Text = settings.Price9x12.ToString();
            price = settings.Price9x12;
            name = radioButton9.Text;
            textBoxPrice12.Text = settings.Price12x15.ToString();
            textBoxPrice18.Text = settings.Price18x24.ToString();
            textBoxQuantity.Text = quantity.ToString();
        }

        private void textBoxPrice9_TextChanged(object sender, EventArgs e)
        {
            ChangeSettingsPrice(textBoxPrice9, SetPrice9);
        }

        private void textBoxPrice12_TextChanged(object sender, EventArgs e)
        {
            ChangeSettingsPrice(textBoxPrice12, SetPrice12);
        }

        private void textBoxPrice18_TextChanged(object sender, EventArgs e)
        {
            ChangeSettingsPrice(textBoxPrice18, SetPrice18);
        }

        private void ChangeSettingsPrice(TextBox textBox, Action<decimal> SetValue)
        {
            if (decimal.TryParse(textBox.Text, out var result))
            {
                try
                {
                    SetValue(result);
                }
                catch (XmlException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Exit();
                }
            }
            else
            {
                textBox.Text = price.ToString();
            }
        }

        private void SetPrice9(decimal result)
        {
            settings.Price9x12 = result;
        }

        private void SetPrice12(decimal result)
        {
            settings.Price12x15 = result;
        }

        private void SetPrice18(decimal result)
        {
            settings.Price18x24 = result;
        }


        private void textBoxQuantity_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxQuantity.Text, out int result))
            {
                quantity = result;
            }
            else
            {
                textBoxQuantity.Text = quantity.ToString();
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            listBoxResult.Items.Insert(0,$"{name} foto {price} x {quantity} = {price * quantity} грн.");
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton9.Checked)
            {
                price = settings.Price9x12;
                name = radioButton9.Text;
            }
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton12.Checked)
            {
                price = settings.Price12x15;
                name = radioButton12.Text;
            }
        }

        private void radioButton18_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton18.Checked)
            {
                price = settings.Price18x24;
                name = radioButton18.Text;
            }
        }
    }
}
