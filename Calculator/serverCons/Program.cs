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

        static void Main(string[] args)
        {
            Console.WriteLine("UDP CHAT SERVER VERSION 3");
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
                    byte[] data = new byte[256]; // буфер для получаемых данных
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

        private static void calculate(string take, IPEndPoint remoteFullIp)
        {
            string[] listTake = take.Split('&');
            float a = float.Parse(listTake[0]);
            float behavior = float.Parse(listTake[1]);
            float b = float.Parse(listTake[2]);
            int flagSystemAfter = int.Parse(listTake[3]);
            float result = 0;
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

                default:
                    break;
            }
            long resultL = (long)(result);
            if (flagSystemAfter == 10)
            {
                BroadcastMessage(result.ToString(), remoteFullIp.Address.ToString());
            }
            else
            {
                if (result.ToString().Split(',').Length==1)
                {
                    string result1 = Convert.ToString(resultL, flagSystemAfter);
                    BroadcastMessage(result1, remoteFullIp.Address.ToString());
                }
                else
                {
                    string result1 = withDouble(result.ToString(), flagSystemAfter);
                    BroadcastMessage(result1, remoteFullIp.Address.ToString());
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
        private static void BroadcastMessage(string message, string ip)
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


