using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        static int remotePort; // Порт для отправки сообщений
        IPAddress ipAddress; // IP адрес сервера
        static Socket listeningSocket; // Сокет


        int checkRegim = 1;
        double a, b;
        string p;
        int count;
        bool znak = true;
        int flagSystemSS = 10;
        int flagSystemAfter = 10;

        string converToTenWithDouble(string text)
        {
            double text1;
            string ttt = text.Split(',')[0];
            string zel;
            long temp;
            temp = Convert.ToInt64(ttt, flagSystemAfter);
            zel = temp.ToString() + ",";
            string textZ = text.Split(',')[1];
            int size = textZ.ToString().Length - 1;
            string forFor = textZ.ToString();
            double fff = 0;
            for (int z = 0; z < forFor.Length; z++)
            {
                if (flagSystemAfter == 16)
                {
                    if (forFor[z].ToString().ToUpper() == "A") {
                        fff += (10 * Math.Pow(flagSystemAfter, -(z + 1)));
                        continue;
                    }
                    if (forFor[z].ToString().ToUpper() == "B") {
                        fff += (11 * Math.Pow(flagSystemAfter, -(z + 1)));
                        continue;
                    }
                    if (forFor[z].ToString().ToUpper() == "C") {
                        fff += (12 * Math.Pow(flagSystemAfter, -(z + 1)));
                        continue;
                    }
                    if (forFor[z].ToString().ToUpper() == "D") {
                        fff += (13 * Math.Pow(flagSystemAfter, -(z + 1)));
                        continue;
                    }
                    if (forFor[z].ToString().ToUpper() == "E") {
                        fff += (14 * Math.Pow(flagSystemAfter, -(z + 1)));
                        continue;
                    }
                    if (forFor[z].ToString().ToUpper() == "F") {
                        fff += (15 * Math.Pow(flagSystemAfter, -(z + 1)));
                        continue;
                    }
                }
                fff += (int.Parse(forFor[z].ToString()) * Math.Pow(flagSystemAfter, -(z + 1)));
            }
            double myRes = Math.Round((temp + fff), 10);
            return myRes.ToString();
        }

        string withDouble(string text, int ss, int after)
        {
            if (flagSystemAfter != 10)
            {
                text = converToTenWithDouble(textBox1.Text);
            }
            double text1;
            text1 = Convert.ToDouble(text);
            string zel;
            long temp;
            if (ss == 10)
            {
                return converToTenWithDouble(textBox1.Text);
            }
            else {
                zel = Convert.ToString(Convert.ToInt32(Math.Truncate(text1)), ss); 
            }
            int zel1 = Convert.ToInt32(Math.Truncate(text1));
            // дробную часть
            double text2;
            text2 = text1 - Math.Truncate(text1);
            int cc;
            cc = ss;
            double[] asd = new double[10];
            asd[0] = text2;

            string drob = null;

            for (int i = 1; i < 7; i++)
            {
                switch (cc)
                {
                    case 2:
                        asd[i] = (2 * asd[i - 1]) - Math.Truncate(asd[i - 1] * 2);
                        int bin = Convert.ToInt32(Math.Truncate(asd[i - 1] * 2));
                        drob += bin;
                        break;
                    case 8:
                        asd[i] = (8 * asd[i - 1]) - Math.Truncate(asd[i - 1] * 8);
                        double oct = Math.Truncate(asd[i - 1] * 8);
                        drob += oct;
                        break;
                    case 10:
                        asd[i] = (10 * asd[i - 1]) - Math.Truncate(asd[i - 1] * 10);
                        double dec = Math.Truncate(asd[i - 1] * 10);
                        drob += dec;
                        break;
                    case 16:
                        asd[i] = (16 * asd[i - 1]) - Math.Truncate(asd[i - 1] * 16);
                        string hex;
                        hex = Convert.ToString(Math.Truncate(asd[i - 1] * 16));

                        int ze;
                        ze = Convert.ToInt32(hex);

                        switch (ze)
                        {
                            case 10:
                                hex = "A";
                                break;
                            case 11:
                                hex = "B";
                                break;
                            case 12:
                                hex = "C";
                                break;
                            case 13:
                                hex = "D";
                                break;
                            case 14:
                                hex = "E";
                                break;
                            case 15:
                                hex = "F";
                                break;
                            default:
                                break;
                        }
                        drob += hex;
                        break;
                    default:
                        break;

                }
            }
            flagSystemAfter = cc;
            string r = zel + "," + drob;
            return r;

        }

        void changeInSS(int ss)
        {
            try
            {
                if (textBox1.Text.Split(',').Length == 2)
                {
                    textBox1.Text = withDouble(textBox1.Text, ss, flagSystemAfter);
                    radioButton2.Checked = true;
                    flagSystemAfter = ss;
                    return;
                }
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
                else
                {
                    MessageBox.Show("Введены не верные значения");
                }
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            try
            {
                float partTwo = float.Parse(textBox1.Text);
                string forSend = a.ToString();
                forSend += "&";
                forSend += 5;
                forSend += "&";
                forSend += textBox1.Text;
                forSend += "&";
                forSend += flagSystemAfter.ToString();

                label4.Text = forSend;

                string message = forSend;
                byte[] data = Encoding.Unicode.GetBytes(message);
                EndPoint remotePoint = new IPEndPoint(ipAddress, remotePort);
                listeningSocket.SendTo(data, remotePoint);

                StringBuilder builder = new StringBuilder(); // получаем сообщение
                int bytes = 0; // количество полученных байтов
                byte[] data2 = new byte[1000]; // буфер для получаемых данных
                EndPoint remoteIp = new IPEndPoint(ipAddress, 0); //адрес, с которого пришли данные

                do
                {
                    bytes = listeningSocket.ReceiveFrom(data, ref remoteIp);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (listeningSocket.Available > 0);
                MessageBox.Show(builder.ToString());
                changeInSS(2);
                radioButton2.Checked = true;
            }
            catch
            {
                MessageBox.Show("Введены неверные значения");
            }

        }

        private void button27_Click(object sender, EventArgs e)
        {
            try
            {
                //int i = Convert.ToInt32(textBox1.Text);
                //textBox1.Text = Convert.ToString(i, 8);
                float partTwo = float.Parse(textBox1.Text);
                string forSend = a.ToString();
                forSend += "&";
                forSend += 6;
                forSend += "&";
                forSend += textBox1.Text;
                forSend += "&";
                forSend += flagSystemAfter.ToString();

                label4.Text = forSend;

                string message = forSend;
                byte[] data = Encoding.Unicode.GetBytes(message);
                EndPoint remotePoint = new IPEndPoint(ipAddress, remotePort);
                listeningSocket.SendTo(data, remotePoint);

                StringBuilder builder = new StringBuilder(); // получаем сообщение
                int bytes = 0; // количество полученных байтов
                byte[] data2 = new byte[1000]; // буфер для получаемых данных
                EndPoint remoteIp = new IPEndPoint(ipAddress, 0); //адрес, с которого пришли данные

                do
                {
                    bytes = listeningSocket.ReceiveFrom(data, ref remoteIp);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (listeningSocket.Available > 0);
                MessageBox.Show(builder.ToString());
                changeInSS(2);
                radioButton2.Checked = true;
                changeInSS(8);
                radioButton3.Checked = true;
            }
            catch
            {
                MessageBox.Show("Введены неверные значения");
            }
}

        private void button28_Click(object sender, EventArgs e)
        {
            try
            {
                //int i = Convert.ToInt32(textBox1.Text);
                //textBox1.Text = Convert.ToString(i, 16);
                float partTwo = float.Parse(textBox1.Text);
                string forSend = a.ToString();
                forSend += "&";
                forSend += 7;
                forSend += "&";
                forSend += textBox1.Text;
                forSend += "&";
                forSend += flagSystemAfter.ToString();

                label4.Text = forSend;

                string message = forSend;
                byte[] data = Encoding.Unicode.GetBytes(message);
                EndPoint remotePoint = new IPEndPoint(ipAddress, remotePort);
                listeningSocket.SendTo(data, remotePoint);

                StringBuilder builder = new StringBuilder(); // получаем сообщение
                int bytes = 0; // количество полученных байтов
                byte[] data2 = new byte[1000]; // буфер для получаемых данных
                EndPoint remoteIp = new IPEndPoint(ipAddress, 0); //адрес, с которого пришли данные

                do
                {
                    bytes = listeningSocket.ReceiveFrom(data, ref remoteIp);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (listeningSocket.Available > 0);
                MessageBox.Show(builder.ToString());
                changeInSS(2);
                radioButton2.Checked = true;
                changeInSS(16);
                radioButton4.Checked = true;
            }
                        
            catch
            {
                MessageBox.Show("Введены неверные значения");
            }
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

            float partTwo = float.Parse(textBox1.Text);
            string forSend = a.ToString();
            forSend += "&";
            forSend += count.ToString();
            forSend += "&";
            forSend += textBox1.Text;
            forSend += "&";
            forSend += flagSystemAfter.ToString();

            label4.Text = forSend;


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
                    long temp2 = Convert.ToInt64(label1.Text.Substring(0, label1.Text.Length - 1), flagSystemAfter);
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

        private void button22_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Введите ip, порт!");

            List<string> list = new List<string>();


            try
            {
                listeningSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp); // Создание сокета
                Task listeningTask = new Task(Listen); // Создание потока
                listeningTask.Start(); // Запуск потока
                ipAddress = IPAddress.Parse(IP.Text);
                remotePort = Int32.Parse(PORT.Text);

                while (true) // Отправление сообщений серверу в бесконечном цикле
                {

                    float partTwo = float.Parse(textBox1.Text);
                    string forSend = a.ToString();
                    forSend += "&";
                    forSend += count.ToString();
                    forSend += "&";
                    forSend += textBox1.Text;
                    forSend += "&";
                    forSend += flagSystemAfter.ToString();

                    label4.Text = forSend;

                    string message = forSend;
                    byte[] data = Encoding.Unicode.GetBytes(message);
                    EndPoint remotePoint = new IPEndPoint(ipAddress, remotePort);
                    listeningSocket.SendTo(data, remotePoint);

                    StringBuilder builder = new StringBuilder(); // получаем сообщение
                    int bytes = 0; // количество полученных байтов
                    byte[] data2 = new byte[256]; // буфер для получаемых данных
                    EndPoint remoteIp = new IPEndPoint(ipAddress, 0); //адрес, с которого пришли данные

                    do
                    {
                        bytes = listeningSocket.ReceiveFrom(data, ref remoteIp);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (listeningSocket.Available > 0);
                    MessageBox.Show(builder.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Close();
            }


        }

        private static void Listen()
        {
            try
            {
                IPEndPoint localIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0); // Прослушиваем по адресу
                listeningSocket.Bind(localIP);

                while (true)
                {
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    byte[] data = new byte[256];

                    EndPoint remoteIp = new IPEndPoint(IPAddress.Any, 0);

                    do
                    {
                        bytes = listeningSocket.ReceiveFrom(data, ref remoteIp);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (listeningSocket.Available > 0);

                    IPEndPoint remoteFullIp = remoteIp as IPEndPoint;

                    Console.WriteLine("{0}:{1} - {2}", remoteFullIp.Address.ToString(), remoteFullIp.Port, builder.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Close();
            }
        }
        // закрытие сокета
        private static void Close()
        {
            if (listeningSocket != null)
            {
                listeningSocket.Shutdown(SocketShutdown.Both);
                listeningSocket.Close();
                listeningSocket = null;
            }

            Console.WriteLine("Сервер остановлен!");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            try
            {
                listeningSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp); // Создание сокета

                ipAddress = IPAddress.Parse(IP.Text);
                remotePort = Int32.Parse(PORT.Text);




            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void button25_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("", "Вы ввели " + flagSystemAfter + "-ичное число?", MessageBoxButtons.YesNo);
            float checkTwoPart = 0;
            if (dialogResult == DialogResult.Yes)
            {
                if (label1.Text.Substring(0, label1.Text.Length - 1).Split(',').Length<2 && textBox1.Text.Split(',').Length<2) 
                {
                    for (int i = 0; i < textBox1.Text.Length; i++)
                    {
                        if (flagSystemAfter == 16)
                        {
                            int Num;
                            bool isNum = int.TryParse(textBox1.Text[i].ToString(), out Num);
                            if (!isNum && !(textBox1.Text[i].ToString().ToUpper() != "A" || textBox1.Text[i].ToString().ToUpper() != "B"
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
                    checkTwoPart = Convert.ToInt64(textBox1.Text, flagSystemAfter);
                    for (int i = 0; i < label1.Text.Substring(0, label1.Text.Length - 1).Length; i++)
                    {
                        if (flagSystemAfter == 16)
                        {
                            int Num;
                            bool isNum = int.TryParse(label1.Text.Substring(0, label1.Text.Length - 1)[i].ToString(), out Num);
                            if (!isNum && !(label1.Text.Substring(0, label1.Text.Length - 1)[i].ToString().ToUpper() != "A" || label1.Text.Substring(0, label1.Text.Length - 1)[i].ToString().ToUpper() != "B"
                                || label1.Text.Substring(0, label1.Text.Length - 1)[i].ToString().ToUpper() != "C" || label1.Text.Substring(0, label1.Text.Length - 1)[i].ToString().ToUpper() != "D" || label1.Text.Substring(0, label1.Text.Length - 1)[i].ToString().ToUpper() != "E" || label1.Text.Substring(0, label1.Text.Length - 1)[i].ToString().ToUpper() != "F"))
                            {
                                MessageBox.Show("Ошибка, вы ввели не " + flagSystemAfter + "-ичное число!\nВычисления не произведены");
                                return;
                            }


                        }
                        else
                        {
                            if (int.Parse(label1.Text.Substring(0, label1.Text.Length - 1)[i].ToString()) > flagSystemAfter - 1)
                            {

                                MessageBox.Show("Ошибка, вы ввели не " + flagSystemAfter + "-ичное число!\nВычисления не произведены");
                                return;
                            }
                        }
                    }
                    a = Convert.ToInt64(label1.Text.Substring(0, label1.Text.Length - 1), flagSystemAfter); 
                }
                else
                {
                    if(label1.Text.Substring(0, label1.Text.Length - 1).Split(',').Length == 2)
                    {
                        for (int ch = 0; ch < 2; ch++)
                        {
                            for (int i = 0; i < label1.Text.Substring(0, label1.Text.Length - 1).Split(',')[ch].Length; i++)
                            {
                                if (flagSystemAfter == 16)
                                {
                                    int Num;
                                    bool isNum = int.TryParse(label1.Text.Substring(0, label1.Text.Length - 1)[i].ToString(), out Num);
                                    if (!isNum && !(label1.Text.Substring(0, label1.Text.Length - 1).Split(',')[ch][i].ToString().ToUpper() != "A" || label1.Text.Substring(0, label1.Text.Length - 1).Split(',')[ch][i].ToString().ToUpper() != "B"
                                        || label1.Text.Substring(0, label1.Text.Length - 1).Split(',')[ch][i].ToString().ToUpper() != "C" || label1.Text.Substring(0, label1.Text.Length - 1).Split(',')[ch][i].ToString().ToUpper() != "D" || label1.Text.Substring(0, label1.Text.Length - 1).Split(',')[ch][i].ToString().ToUpper() != "E" || label1.Text.Substring(0, label1.Text.Length - 1).Split(',')[ch][i].ToString().ToUpper() != "F"))
                                    {
                                        MessageBox.Show("Ошибка, вы ввели не " + flagSystemAfter + "-ичное число!\nВычисления не произведены");
                                        return;
                                    }


                                }
                                else
                                {
                                    if (int.Parse(label1.Text.Substring(0, label1.Text.Length - 1).Split(',')[ch][i].ToString()) > flagSystemAfter - 1)
                                    {

                                        MessageBox.Show("Ошибка, вы ввели не " + flagSystemAfter + "-ичное число!\nВычисления не произведены");
                                        return;
                                    }
                                }
                            }
                        }
                        a = double.Parse(converToTenWithDouble(label1.Text.Substring(0, label1.Text.Length - 1)));
                    }
                    else
                    {
                        for (int i = 0; i < label1.Text.Substring(0, label1.Text.Length - 1).Length; i++)
                        {
                            if (flagSystemAfter == 16)
                            {
                                int Num;
                                bool isNum = int.TryParse(textBox1.Text[i].ToString(), out Num);
                                if (!isNum && !(textBox1.Text[i].ToString().ToUpper() != "A" || textBox1.Text[i].ToString().ToUpper() != "B"
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
                        a = Convert.ToInt64(label1.Text.Substring(0, label1.Text.Length - 1), flagSystemAfter);

                    }
                    if (textBox1.Text.Split(',').Length == 2)
                    {
                        for (int ch = 0; ch < 2; ch++)
                        {
                            for (int i = 0; i < textBox1.Text.Split(',')[ch].Length; i++)
                            {
                                if (flagSystemAfter == 16)
                                {
                                    int Num;
                                    bool isNum = int.TryParse(textBox1.Text[i].ToString(), out Num);
                                    if (!isNum && !(textBox1.Text.Split(',')[ch][i].ToString().ToUpper() != "A" || textBox1.Text.Split(',')[ch][i].ToString().ToUpper() != "B"
                                        || textBox1.Text.Split(',')[ch][i].ToString().ToUpper() != "C" || textBox1.Text.Split(',')[ch][i].ToString().ToUpper() != "D" || textBox1.Text.Split(',')[ch][i].ToString().ToUpper() != "E" || textBox1.Text.Split(',')[ch][i].ToString().ToUpper() != "F"))
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
                        }
                        checkTwoPart = float.Parse(converToTenWithDouble(textBox1.Text));
                    }
                    else
                    {
                        for (int i = 0; i < textBox1.Text.Split(',').Length; i++)
                        {
                            if (flagSystemAfter == 16)
                            {
                                int Num;
                                bool isNum = int.TryParse(textBox1.Text[i].ToString(), out Num);
                                if (!isNum && !(textBox1.Text.Split(',')[i].ToString().ToUpper() != "A" || textBox1.Text.Split(',')[i].ToString().ToUpper() != "B"
                                    || textBox1.Text.Split(',')[i].ToString().ToUpper() != "C" || textBox1.Text.Split(',')[i].ToString().ToUpper() != "D" || textBox1.Text.Split(',')[i].ToString().ToUpper() != "E" || textBox1.Text.Split(',')[i].ToString().ToUpper() != "F"))
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
                        checkTwoPart = Convert.ToInt64(textBox1.Text, flagSystemAfter);
                    }
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                if (flagSystemAfter == 10)
                {
                    long convertTwoPart = long.Parse(textBox1.Text);
                    //checkTwoPart = Convert.ToInt64(textBox1.Text, flagSystemAfter);
                    a = Convert.ToInt64(label1.Text.Substring(0, label1.Text.Length - 1), flagSystemAfter);
                }
                if (label1.Text.Substring(0, label1.Text.Length - 1).Split(',').Length == 1 && textBox1.Text.Split(',').Length==1)
                {
                    long convertTwoPart = long.Parse(textBox1.Text);
                    string convertMy = Convert.ToString(convertTwoPart, flagSystemAfter);
                    checkTwoPart = float.Parse(convertMy);
                    //checkTwoPart = Convert.ToInt64(textBox1.Text, flagSystemAfter);
                    a = Convert.ToInt64(label1.Text.Substring(0, label1.Text.Length - 1), flagSystemAfter); 
                }
                else
                {
                    if (textBox1.Text.Split(',').Length == 2)
                    {
                        checkTwoPart = float.Parse(converToTenWithDouble(textBox1.Text));
                    }
                    else
                    {
                        checkTwoPart = Convert.ToInt64(textBox1.Text, flagSystemAfter);
                    }
                    if (label1.Text.Substring(0, label1.Text.Length - 1).Split(',').Length == 2)
                    {
                        a = double.Parse(converToTenWithDouble(label1.Text.Substring(0, label1.Text.Length - 1)));
                    }
                    else
                    {
                        a = Convert.ToInt64(label1.Text.Substring(0, label1.Text.Length - 1), flagSystemAfter);
                    }
                }


            }
            try
            {
                float partTwo = float.Parse(textBox1.Text);
                string forSend = a.ToString();
                forSend += "&";
                forSend += count.ToString();
                forSend += "&";
                forSend += checkTwoPart;
                forSend += "&";
                forSend += flagSystemAfter.ToString();

                label4.Text = forSend;

                string message = forSend;
                byte[] data = Encoding.Unicode.GetBytes(message);
                EndPoint remotePoint = new IPEndPoint(ipAddress, remotePort);
                listeningSocket.SendTo(data, remotePoint);

                StringBuilder builder = new StringBuilder(); // получаем сообщение
                int bytes = 0; // количество полученных байтов
                byte[] data2 = new byte[1000]; // буфер для получаемых данных
                EndPoint remoteIp = new IPEndPoint(ipAddress, 0); //адрес, с которого пришли данные

                do
                {
                    bytes = listeningSocket.ReceiveFrom(data, ref remoteIp);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (listeningSocket.Available > 0);
                MessageBox.Show(builder.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            Close();
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
