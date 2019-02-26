using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.Core;
using Amazon.Runtime;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace SESLambda
{
    public class Function
    {

        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string FunctionHandler(object notificationJSON)
        {
            string JSONVal = JsonConvert.SerializeObject(notificationJSON);

            var cleanNotificationJSON = JObject.Parse(JSONVal);
            //var strNotifyJSON = cleanNotificationJSON.ToString();
            //object serializedNotifyObject = JsonConvert.DeserializeObject(strNotifyJSON);
            dynamic dynVal;
            dynVal = Convert.ChangeType(cleanNotificationJSON, cleanNotificationJSON.GetType());
            string message = dynVal.Records[0].Sns.Message;


            var cleanMessage = JObject.Parse(message);
            //var ss2 = o.ToString();
            //object val2 = JsonConvert.DeserializeObject(ss2);
            dynamic dynMessage;
            dynMessage = Convert.ChangeType(cleanMessage, cleanMessage.GetType());

            switch (dynMessage.eventType.ToString())
            {
                case "Bounce":
                    handleBounce(dynMessage);
                    break;
                case "Complaint":
                    handleComplaint(dynMessage);
                    break;
                case "Delivery":
                    handleDelivery(dynMessage);
                    break;
                default:
                    Console.WriteLine("Unknown notification type: " + dynMessage.notificationType);
                    break;
            }

            #region Not needed
            ////var cleanVal = JObject.Parse(JSONVal);
            ////cleanVal.ReplaceAll(");
            ////JSONVal = JSONVal.Replace(@"\", "");
            ////JSONVal = JSONVal.Replace("\"{", "{");
            ////JSONVal = JSONVal.Replace("}n\"", "}");
            ////JSONVal = JSONVal.Replace("\"\"", "\"");
            ////JSONVal = JSONVal.Replace("=\"", "");
            ////object val = JsonConvert.DeserializeObject(JSONVal);

            //var temp =
            //    "{\n  \"Records\": [\n    {\n      \"EventSource\": \"aws:sns\",\n      \"EventVersion\": \"1.0\",\n      \"EventSubscriptionArn\": \"arn:aws:sns:us-east-1:562471491030:ses-notifications:af7d7fbf-2903-491b-83e8-980013fee992\",\n      \"Sns\": {\n        \"Type\": \"Notification\",\n        \"MessageId\": \"7df853d9-1ae8-59b7-aa9f-0713d6365ca6\",\n        \"TopicArn\": \"arn:aws:sns:us-east-1:562471491030:ses-notifications\",\n        \"Subject\": \"Amazon SES Email Event Notification\",\n        \"Message\": \"{\\\"eventType\\\":\\\"Send\\\",\\\"mail\\\":{\\\"timestamp\\\":\\\"2019-01-29T21:50:22.301Z\\\",\\\"source\\\":\\\"andrew.test.reason@gmail.com\\\",\\\"sourceArn\\\":\\\"arn:aws:ses:us-east-1:562471491030:identity/andrew.test.reason@gmail.com\\\",\\\"sendingAccountId\\\":\\\"562471491030\\\",\\\"messageId\\\":\\\"010001689b977a5d-4b0e649d-9572-4f48-bb22-b1aa94b5b4e7-000000\\\",\\\"destination\\\":[\\\"andrew.gergis.eissa@gmail.com\\\"],\\\"headersTruncated\\\":false,\\\"headers\\\":[{\\\"name\\\":\\\"From\\\",\\\"value\\\":\\\"andrew.test.reason@gmail.com\\\"},{\\\"name\\\":\\\"To\\\",\\\"value\\\":\\\"andrew.gergis.eissa@gmail.com\\\"},{\\\"name\\\":\\\"Subject\\\",\\\"value\\\":\\\"Amazon SES test (AWS SDK for .NET)\\\"},{\\\"name\\\":\\\"MIME-Version\\\",\\\"value\\\":\\\"1.0\\\"},{\\\"name\\\":\\\"Content-Type\\\",\\\"value\\\":\\\"multipart/alternative;  boundary=\\\\\\\"----=_Part_3024076_1224405706.1548798622303\\\\\\\"\\\"}],\\\"commonHeaders\\\":{\\\"from\\\":[\\\"andrew.test.reason@gmail.com\\\"],\\\"to\\\":[\\\"andrew.gergis.eissa@gmail.com\\\"],\\\"messageId\\\":\\\"010001689b977a5d-4b0e649d-9572-4f48-bb22-b1aa94b5b4e7-000000\\\",\\\"subject\\\":\\\"Amazon SES test (AWS SDK for .NET)\\\"},\\\"tags\\\":{\\\"ses:operation\\\":[\\\"SendEmail\\\"],\\\"ses:configuration-set\\\":[\\\"TestConfigSet\\\"],\\\"ses:source-ip\\\":[\\\"197.39.122.81\\\"],\\\"ses:from-domain\\\":[\\\"gmail.com\\\"],\\\"ses:caller-identity\\\":[\\\"AndrewGergis\\\"]}},\\\"send\\\":{}}\\n\",\n        \"Timestamp\": \"2019-01-29T21:50:22.556Z\",\n        \"SignatureVersion\": \"1\",\n        \"Signature\": \"M48/kMcdoBoN0ksyptL7jciC7AC1eENsjrqntuCC5QFsxvkNZGVhD2V58tnLRM44MB4mOoiXys4g9jnJgb8/qwhyQqE+veAR/7xtWQiox11Teqf/Q9MBrUan0vvETUtrcU00yUIDUX3bs5B7B2Wvc3UJypdPm9mNEHtzYOfUB6raFTKidu4cKhTzBN78d8LBShVEcRBbBWAKa6GZWiMA9WKcvRHzLxm7hdrRSgns+Bap+B5paIKW/qLsyDiRthDTTSJlBCTxZrbHamzlej9GOYrFmLhqZtN5vvwuPC5QeRORKGxwNyzU1tv+Ygwm0XM6JVxLOIajron3z6Fmk9R6Ag==\",\n        \"SigningCertUrl\": \"https://sns.us-east-1.amazonaws.com/SimpleNotificationService-ac565b8b1a6c5d002d285f9598aa1d9b.pem\",\n        \"UnsubscribeUrl\": \"https://sns.us-east-1.amazonaws.com/?Action=Unsubscribe&SubscriptionArn=arn:aws:sns:us-east-1:562471491030:ses-notifications:af7d7fbf-2903-491b-83e8-980013fee992\",\n        \"MessageAttributes\": {}\n      }\n    }\n  ]\n}";

            //var str =
            //    "{\"eventType\":\"Send\",\"mail\":{\"timestamp\":\"2019-01-30T09:17:14.664Z\",\"source\":\"andrew.test.reason@gmail.com\",\"sourceArn\":\"arn:aws:ses:us-east-1:562471491030:identity/andrew.test.reason@gmail.com\",\"sendingAccountId\":\"562471491030\",\"messageId\":\"010001689e0c5428-8aedf91d-75f6-4a21-9158-e31a8ceccca3-000000\",\"destination\":[\"andrew.gergis.eissa@gmail.com\"],\"headersTruncated\":false,\"headers\":[{\"name\":\"From\",\"value\":\"andrew.test.reason@gmail.com\"},{\"name\":\"To\",\"value\":\"andrew.gergis.eissa@gmail.com\"},{\"name\":\"Subject\",\"value\":\"Amazon SES test (AWS SDK for .NET)\"},{\"name\":\"MIME-Version\",\"value\":\"1.0\"},{\"name\":\"Content-Type\",\"value\":\"multipart/alternative;  boundary=\\\"----=_Part_3213698_873263962.1548839834666\\\"\"}],\"commonHeaders\":{\"from\":[\"andrew.test.reason@gmail.com\"],\"to\":[\"andrew.gergis.eissa@gmail.com\"],\"messageId\":\"010001689e0c5428-8aedf91d-75f6-4a21-9158-e31a8ceccca3-000000\",\"subject\":\"Amazon SES test (AWS SDK for .NET)\"},\"tags\":{\"ses:operation\":[\"SendEmail\"],\"ses:configuration-set\":[\"TestConfigSet\"],\"ses:source-ip\":[\"197.39.122.81\"],\"ses:from-domain\":[\"gmail.com\"],\"ses:caller-identity\":[\"AndrewGergis\"]}},\"send\":{}}\n";

            //string tt = Regex.Unescape(str);
            ////var str2 = JsonConvert.ToString(temp, '"', StringEscapeHandling.Default);

            ////temp.Decode("json", "utf-8");
            ////StringBuilder builder=new StringBuilder();
            ////var obj = JsonConvert.DeserializeObject<string>(temp);
            ////string str = JsonConvert.DeserializeObject<string>(obj); 
            #endregion

            return HttpStatusCode.OK.ToString();
        }

        private void handleDelivery(dynamic message)
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

        private void handleComplaint(dynamic message)
        {
            var tableName = "mailing";
            string messageId = message.mail.messageId;

            var addresses = message.complaint.complainedRecipients;
            foreach (var address in addresses)
            {
                writeDDB(address.ToString(), message, tableName, "disable");
            }
        }

        private void handleBounce(dynamic message)
        {
            var tableName = "mailing";
            string messageId = message.mail.messageId;
            var addresses = message.bounce.bouncedRecipients;

            //var addressesJSON = JObject.Parse(addresses);
            //dynamic addressesArr;
            //addressesArr = Convert.ChangeType(addressesJSON, addressesJSON.GetType());

            string bounceType = message.bounce.bounceType;

            foreach (var address in addresses)
            {
                writeDDB(address.ToString(), message, tableName, "disable");
            }
        }

        private void writeDDB(string id, dynamic message, string tableName, string status)
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
