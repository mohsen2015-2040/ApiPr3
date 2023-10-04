using ApiPr3.Interface;
using ApiPr3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace ApiPr3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<BooksController> _logger;
        private readonly IDBOperation _dBOperation;

        public BooksController(IDBOperation dBOperation, IConfiguration configuration, ILogger<BooksController> logger)
        {
            _dBOperation = dBOperation;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetBooks")]
        public IActionResult GetBooks()
        {
            return Ok(_dBOperation.Select("SELECT * FROM Book"));
        }

        
        [Route("InsertBook")]
        public IActionResult PostBook()
        {
            var result = _dBOperation.Insert("INSERT INTO Book (PublisherID, Title, Pages, Weight, Subject) VALUES ('2', 'QWERTY', '100', '101','SomeThingSUB')");
            
            if (result > 0)
                return Ok($"{result}(s) was(were) inserted");
            else
                return Ok($"was not successfully! There is an issue");
        }

        [Route("DeleteBook")]
        public IActionResult DeleteBook()
        {
            //result: number of rows effected.
            var result = _dBOperation.Delete("DELETE FROM Book WHERE ID = 1004");

            if (result > 0) 
                return Ok($"{result}(s) was(were) deleted successfully");
            else
                return Ok("was not successfully! There is an issue");
        }

        [Route("UpdateBook")]
        public IActionResult UpdateBook()
        {
            var result = _dBOperation.Update("UPDATE Book SET Subject='fun' WHERE ID='1002'");

            if (result > 0)
                return Ok($"{result}(s) was(were) updated successfully");
            else 
                return Ok("Was not successfully! There is an issue");
        }
    }
}