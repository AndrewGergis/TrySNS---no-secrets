using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace DynamoDb.Libs
{
    public class DynamoDbExample : IDynamoDbExample
    {
        private readonly IAmazonDynamoDB _dynamoDbClient;

        public DynamoDbExample(IAmazonDynamoDB dynamoDbClient)
        {
            _dynamoDbClient = dynamoDbClient;
        }

        public HttpStatusCode AddNewItem(int id, string replyDateTime, double price)
        {
            var request = new PutItemRequest()
            {
                TableName = "DynamoDbExample",
                Item = new Dictionary<string, AttributeValue>()
                {
                    {
                        "Id", new AttributeValue()
                        {
                            N = id.ToString(),
                        }
                    },
                    {
                        "ReplyDateTime", new AttributeValue()
                        {
                            N = replyDateTime
                        }
                    },
                    {
                        "Price",new AttributeValue()
                        {
                            N = price.ToString()
                        }
                    }
                }
            };
            var response = _dynamoDbClient.PutItemAsync(request).GetAwaiter().GetResult();
            return response.HttpStatusCode;
        }
        public HttpStatusCode CreateTable()
        {
            var request = new CreateTableRequest
            {
                AttributeDefinitions = new List<AttributeDefinition>()
                {
                    new AttributeDefinition()
                    {
                        AttributeName = "Id",
                        AttributeType = "N"
                    },
                    new AttributeDefinition()
                    {
                        AttributeName = "ReplyDateTime",
                        AttributeType = "N"
                    }
                },
                KeySchema = new List<KeySchemaElement>()
                {
                    new KeySchemaElement()
                    {
                        AttributeName = "Id",
                        KeyType = "HASH"
                    },
                    new KeySchemaElement()
                    {
                        AttributeName = "ReplyDateTime",
                        KeyType = "RANGE"
                    }
                },
                ProvisionedThroughput = new ProvisionedThroughput()
                {
                    ReadCapacityUnits = 5,
                    WriteCapacityUnits = 5
                },
                TableName = "DynamoDbExample"
            };

            var response = _dynamoDbClient.CreateTableAsync(request).GetAwaiter().GetResult();
            return response.HttpStatusCode;
        }

        public ScanResponse GetItems(int? id)
        {
            var scanRequest = new ScanRequest();
            if (id.HasValue)
            {

                scanRequest.TableName = "DynamoDbExample";
                scanRequest.ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {":v_Id", new AttributeValue() {N = id.ToString()}}
                };
                scanRequest.FilterExpression = "Id = :v_Id";
                scanRequest.ProjectionExpression = "Id,ReplyDateTime,Price";
            }
            else
            {
                scanRequest.TableName = "DynamoDbExample";
            }

            var response = _dynamoDbClient.ScanAsync(scanRequest).GetAwaiter().GetResult();

            return response;

        }

        public UpdateItemResponse UpdateItem(int id, double price)
        {
            var getItemResponse = GetItems(id);
            var currentPrice = getItemResponse.Items.Select(i => { return new {Price = i["Price"].N}; })
                .FirstOrDefault().Price;
            var replyDateTime = getItemResponse.Items.Select(i => { return new { ReplyDateTime = i["ReplyDateTime"].N }; })
                .FirstOrDefault().ReplyDateTime;

            var request = new UpdateItemRequest()
            {
                Key = new Dictionary<string, AttributeValue>()
                {
                    {
                        "Id",new AttributeValue()
                        {
                            N = id.ToString()
                        }
                    },
                    {
                        "ReplyDateTime",new AttributeValue()
                        {
                            N = replyDateTime
                        }
                    }
                },
                ExpressionAttributeNames = new Dictionary<string, string>()
                {
                    {"#P","Price" }
                },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>()
                {
                    {
                        ":newprice",new AttributeValue()
                        {
                            N = price.ToString()
                        }
                    },
                    {
                        ":currprice",new AttributeValue()
                        {
                            N = currentPrice
                        }
                    }
                },
                UpdateExpression = " SET #P = :newprice",
                ConditionExpression = "#P = :currprice",
                TableName = "DynamoDbExample",
                ReturnValues = "ALL_NEW"
            };
            var updateResponse = _dynamoDbClient.UpdateItemAsync(request).GetAwaiter().GetResult();
            return updateResponse;
        }
    }
}
