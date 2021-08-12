using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Threading.Tasks;

namespace DeQueueApp
{
    //Storage Account - Receiving Messages
    class Program
    {
        private const string ConnectionString = "DefaultEndpointsProtocol=https;EndpointSuffix=core.windows.net;AccountName=azstorageanandn;AccountKey=+YGBc+yWLhsBK4svxd+R88Le18FfQSLcn4Y1c7Iy1X/GFTeaS+97PyR3frskXUV+3ys2rzx4EloQ5esxA1etNQ==";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ReceiveArticleAsync().ConfigureAwait(false);
            Console.WriteLine($"Received");
        }

        static async Task<string> ReceiveArticleAsync()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);
            CloudQueueClient cloudQueueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue cloudQueue = cloudQueueClient.GetQueueReference("messagequeue");

            //bool exists = await cloudQueue.ExistsAsync();
            //if (exists)
            //{
                CloudQueueMessage retrievedArticle = await cloudQueue.GetMessageAsync();
                if (retrievedArticle != null)
                {
                    string newMessage = retrievedArticle.AsString;
                    await cloudQueue.DeleteMessageAsync(retrievedArticle);
                    return newMessage;
                }
            //}
            return "Queue Empty or not created";
        } 
    }
}
