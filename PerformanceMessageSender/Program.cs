using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceMessageSender
{
    class Program
    {
        const string ServiceBusConnectionString = "Endpoint=sb://learnservicebusanandn.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=X5rqsFRdelysYRUZpwyu3VOFwk+4oMRAu75vSSt5krU=";

        const string TopicName = "messagestopic";

        static ITopicClient topicClient;

        static void Main(string[] args)
        {
            Console.WriteLine("Sending a message to the Sales Performance Topic...");

            SendPerformanceMessageAsync().GetAwaiter().GetResult();

            Console.WriteLine("Message was sent successfully.");
        }

        static async Task SendPerformanceMessageAsync()
        {
            //Create Topic Client
            topicClient = new TopicClient(ServiceBusConnectionString, TopicName);

            // Send messages.
            try
            {
                // Send the Message here
                for (int i = 0; i < 100; i++)
                {
                    string messageBody = $"Total sales for Brazil in August: $" + i + "m.";
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                    Console.WriteLine($"Sending message: {messageBody}");

                    await topicClient.SendAsync(message);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }

            // Close the connection to the queue here
            await topicClient.CloseAsync();
        }
    }
}
