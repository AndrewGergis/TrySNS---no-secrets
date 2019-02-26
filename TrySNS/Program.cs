using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using System;

namespace TrySNS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var aws_access_key_id = "//secret removed";
            var aws_secret_access_key = "//secret removed";
            //create a new SNS client and set endpoint
            AmazonSimpleNotificationServiceClient snsClient =
                new AmazonSimpleNotificationServiceClient(aws_access_key_id,aws_secret_access_key);


            //delete an SNS topic
            var topicArn = "arn:aws:sns:us-east-1:562471491030:MyNewTopic";
            DeleteTopicRequest deleteTopicRequest = new DeleteTopicRequest(topicArn);
            DeleteTopicResponse deleteTopicResponse =
                snsClient.DeleteTopicAsync(deleteTopicRequest).GetAwaiter().GetResult();
            //get request id for DeleteTopicRequest from SNS metadata
            Console.WriteLine("DeleteTopicRequest - " + deleteTopicResponse.ResponseMetadata.RequestId);


            ////publish to an SNS topic
            //var topicArn = "arn:aws:sns:us-east-1:562471491030:MyNewTopic";
            //String msg = "My text published to SNS topic with email endpoint";
            //PublishRequest publishRequest = new PublishRequest(topicArn, msg);
            //PublishResponse publishResult = snsClient.PublishAsync(publishRequest).GetAwaiter().GetResult();
            ////print MessageId of message published to SNS topic
            //Console.WriteLine("MessageId - " + publishResult.MessageId);


            ////subscribe to an SNS topic
            //var topicArn = "arn:aws:sns:us-east-1:562471491030:MyNewTopic";
            //SubscribeRequest subRequest = new SubscribeRequest(topicArn, "email", "andrew.gergis.edx@gmail.com");
            //SubscribeResponse subResponse = snsClient.SubscribeAsync(subRequest).GetAwaiter().GetResult();
            ////get request id for SubscribeRequest from SNS metadata
            //Console.WriteLine("SubscribeRequest - " + subResponse.ResponseMetadata.RequestId);
            //Console.WriteLine("Check your email and confirm subscription.");

            ////create a new SNS topic
            //CreateTopicRequest createTopicRequest = new CreateTopicRequest("MyNewTopic");
            //CreateTopicResponse createTopicResponse =
            //    snsClient.CreateTopicAsync(createTopicRequest).GetAwaiter().GetResult();
            ////print TopicArn
            //Console.WriteLine(createTopicResponse.TopicArn);
            ////get request id for CreateTopicRequest from SNS metadata		
            //Console.WriteLine("CreateTopicRequest - " + createTopicResponse.ResponseMetadata.RequestId);

            Console.ReadLine();
        }
    }
}
