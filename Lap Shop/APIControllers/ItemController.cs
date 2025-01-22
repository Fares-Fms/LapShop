using Lap_Shop.BL;
using Lap_Shop.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lap_Shop.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        IItems oitems;
        public ItemController(IItems items)
        {
            oitems = items;
        }
        // GET: api/<ItemController>
        [HttpGet("{ID}")]
        [Route("GetAll/{ID}")]
        public ApiResponse GetAll(int? ID)
        {
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.data= oitems.GetAllItemsDta(ID).Take(25).ToList();
            apiResponse.errorMessage = null;
            apiResponse.status = "200";
            return apiResponse;
        }

        [HttpGet("{ID}")]
      [Route("Get/{ID}")]
        public ApiResponse Get(int ID)
        {
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.data = oitems.GetById(ID);
            apiResponse.errorMessage = null;
            apiResponse.status = "200";
            return apiResponse;
           
        }

        // POST api/<ItemController>
        [HttpPost]
        public ApiResponse Delete([FromBody] int Id)
        {
            try
            {
                ApiResponse apiResponse = new ApiResponse();
                apiResponse.data = oitems.Delete(Id);
                apiResponse.errorMessage = null;
                apiResponse.status = "200";
                return apiResponse;
            }
            catch (Exception ex)
            {
                ApiResponse apiResponse = new ApiResponse();
                apiResponse.data = null;
                apiResponse.errorMessage = ex.Message;
                apiResponse.status = "404";
                return apiResponse;
            }
            
        }
        [HttpPost("Save")]
        [Route("Save")]
        public ApiResponse Save([FromBody] TbItem Id)
        {
            try
            {
                ApiResponse apiResponse = new ApiResponse();
                apiResponse.data = oitems.Save(Id);
                apiResponse.errorMessage = null;
                apiResponse.status = "200";
                return apiResponse;
            }
            catch (Exception ex)
            {
                ApiResponse apiResponse = new ApiResponse();
                apiResponse.data = null;
                apiResponse.errorMessage = ex.Message;
                apiResponse.status = "404";
                return apiResponse;
            }
           
        }

    }
}
