using Amazon.DynamoDBv2.Model;
using System.Net;

namespace DynamoDb.Libs
{
    public interface IDynamoDbExample
    {
        HttpStatusCode CreateTable();
        HttpStatusCode AddNewItem(int id, string replyDateTime, double price);
        ScanResponse GetItems(int? id);
        UpdateItemResponse UpdateItem(int id, double price);
    }
}