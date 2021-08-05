using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading.Tasks;

namespace PrivateMessageSender
{
    class Program
    {
        const string ServiceBusConnectionString = "Endpoint=sb://learnservicebusanandn.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=X5rqsFRdelysYRUZpwyu3VOFwk+4oMRAu75vSSt5krU=";

        const string QueueName = "messagesQueue";

        static IQueueClient queueClient;

        static void Main(string[] args)
        {
            Console.WriteLine("Sending a message to the Sales Messages queue...");

            SendSalesMessageAsync().GetAwaiter().GetResult();

            Console.WriteLine("Message was sent successfully.");
        }

        static async Task SendSalesMessageAsync()
        {
            //Create Queue Client
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

            // Send messages.
            try
            {
                // Send the Message here
                string messageBody = $"$10,000 order for bicycle parts from retailer Adventure Works.";
                var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                Console.WriteLine($"Sending message: {messageBody}");

                await queueClient.SendAsync(message);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }

            // Close the connection to the queue here
            await queueClient.CloseAsync();
        }
    }
}
