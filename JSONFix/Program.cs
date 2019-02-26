using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace JSONFix
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var temp =
             "{\n  \"Records\": [\n    {\n      \"EventSource\": \"aws:sns\",\n      \"EventVersion\": \"1.0\",\n      \"EventSubscriptionArn\": \"arn:aws:sns:us-east-1:562471491030:ses-notifications:af7d7fbf-2903-491b-83e8-980013fee992\",\n      \"Sns\": {\n        \"Type\": \"Notification\",\n        \"MessageId\": \"7df853d9-1ae8-59b7-aa9f-0713d6365ca6\",\n        \"TopicArn\": \"arn:aws:sns:us-east-1:562471491030:ses-notifications\",\n        \"Subject\": \"Amazon SES Email Event Notification\",\n        \"Message\": \"{\\\"eventType\\\":\\\"Send\\\",\\\"mail\\\":{\\\"timestamp\\\":\\\"2019-01-29T21:50:22.301Z\\\",\\\"source\\\":\\\"andrew.test.reason@gmail.com\\\",\\\"sourceArn\\\":\\\"arn:aws:ses:us-east-1:562471491030:identity/andrew.test.reason@gmail.com\\\",\\\"sendingAccountId\\\":\\\"562471491030\\\",\\\"messageId\\\":\\\"010001689b977a5d-4b0e649d-9572-4f48-bb22-b1aa94b5b4e7-000000\\\",\\\"destination\\\":[\\\"andrew.gergis.eissa@gmail.com\\\"],\\\"headersTruncated\\\":false,\\\"headers\\\":[{\\\"name\\\":\\\"From\\\",\\\"value\\\":\\\"andrew.test.reason@gmail.com\\\"},{\\\"name\\\":\\\"To\\\",\\\"value\\\":\\\"andrew.gergis.eissa@gmail.com\\\"},{\\\"name\\\":\\\"Subject\\\",\\\"value\\\":\\\"Amazon SES test (AWS SDK for .NET)\\\"},{\\\"name\\\":\\\"MIME-Version\\\",\\\"value\\\":\\\"1.0\\\"},{\\\"name\\\":\\\"Content-Type\\\",\\\"value\\\":\\\"multipart/alternative;  boundary=\\\\\\\"----=_Part_3024076_1224405706.1548798622303\\\\\\\"\\\"}],\\\"commonHeaders\\\":{\\\"from\\\":[\\\"andrew.test.reason@gmail.com\\\"],\\\"to\\\":[\\\"andrew.gergis.eissa@gmail.com\\\"],\\\"messageId\\\":\\\"010001689b977a5d-4b0e649d-9572-4f48-bb22-b1aa94b5b4e7-000000\\\",\\\"subject\\\":\\\"Amazon SES test (AWS SDK for .NET)\\\"},\\\"tags\\\":{\\\"ses:operation\\\":[\\\"SendEmail\\\"],\\\"ses:configuration-set\\\":[\\\"TestConfigSet\\\"],\\\"ses:source-ip\\\":[\\\"197.39.122.81\\\"],\\\"ses:from-domain\\\":[\\\"gmail.com\\\"],\\\"ses:caller-identity\\\":[\\\"AndrewGergis\\\"]}},\\\"send\\\":{}}\\n\",\n        \"Timestamp\": \"2019-01-29T21:50:22.556Z\",\n        \"SignatureVersion\": \"1\",\n        \"Signature\": \"M48/kMcdoBoN0ksyptL7jciC7AC1eENsjrqntuCC5QFsxvkNZGVhD2V58tnLRM44MB4mOoiXys4g9jnJgb8/qwhyQqE+veAR/7xtWQiox11Teqf/Q9MBrUan0vvETUtrcU00yUIDUX3bs5B7B2Wvc3UJypdPm9mNEHtzYOfUB6raFTKidu4cKhTzBN78d8LBShVEcRBbBWAKa6GZWiMA9WKcvRHzLxm7hdrRSgns+Bap+B5paIKW/qLsyDiRthDTTSJlBCTxZrbHamzlej9GOYrFmLhqZtN5vvwuPC5QeRORKGxwNyzU1tv+Ygwm0XM6JVxLOIajron3z6Fmk9R6Ag==\",\n        \"SigningCertUrl\": \"https://sns.us-east-1.amazonaws.com/SimpleNotificationService-ac565b8b1a6c5d002d285f9598aa1d9b.pem\",\n        \"UnsubscribeUrl\": \"https://sns.us-east-1.amazonaws.com/?Action=Unsubscribe&SubscriptionArn=arn:aws:sns:us-east-1:562471491030:ses-notifications:af7d7fbf-2903-491b-83e8-980013fee992\",\n        \"MessageAttributes\": {}\n      }\n    }\n  ]\n}";


            //            var t= "{
            //  "Type" : "Notification",
            //  "MessageId" : "eef338d6-5ff2-5399-8844-6c1d3228bc28",
            //  "TopicArn" : "arn:aws:sns:us-east-1:562471491030:ses-notifications",
            //  "Subject" : "Amazon SES Email Event Notification",
            //  "Message" : "{\"eventType\":\"Send\",\"mail\":{\"timestamp\":\"2019-01-30T09:17:14.664Z\",\"source\":\"andrew.test.reason@gmail.com\",\"sourceArn\":\"arn:aws:ses:us-east-1:562471491030:identity/andrew.test.reason@gmail.com\",\"sendingAccountId\":\"562471491030\",\"messageId\":\"010001689e0c5428-8aedf91d-75f6-4a21-9158-e31a8ceccca3-000000\",\"destination\":[\"andrew.gergis.eissa@gmail.com\"],\"headersTruncated\":false,\"headers\":[{\"name\":\"From\",\"value\":\"andrew.test.reason@gmail.com\"},{\"name\":\"To\",\"value\":\"andrew.gergis.eissa@gmail.com\"},{\"name\":\"Subject\",\"value\":\"Amazon SES test (AWS SDK for .NET)\"},{\"name\":\"MIME-Version\",\"value\":\"1.0\"},{\"name\":\"Content-Type\",\"value\":\"multipart/alternative;  boundary=\\\"----=_Part_3213698_873263962.1548839834666\\\"\"}],\"commonHeaders\":{\"from\":[\"andrew.test.reason@gmail.com\"],\"to\":[\"andrew.gergis.eissa@gmail.com\"],\"messageId\":\"010001689e0c5428-8aedf91d-75f6-4a21-9158-e31a8ceccca3-000000\",\"subject\":\"Amazon SES test (AWS SDK for .NET)\"},\"tags\":{\"ses:operation\":[\"SendEmail\"],\"ses:configuration-set\":[\"TestConfigSet\"],\"ses:source-ip\":[\"197.39.122.81\"],\"ses:from-domain\":[\"gmail.com\"],\"ses:caller-identity\":[\"AndrewGergis\"]}},\"send\":{}}\n",
            //  "Timestamp" : "2019-01-30T09:17:14.828Z",
            //  "SignatureVersion" : "1",
            //  "Signature" : "oKze4mj4pyQK+EaFefEg9v1ru5KoItE8FFziMcKAsFFaHkFvMiyD1rB92rZ+Rk8Iq9BBPY3ejYP380OhEpts1DmlhbUPZ8w2oi0tRYF11AM9gBZ9H/n6tejiBFF59D8QcSLqDLXIlsep9GY3V0fqmYRbUsF8oV/CHRUlBKQgqk+mImvMUWpXE8iXwpHwE5MpNMJ84OrP2zntJnryhwXZn7TpAeDbWcO5jMJpACZ6j5tFqhWSzwwNnmpz4isonr6mPaOV/uf4CysCo0ag4xVjfdvzsr5fRHsFpOS7Hj/U+F6ZxmXVGJ3rIHHMAwgAHYG0Sbzlp0L43QAOQtyuTc0EcQ==",


            //  "SigningCertURL" : "https://sns.us-east-1.amazonaws.com/SimpleNotificationService-ac565b8b1a6c5d002d285f9598aa1d9b.pem",
            //  "UnsubscribeURL" : "https://sns.us-east-1.amazonaws.com/?Action=Unsubscribe&SubscriptionArn=arn:aws:sns:us-east-1:562471491030:ses-notifications:9ca9e743-4906-40a8-a145-0e59fb2e0576"
            //}";
            //string tt = JsonConvert.DeserializeObject<string>(temp);

            var str =
                "{\"eventType\":\"Send\",\"mail\":{\"timestamp\":\"2019-01-30T09:17:14.664Z\",\"source\":\"andrew.test.reason@gmail.com\",\"sourceArn\":\"arn:aws:ses:us-east-1:562471491030:identity/andrew.test.reason@gmail.com\",\"sendingAccountId\":\"562471491030\",\"messageId\":\"010001689e0c5428-8aedf91d-75f6-4a21-9158-e31a8ceccca3-000000\",\"destination\":[\"andrew.gergis.eissa@gmail.com\"],\"headersTruncated\":false,\"headers\":[{\"name\":\"From\",\"value\":\"andrew.test.reason@gmail.com\"},{\"name\":\"To\",\"value\":\"andrew.gergis.eissa@gmail.com\"},{\"name\":\"Subject\",\"value\":\"Amazon SES test (AWS SDK for .NET)\"},{\"name\":\"MIME-Version\",\"value\":\"1.0\"},{\"name\":\"Content-Type\",\"value\":\"multipart/alternative;  boundary=\\\"----=_Part_3213698_873263962.1548839834666\\\"\"}],\"commonHeaders\":{\"from\":[\"andrew.test.reason@gmail.com\"],\"to\":[\"andrew.gergis.eissa@gmail.com\"],\"messageId\":\"010001689e0c5428-8aedf91d-75f6-4a21-9158-e31a8ceccca3-000000\",\"subject\":\"Amazon SES test (AWS SDK for .NET)\"},\"tags\":{\"ses:operation\":[\"SendEmail\"],\"ses:configuration-set\":[\"TestConfigSet\"],\"ses:source-ip\":[\"197.39.122.81\"],\"ses:from-domain\":[\"gmail.com\"],\"ses:caller-identity\":[\"AndrewGergis\"]}},\"send\":{}}\n";

            var str2 =
                "{\"Records\":[{\"EventSource\":\"aws:sns\",\"EventVersion\":\"1.0\",\"EventSubscriptionArn\":\"arn:aws:sns:us-east-1:562471491030:ses-notifications:af7d7fbf-2903-491b-83e8-980013fee992\",\"Sns\":{\"Type\":\"Notification\",\"MessageId\":\"615044d6-8e0c-5370-83d3-101dfa52a7bb\",\"TopicArn\":\"arn:aws:sns:us-east-1:562471491030:ses-notifications\",\"Subject\":\"Amazon SES Email Event Notification\",\"Message\":\"{\\\"eventType\\\":\\\"Send\\\",\\\"mail\\\":{\\\"timestamp\\\":\\\"2019-01-30T09:45:02.404Z\\\",\\\"source\\\":\\\"andrew.test.reason@gmail.com\\\",\\\"sourceArn\\\":\\\"arn:aws:ses:us-east-1:562471491030:identity/andrew.test.reason@gmail.com\\\",\\\"sendingAccountId\\\":\\\"562471491030\\\",\\\"messageId\\\":\\\"010001689e25c6c4-17511636-a61f-4b47-9735-e7534171391f-000000\\\",\\\"destination\\\":[\\\"andrew.gergis.eissa@gmail.com\\\"],\\\"headersTruncated\\\":false,\\\"headers\\\":[{\\\"name\\\":\\\"From\\\",\\\"value\\\":\\\"andrew.test.reason@gmail.com\\\"},{\\\"name\\\":\\\"To\\\",\\\"value\\\":\\\"andrew.gergis.eissa@gmail.com\\\"},{\\\"name\\\":\\\"Subject\\\",\\\"value\\\":\\\"Amazon SES test (AWS SDK for .NET)\\\"},{\\\"name\\\":\\\"MIME-Version\\\",\\\"value\\\":\\\"1.0\\\"},{\\\"name\\\":\\\"Content-Type\\\",\\\"value\\\":\\\"multipart/alternative;  boundary=\\\\\\\"----=_Part_3242901_1464927575.1548841502406\\\\\\\"\\\"}],\\\"commonHeaders\\\":{\\\"from\\\":[\\\"andrew.test.reason@gmail.com\\\"],\\\"to\\\":[\\\"andrew.gergis.eissa@gmail.com\\\"],\\\"messageId\\\":\\\"010001689e25c6c4-17511636-a61f-4b47-9735-e7534171391f-000000\\\",\\\"subject\\\":\\\"Amazon SES test (AWS SDK for .NET)\\\"},\\\"tags\\\":{\\\"ses:operation\\\":[\\\"SendEmail\\\"],\\\"ses:configuration-set\\\":[\\\"TestConfigSet\\\"],\\\"ses:source-ip\\\":[\\\"197.39.122.81\\\"],\\\"ses:from-domain\\\":[\\\"gmail.com\\\"],\\\"ses:caller-identity\\\":[\\\"AndrewGergis\\\"]}},\\\"send\\\":{}}\\n\",\"Timestamp\":\"2019-01-30T09:45:02.646Z\",\"SignatureVersion\":\"1\",\"Signature\":\"I3apEoGTxVKe6kGMAbztnA6PhuEdvU6BTL3x+0xwAvFksHNb/b+Vjk9ImbcB7qXsZgn8GlU60vzYY/koRXTznWu91lcShHRkNMIyVqX0saD3WpxQ3m05dmR6ITYYTlXe2Ha8zrT2aKmMNRu5pXwc37YdZ1HUITcmSyr3GcW2ZdmdNo9tyTmHswzbwgVUEpKwsJ+PguvLyAAtjr11dfLpNiSTv/DVdXtFh1bvMzCXHFnEafCx/Ii9ADG7wpU+Ljqbsd5yBw3uYE0RR0SimRzmVDBYQEBt2VE0a+mxcM63IPuJQ231wtEADNIVaZVo7J9eboIoTH0qb9CrYnDWGkVGGg==\",\"SigningCertUrl\":\"https://sns.us-east-1.amazonaws.com/SimpleNotificationService-ac565b8b1a6c5d002d285f9598aa1d9b.pem\",\"UnsubscribeUrl\":\"https://sns.us-east-1.amazonaws.com/?Action=Unsubscribe&SubscriptionArn=arn:aws:sns:us-east-1:562471491030:ses-notifications:af7d7fbf-2903-491b-83e8-980013fee992\",\"MessageAttributes\":{}}}]}";
            

            
            //var tt = Regex.Unescape(temp);
            //var tt2 = Regex.Unescape(tt);
            //var tt3 = Regex.Unescape(str2);

            var tt4 = JObject.Parse(str2);
            //var ss = tt4.ToString();
            //object val = JsonConvert.DeserializeObject(ss);
            dynamic dynVal;
            dynVal = Convert.ChangeType(tt4, tt4.GetType());
            string x = dynVal.Records[0].Sns.Message;


            var o = JObject.Parse(x);
            //var ss2 = o.ToString();
            //object val2 = JsonConvert.DeserializeObject(ss2);
            dynamic dynVal2;
            dynVal2 = Convert.ChangeType(o, o.GetType());



            switch (dynVal2.eventType.ToString())
            {
                case "Bounce":
                    handleBounce(dynVal2);
                    break;
                case "Complaint":
                    handleComplaint(dynVal2);
                    break;
                case "Send":
                    handleDelivery(dynVal2);
                    break;
                default:
                    Console.WriteLine("Unknown notification type: " + dynVal2.notificationType);
                    break;
            }


            //foreach (var item in dynVal2.mail.headers)
            //{
            //    Console.WriteLine($"{item.name} : {item.value}");
            //}


            //Console.WriteLine(dynVal2.eventType);
            //Console.WriteLine(o);
            //Console.WriteLine(tt4);

            Console.ReadLine();
        }

        private static void handleBounce(dynamic dynVal2)
        {
            throw new NotImplementedException();
        }

        private static void handleComplaint(dynamic dynVal2)
        {
            throw new NotImplementedException();
        }

        static void handleDelivery(dynamic message)
        {
            var tableName = "mailing";

            var messageId = message.mail.messageId;
            //var deliveryTimestamp = message.delivery.timestamp;
            var addresses = message.mail.destination;

            foreach (var address in addresses)
            {
                writeDDB(address.ToString(), message, tableName, "disable");
            }
        }
        static void writeDDB(string id, dynamic message, string tableName, string status)
        {

            var _dynamoDbClient = new AmazonDynamoDBClient();

            //dynamic dynVal;
            //dynVal = Convert.ChangeType(val, val.GetType());

            var request = new PutItemRequest()
            {
                TableName = tableName,
                Item = new Dictionary<string, AttributeValue>()
                {
                    {
                        "UserId", new AttributeValue()
                        {
                            S = id,
                        }
                    },
                    {
                        "EventType", new AttributeValue()
                        {
                            S = message.eventType
                        }
                    },
                    {
                        "From", new AttributeValue()
                        {
                            S = message.mail.source
                        }
                    },
                    {
                        "Timestamp", new AttributeValue()
                        {
                            S = message.mail.timestamp
                        }
                    },
                    {
                        "State", new AttributeValue()
                        {
                            S = status
                        }
                    }
                }
            };
            var response = _dynamoDbClient.PutItemAsync(request).GetAwaiter().GetResult();
        }
    }
}
