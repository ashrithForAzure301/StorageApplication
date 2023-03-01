using StorageCRUD.Models;
using StorageCRUD.StorageInterface;
using Azure.Storage.Queues;
using Newtonsoft.Json;

namespace StorageCRUD.Repositories
{
    public class QueueRepository : QueueInterface
    {
        private readonly QueueClient _queueClient;

        public QueueRepository(IConfiguration configuration)
        {
            string connectionString = configuration["Storage:ConnectionString"];
            string queueName = configuration.GetValue<string>("Storage:QueueName");
            _queueClient = new QueueClient(connectionString, queueName);
        }

        public async Task AddMessageAsync(QueueModel message)
        {
            string messageBody = JsonConvert.SerializeObject(message);
            await _queueClient.SendMessageAsync(messageBody);
        }

        public async Task<QueueModel> DequeueMessageAsync()
        {
            QueueModel message = null;
            var receivedMessage = await _queueClient.ReceiveMessageAsync();

            if (receivedMessage != null)
            {
                message = JsonConvert.DeserializeObject<QueueModel>(receivedMessage.Value.MessageText);
                await _queueClient.DeleteMessageAsync(receivedMessage.Value.MessageId, receivedMessage.Value.PopReceipt);
            }

            return message;
        }

        public async Task UpdateMessageAsync(QueueModel message)
        {
            var receivedMessage = await _queueClient.ReceiveMessageAsync();
            if (receivedMessage?.Value != null)
            {
                var updatedMessage = JsonConvert.DeserializeObject<QueueModel>(receivedMessage.Value.MessageText);
                updatedMessage.MessageId = message.MessageId;
                updatedMessage.MessageContent = message.MessageContent;
                var messageBody = JsonConvert.SerializeObject(updatedMessage);
                await _queueClient.UpdateMessageAsync(receivedMessage.Value.MessageId, receivedMessage.Value.PopReceipt, messageBody, TimeSpan.Zero);
            }
        }


        public async Task ClearQueueAsync()
        {
            await _queueClient.ClearMessagesAsync();
        }

    }
}
