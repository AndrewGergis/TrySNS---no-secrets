using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.Model;
using DynamoDb.Libs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DynamoDb.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DynamoDbController : ControllerBase
    {
        private readonly IDynamoDbExample _dynamoDbExample;

        public DynamoDbController(IDynamoDbExample dynamoDbExample)
        {
            _dynamoDbExample = dynamoDbExample;
        }

        [HttpPost("createtable")]
        public IActionResult CreateTable()
        {
            try
            {
                var statusCode = _dynamoDbExample.CreateTable();
                if (statusCode == HttpStatusCode.OK)
                {
                    return Ok("Table created successfully!");
                }

                return BadRequest($"Error while creating table, StatusCode: {statusCode}");
            }
            catch (Exception exp)
            {
                return BadRequest($"Error while creating table: {exp.Message}");
            }
        }

        [HttpPost("putitem")]
        public IActionResult PutItem(int id, string replyDateTime, double price)
        {
            try
            {
                var statusCode = _dynamoDbExample.AddNewItem(id, replyDateTime, price);
                if (statusCode == HttpStatusCode.OK)
                {
                    return Ok("Item added!");
                }

                return BadRequest($"Error while adding item, StatusCode: {statusCode}");
            }
            catch (Exception exp)
            {
                return BadRequest($"Error while adding item: {exp.Message}");
            }
        }

        [HttpGet("items")]
        public IActionResult GetItems(int? id)
        {
            try
            {
                var response = _dynamoDbExample.GetItems(id);
                if (response.HttpStatusCode == HttpStatusCode.OK)
                {
                    return Ok(response.Items.Select(item =>
                    {
                        return new
                        {
                            Id = Convert.ToInt32(item["Id"].N),
                            ReplyDateTime = item["ReplyDateTime"].N,
                            Price= item.ContainsKey("Price")?item["Price"].N:""
                        };
                    }));
                }

                return BadRequest($"Error while getting the items, status code: {response.HttpStatusCode}");
            }
            catch (Exception exp)
            {
                return BadRequest($"Error while getting the items: {exp.Message}");
            }
        }

        [HttpPut("updateitem")]
        public IActionResult UpdateItem(int id, double price)
        {
            try
            {
                var response = _dynamoDbExample.UpdateItem(id, price);
                if (response.HttpStatusCode==HttpStatusCode.OK)
                {
                    return Ok(response);
                }

                return BadRequest($"Error while updating item, status code: {response.HttpStatusCode}");
            }
            catch (Exception exp)
            {
                return BadRequest($"Error while updating item: {exp.Message}");
            }
        }
    }
}