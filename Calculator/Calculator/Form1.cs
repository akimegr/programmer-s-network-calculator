using System;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int checkRegim = 1;
        double a, b;
        string p;
        int count;
        bool znak = true;
        int flagSystemSS = 10;
        int flagSystemAfter = 10;

        void changeInSS(int ss)
        {
            try
            {
                long temp = Convert.ToInt64(textBox1.Text, flagSystemAfter);
                textBox1.Text = Convert.ToString(temp, ss);
                flagSystemAfter = ss;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Введите доступные числа");
            }

            //if (flagSystemAfter == 10)
            //{
            //    int i = Convert.ToInt32(textBox1.Text);
            //    textBox1.Text = Convert.ToString(i, ss);
            //    flagSystemSS = ss;
            //}
            //else if (flagSystemAfter == 2)
            //{

            //}
            //else if (flagSystemAfter == 8)
            //{
            //    long temp = Convert.ToInt64(textBox1.Text, 8);
            //    textBox1.Text = Convert.
            //}
            //else if (flagSystemAfter == 16)
            //{

            //}
        }


        private void button21_Click(object sender, EventArgs e)
        {
            //int i = Convert.ToInt32(textBox1.Text);
            //textBox1.Text = Convert.ToString(i, 10);
            try
            {
                changeInSS(10);
                radioButton1.Checked = true;
            }
            catch (Exception ex)
            {
                if (flagSystemAfter == 2)
                {
                    MessageBox.Show("Доступные значения одного значения: 1 и 0");
                }
                else if (flagSystemAfter == 8)
                {
                    MessageBox.Show("Доступные значения одного значения: от 0 до 7");
                }
                else if (flagSystemAfter == 16)
                {
                    MessageBox.Show("Доступные значения одного значения: от 0 до 9 и от A до B");
                }
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            changeInSS(2);
            radioButton2.Checked = true;

        }

        private void button27_Click(object sender, EventArgs e)
        {
            //int i = Convert.ToInt32(textBox1.Text);
            //textBox1.Text = Convert.ToString(i, 8);
            changeInSS(8);
            radioButton3.Checked = true;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            //int i = Convert.ToInt32(textBox1.Text);
            //textBox1.Text = Convert.ToString(i, 16);
            changeInSS(16);
            radioButton4.Checked = true;
        }

        public void myButtonVisible(bool checkVisible)
        {
            button21.Enabled = checkVisible;
            button24.Enabled = checkVisible;
            button27.Enabled = checkVisible;
            button28.Enabled = checkVisible;
            radioButton1.Enabled = checkVisible;
            radioButton2.Enabled = checkVisible;
            radioButton3.Enabled = checkVisible;
            radioButton4.Enabled = checkVisible;

        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 0;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + ",";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 1;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 2;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 3;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 4;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 5;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 6;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 7;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 8;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 9;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (znak == true)
                {
                    textBox1.Text = "-" + textBox1.Text;
                    znak = false;
                }
                else if (znak == false)
                {
                    textBox1.Text = textBox1.Text.Replace("-", "");
                    znak = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Введите корректное число! Число формата " + textBox1.Text + " имеет не верный формат");
            }

        }

        private void calculate()
        {
            if (label1.Text == "")
            {
                textBox1.Text = textBox1.Text;
                return;
            }
            float partTwo;
            if (flagSystemAfter == 10)
            {
                partTwo = float.Parse(textBox1.Text);
                switch (count)
                {
                    case 1:
                        b = a + partTwo;
                        textBox1.Text = b.ToString();
                        break;

                    case 2:
                        b = a - partTwo;
                        textBox1.Text = b.ToString();
                        break;
                    case 3:
                        b = a * partTwo;
                        textBox1.Text = b.ToString();
                        break;
                    case 4:
                        b = a / partTwo;
                        textBox1.Text = b.ToString();
                        break;

                    default:
                        break;
                }
            }

            if (flagSystemAfter != 10)
            {
                DialogResult dialogResult = MessageBox.Show("", "Вы ввели " + flagSystemAfter + "-ичное число?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    for (int i = 0; i < textBox1.Text.Length; i++)
                    {
                        if (flagSystemAfter == 16)
                        {
                            int Num;
                            bool isNum = int.TryParse(textBox1.Text[i].ToString(), out Num);
                            if (!isNum && (textBox1.Text[i].ToString().ToUpper() != "A" || textBox1.Text[i].ToString().ToUpper() != "B"
                                || textBox1.Text[i].ToString().ToUpper() != "C" || textBox1.Text[i].ToString().ToUpper() != "D" || textBox1.Text[i].ToString().ToUpper() != "E" || textBox1.Text[i].ToString().ToUpper() != "F"))
                            {
                                MessageBox.Show("Ошибка, вы ввели не " + flagSystemAfter + "-ичное число!\nВычисления не произведены");
                                return;
                            }


                        }
                        else
                        {
                            if (int.Parse(textBox1.Text[i].ToString()) > flagSystemAfter - 1)
                            {
                                
                                MessageBox.Show("Ошибка, вы ввели не " + flagSystemAfter + "-ичное число!\nВычисления не произведены");
                                return;
                            }
                        }
                    }
                    long temp1 = Convert.ToInt64(textBox1.Text, flagSystemAfter);
                    long temp2 = Convert.ToInt64(label1.Text.Substring(0, label1.Text.Length-1), flagSystemAfter);
                    long zo = 0;
                    switch (count)
                    {
                        case 1:
                            zo = temp1 + temp2;
                            label3.Text = zo.ToString();
                            textBox1.Text = Convert.ToString(zo, flagSystemAfter);
                            break;

                        case 2:
                            zo = temp1 - temp2;
                            label3.Text = zo.ToString();
                            textBox1.Text = Convert.ToString(zo, flagSystemAfter);
                            break;
                        case 3:
                            zo = temp1 * temp2;
                            label3.Text = zo.ToString();
                            textBox1.Text = Convert.ToString(zo, flagSystemAfter);
                            break;
                        case 4:
                            b = temp1 / temp2;
                            label3.Text = zo.ToString();
                            textBox1.Text = Convert.ToString(zo, flagSystemAfter);
                            break;

                        default:
                            zo = 0;
                            break;
                    }

                }
                else if (dialogResult == DialogResult.No)
                {
                    long temp1 = Convert.ToInt64(textBox1.Text, flagSystemAfter);
                    long temp2 = Convert.ToInt64(label1.Text, flagSystemAfter);

                    switch (count)
                    {
                        case 1:
                            b = temp1 + temp2;
                            textBox1.Text = b.ToString();
                            break;

                        case 2:
                            b = temp1 - temp2;
                            textBox1.Text = b.ToString();
                            break;
                        case 3:
                            b = temp1 * temp2;
                            textBox1.Text = b.ToString();
                            break;
                        case 4:
                            b = temp1 / temp2;
                            textBox1.Text = b.ToString();
                            break;

                        default:
                            break;
                    }
                }
                partTwo = Convert.ToInt64(textBox1.Text, flagSystemAfter);
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (flagSystemAfter == 10)
            {
                try
                {
                    a = float.Parse(textBox1.Text);
                    textBox1.Clear();
                    count = 1;
                    label1.Text = a.ToString() + "+";
                    znak = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Введите корректное число! Число формата " + textBox1.Text + " имеет не верный формат");
                }
            }
            else
            {
                p = textBox1.Text;
                textBox1.Clear();
                count = 1;
                label1.Text = p.ToString() + "+";
                znak = true;
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (flagSystemAfter == 10)
            {
                try
                {
                    a = float.Parse(textBox1.Text);
                    textBox1.Clear();
                    count = 2;
                    label1.Text = a.ToString() + "-";
                    znak = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Введите корректное число! Число формата " + textBox1.Text + " имеет не верный формат");
                }
            }
            else
            {
                p = textBox1.Text;
                textBox1.Clear();
                count = 2;
                label1.Text = p.ToString() + "-";
                znak = true;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (flagSystemAfter == 10)
            {
                try
                {
                    a = float.Parse(textBox1.Text);
                    textBox1.Clear();
                    count = 3;
                    label1.Text = a.ToString() + "*";
                    znak = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Введите корректное число! Число формата " + textBox1.Text + " имеет не верный формат");
                }
            }
            else
            {
                p = textBox1.Text;
                textBox1.Clear();
                count = 3;
                label1.Text = p.ToString() + "*";
                znak = true;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (flagSystemAfter == 10)
            {
                try
                {
                    a = float.Parse(textBox1.Text);
                    textBox1.Clear();
                    count = 4;
                    label1.Text = a.ToString() + "/";
                    znak = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Введите корректное число! Число формата " + textBox1.Text + " имеет не верный формат");
                }
            }
            else
            {
                p = textBox1.Text;
                textBox1.Clear();
                count = 4;
                label1.Text = p.ToString() + "/";
                znak = true;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            try
            {
                calculate();
                label1.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Введите корректное число! Число формата " + textBox1.Text + " имеет не верный формат");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            label1.Text = "";
        }

        private void button20_Click(object sender, EventArgs e)
        {
            checkRegim++;
            if (checkRegim % 2 == 0)
            {
                button20.Text = "В обычный калькулятор";
                myButtonVisible(true);
            }
            else
            {
                button20.Text = "В калькулятор программиста";

                myButtonVisible(false);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            flagSystemAfter = 8;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            flagSystemAfter = 2;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            flagSystemAfter = 16;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            flagSystemAfter = 10;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int lenght = textBox1.Text.Length - 1;
            string text = textBox1.Text;
            textBox1.Clear();
            for (int i = 0; i < lenght; i++)
            {
                textBox1.Text = textBox1.Text + text[i];
            }
        }


    }
}
