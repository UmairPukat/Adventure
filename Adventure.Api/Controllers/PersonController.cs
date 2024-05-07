namespace Adventure.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ILogger<PersonController> _logger;
        public PersonController(IPersonService personService
            , ILogger<PersonController> logger)
        {
            _personService = personService;
            _logger = logger;

        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _personService.GetAll();
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetById(long Id)
        {
            var result = await _personService.GetById(Id);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Add(PersonDto model)
        {
            bool result = await _personService.Add(model);
            if (!result)
                return BadRequest(new { message = "Something Went Wrong" });

            return Ok(new { message = "Data Added Successfully" });
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Update(long Id, PersonDto model)
        {
            bool result = await _personService.Update(Id, model);
            if (!result)
            {
                return BadRequest(new { message = "Something Went Wrong" });
            }
            return Ok(new { message = "Data Updated Successfully" });
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Delete(long Id)
        {
            var Product = await _personService.FindById(Id);
            if (Product == null)
                return BadRequest(new { message = "Data Not Found" });

            bool result = await _personService.Delete(Product);
            if (!result)
            {
                return BadRequest(new { message = "Something Went Wrong" });
            }
            return Ok(new { message = "Data Deleted Successfully" });
        }
    }
}

