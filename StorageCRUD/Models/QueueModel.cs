namespace StorageCRUD.Models
{
    public class QueueModel
    {
        public string MessageId { get; set; }
        public string MessageContent { get; set; }
        public DateTime MessageTimestamp { get; set; }
        public QueueModel(string messageContent)
        {
            MessageContent = messageContent;
        }
    }
}
