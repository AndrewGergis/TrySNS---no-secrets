namespace SESLambda
{
    public class Record
    {
        public Sns Sns { get; set; }
    }

    public class Sns
    {
        public Message Message { get; set; }
    }

    public class Message
    {
        public string eventType { get; set; }
    }
}