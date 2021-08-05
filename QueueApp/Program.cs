using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Threading.Tasks;

namespace QueueApp
{
    //Storage Account - Adding Messages
    class Program
    {
        private const string ConnectionString = "DefaultEndpointsProtocol=https;EndpointSuffix=core.windows.net;AccountName=azstorageanandn;AccountKey=+YGBc+yWLhsBK4svxd+R88Le18FfQSLcn4Y1c7Iy1X/GFTeaS+97PyR3frskXUV+3ys2rzx4EloQ5esxA1etNQ==";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            SendArticleAsync("New Message Added 2").Wait();
            Console.WriteLine($"Sent");
        }

        static async Task SendArticleAsync(string newsMessage)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);
            CloudQueueClient cloudQueueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue cloudQueue = cloudQueueClient.GetQueueReference("messagequeue");

            bool createdQueue = await cloudQueue.CreateIfNotExistsAsync();
            if (createdQueue)
            {
                Console.WriteLine("Queue Created");
            }
            CloudQueueMessage queueMessage = new CloudQueueMessage(newsMessage);
            await cloudQueue.AddMessageAsync(queueMessage);
            Console.WriteLine("Message sent");
        }
    }
}
