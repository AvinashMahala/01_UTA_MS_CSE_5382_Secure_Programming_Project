using Microsoft.AspNetCore.Mvc;
using NLog;
using PhoneBook.Exceptions;
using PhoneBook.Model;
using PhoneBook.Services;

namespace PhoneBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhoneBookController : ControllerBase
    {
        private readonly IPhoneBookService _phoneBookService;
        private readonly ILogger<PhoneBookController> _logger;
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        public PhoneBookController(IPhoneBookService phoneBookService, ILogger<PhoneBookController> logger)
        {
            _phoneBookService = phoneBookService;
            _logger = logger;
        }

        [HttpGet]
        [Route("list")]
        public IEnumerable<PhoneBookEntry> List()
        {
            try
            {
                return _phoneBookService.List();
            }
            catch (Exception ex)
            {
                logger.Error(ex,"Error Occured While Retrieving Phonenumber List!!");
                throw;
            }
            
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody]PhoneBookEntry newEntry)
        {
            if (!ModelState.IsValid)
            {
                logger.Error("Invalid Name or Phonenumber Provided!!");
                return BadRequest(ModelState);
            }

            _phoneBookService.Add(newEntry);

            return Ok();
        }

        [HttpPut]
        [Route("deleteByName")]
        public IActionResult DeleteByName([FromQuery] string name)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    logger.Error("Missing Required Name Field!!");
                    return BadRequest(ModelState);
                }
                _phoneBookService.DeleteByName(name);

                return Ok();
            }
            catch (NotFoundException ex)
            {
                logger.Error(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        [Route("deleteByNumber")]
        public IActionResult DeleteByNumber([FromQuery] string number)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    logger.Error("Missing Required Number Field!!");
                    return BadRequest(ModelState);
                }
                _phoneBookService.DeleteByNumber(number);

                return Ok();
            }
            catch (NotFoundException ex)
            {
                logger.Error(ex.Message);
                return NotFound(ex.Message);
            }
        }
    }
}