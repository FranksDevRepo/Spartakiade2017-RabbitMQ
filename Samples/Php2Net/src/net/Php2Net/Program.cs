using System;
using EasyNetQ;

namespace FP.Spartakiade2017.PicFlow.Php2Net
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IBus myBus = null;
            try
            {
                myBus = RabbitHutch.CreateBus("host=localhost");
                for (int i = 0; i < 100; i++)
                {
                    var tc = new DtoTest {Text = $"Testnachricht {DateTime.Now}"};
                    myBus.Publish(tc);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                myBus?.Dispose();
            }
            Console.ReadLine();
        }

        
    }
}
