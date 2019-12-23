using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DuplicateDetectionSB
{
    class Program
    {       
        static void Main(string[] args)
        {
            string queueName = "yourqueueName";
            string ServiceBusQueueConnectionString = "yourservicebusconnectionstring";
            QueueClient queueClient = new QueueClient(ServiceBusQueueConnectionString, queueName, ReceiveMode.PeekLock, RetryPolicy.Default);
            Console.WriteLine("Queue is configured for duplicate detection with window of 5mins. i.e ");
            Console.WriteLine("This app will push 10 messages with 30 seconds interval & at the end we will messageCount in queue!!!");
                for (int i = 0; i < 10; i++)
                {
                    string testMessage = "this is test message for duplication";
                    Message sbMessage = new Message(Encoding.UTF8.GetBytes(testMessage));
                    sbMessage.MessageId = "testMessageforDuplicacy";        //MessageId is same for all properties
                    queueClient.SendAsync(sbMessage).Wait();
                    Thread.Sleep(30);
                    Console.WriteLine("sending message .........." + (i + 1));
                }
                var count =  GetMessageCountFromQueue(queueName, ServiceBusQueueConnectionString).Result;
                Console.WriteLine("active message count in queue :" );
                Console.WriteLine(count);
                Console.ReadLine();           
        }

        public static async Task<long> GetMessageCountFromQueue(string queueName,string ServiceBusQueueConnectionString)
        {
            var managementClient = new ManagementClient(ServiceBusQueueConnectionString);
            var queue =  await managementClient.GetQueueRuntimeInfoAsync(queueName);
            var messageCount = queue.MessageCount;
            return messageCount;
        }
    }
}

