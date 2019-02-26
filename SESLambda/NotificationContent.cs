using System;
using System.Collections.Generic;
using System.Text;
using Amazon.DynamoDBv2.Model;

namespace SESLambda
{
    public class NotificationContent
    {
        public List<Record> Records { get; set; }
    }
}
