using System;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace serverCons
{
    class Program
    {
        static int localPort; // порт приема сообщений
        static Socket listeningSocket; // Сокет

        static List<IPEndPoint> clients = new List<IPEndPoint>(); // Список "подключенных" клиентов
       static  int flagSystemAfter;

        static void Main(string[] args)
        {
            Console.WriteLine("SERVER VERSIO1 1");
            Console.Write("Введите порт для приема сообщений: ");
            localPort = Int32.Parse(Console.ReadLine());
            Console.WriteLine();

            try
            {
                listeningSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp); // Создание сокета
                Task listeningTask = new Task(Listen); // Создание потока для получения сообщений
                listeningTask.Start(); // Запуск потока
                listeningTask.Wait(); // Не идем дальше пока поток не будет остановлен
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Close(); // Закрываем сокет
            }
        }

        // поток для приема подключений
        private static void Listen()
        {
            try
            {
                //Прослушиваем по адресу
                IPEndPoint localIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), localPort);
                listeningSocket.Bind(localIP);

                while (true)
                {
                    StringBuilder builder = new StringBuilder(); // получаем сообщение
                    int bytes = 0; // количество полученных байтов
                    byte[] data = new byte[1000]; // буфер для получаемых данных
                    EndPoint remoteIp = new IPEndPoint(IPAddress.Any, 0); //адрес, с которого пришли данные

                    do
                    {
                        bytes = listeningSocket.ReceiveFrom(data, ref remoteIp);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (listeningSocket.Available > 0);
                    IPEndPoint remoteFullIp = remoteIp as IPEndPoint; // получаем данные о подключении

                    Console.WriteLine("{0}:{1} - {2}", remoteFullIp.Address.ToString(), remoteFullIp.Port, builder.ToString()); // выводим сообщение

                    bool addClient = true; // Переменная для определения нового пользователя

                    for (int i = 0; i < clients.Count; i++) // Циклом перебераем всех пользователей которые отправляли сообщения на сервер
                        if (clients[i].Address.ToString() == remoteFullIp.Address.ToString()) // Если аддресс отправителя данного сообщения совпадает с аддрессом в списке
                            addClient = false; // Не добавляем клиента в историю

                    if (addClient == true) // Если этого отправителя не было обноруженно в истории
                        clients.Add(remoteFullIp); // Добавляем клиента в исторю

                    //BroadcastMessage(builder.ToString(), remoteFullIp.Address.ToString()); // Рассылаем сообщения 
                    calculate(builder.ToString(), remoteFullIp);
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

        static string converToTenWithDouble(string text)
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
                    if (forFor[z].ToString().ToUpper() == "A")
                    {
                        fff += (10 * Math.Pow(flagSystemAfter, -(z + 1)));
                        continue;
                    }
                    if (forFor[z].ToString().ToUpper() == "B")
                    {
                        fff += (11 * Math.Pow(flagSystemAfter, -(z + 1)));
                        continue;
                    }
                    if (forFor[z].ToString().ToUpper() == "C")
                    {
                        fff += (12 * Math.Pow(flagSystemAfter, -(z + 1)));
                        continue;
                    }
                    if (forFor[z].ToString().ToUpper() == "D")
                    {
                        fff += (13 * Math.Pow(flagSystemAfter, -(z + 1)));
                        continue;
                    }
                    if (forFor[z].ToString().ToUpper() == "E")
                    {
                        fff += (14 * Math.Pow(flagSystemAfter, -(z + 1)));
                        continue;
                    }
                    if (forFor[z].ToString().ToUpper() == "F")
                    {
                        fff += (15 * Math.Pow(flagSystemAfter, -(z + 1)));
                        continue;
                    }
                }
                fff += (int.Parse(forFor[z].ToString()) * Math.Pow(flagSystemAfter, -(z + 1)));
            }
            double myRes = Math.Round((temp + fff), 10);
            return myRes.ToString();
        }

        static string withDouble(string text, int ss, int after, string str)
        {
            string[] listTake = str.Split('&');
            flagSystemAfter = int.Parse(listTake[3]);
            if (listTake[3] != "10")
            {
                text = converToTenWithDouble(listTake[2]);
            }
            double text1;
            text1 = Convert.ToDouble(text);
            string zel;
            long temp;
            if (ss == 10)
            {
                return converToTenWithDouble(listTake[2]);
            }
            else
            {
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
            string r = zel + "," + drob;
            string otvet = r + "&" + cc;
            return otvet;

        }

        static string changeInSS(int ss, string str)
        {
            try
            {
                string[] listTake = str.Split('&');
                float a = float.Parse(listTake[0]);
                float behavior = float.Parse(listTake[1]);
                float b = float.Parse(listTake[2]);
                int flagSystemAfter = int.Parse(listTake[3]);
                float result = 0;
                string res;
                if (listTake[2].Split(',').Length == 2)
                {
                    res = withDouble(listTake[2], ss, flagSystemAfter, str);
                    res += "&" + ss;
                    return res;
                }
                long temp = Convert.ToInt64(listTake[2], flagSystemAfter);
                res = Convert.ToString(temp, ss);
                res += "&" + ss;
                return res;
            }
            catch (Exception ex)
            {
                return ("Введите доступные числа");
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

        private static void calculate(string take, IPEndPoint remoteFullIp)
        {
            string[] listTake = take.Split('&');
            float a = float.Parse(listTake[0]);
            float behavior = float.Parse(listTake[1]);
            float b = float.Parse(listTake[2]);
            int flagSystemAfter = int.Parse(listTake[3]);
            float result = 0;
            string forSS;
            switch (behavior)
            {
                case 1:
                    result = a + b;
                    break;

                case 2:
                    result = a - b;
                    break;
                case 3:
                    result = a * b;
                    break;
                case 4:
                    result = a / b;
                    break;
                case 5:
                    forSS = changeInSS(2, take);
                    result = float.Parse(forSS.Split("&")[0]);
                    sendMessage(result.ToString(), remoteFullIp.Address.ToString());
                    return;
                    break;
                case 6:
                    forSS = changeInSS(8, take);
                    result = float.Parse(forSS.Split("&")[0]);
                    sendMessage(result.ToString(), remoteFullIp.Address.ToString());
                    return;
                    break;
                case 7:
                    forSS = changeInSS(16, take);
                    result = float.Parse(forSS.Split("&")[0]);
                    sendMessage(result.ToString(), remoteFullIp.Address.ToString());
                    return;
                    break;
                case 8:
                    forSS = changeInSS(10, take);
                    result = float.Parse(forSS.Split("&")[0]);
                    sendMessage(result.ToString(), remoteFullIp.Address.ToString());
                    return;
                    break;


                default:
                    break;
            }
            long resultL = (long)(result);
            if (flagSystemAfter == 10)
            {
                sendMessage(result.ToString(), remoteFullIp.Address.ToString());
            }
            else
            {
                if (result.ToString().Split(',').Length == 1)
                {
                    string result1 = Convert.ToString(resultL, flagSystemAfter);
                    sendMessage(result1, remoteFullIp.Address.ToString());
                }
                else
                {
                    string result1 = withDouble(result.ToString(), flagSystemAfter);
                    sendMessage(result1, remoteFullIp.Address.ToString());
                }
            }
        }


        static string withDouble(string text, int ss)
        {
            double text1;
            text1 = Convert.ToDouble(text);
            string zel;
            long temp;
            zel = Convert.ToString(Convert.ToInt32(Math.Truncate(text1)), ss);

            int zel1 = Convert.ToInt32(Math.Truncate(text1));
            // дробную часть
            double text2;
            text2 = text1 - Math.Truncate(text1);
            int cc;
            cc = ss;
            double[] asd = new double[10];
            asd[0] = text2;

            string drob = null;

            for (int i = 1; i < 5; i++)
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
            string r = zel + "," + drob;
            return r;

        }

        // Метод для рассылки сообщений
        private static void sendMessage(string message, string ip)
        {
            byte[] data = Encoding.Unicode.GetBytes(message); // Формируем байты из текста

            for (int i = 0; i < clients.Count; i++) // Циклом перебераем всех клиентов
                if (clients[i].Address.ToString() == ip) // Если аддресс получателя не совпадает с аддрессом отправителя
                    listeningSocket.SendTo(data, clients[i]); // Отправляем сообщение
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
    }
}


