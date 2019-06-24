using System;
using System.Threading.Tasks;
using System.Web.Http;
using WindowsStorageLayer.Models;

namespace WindowsStorageLayer.Controllers
{
    
    public class StorageController : ApiController
    {
        // GET
        [Route("temp/{uuid}")]
        [HttpPut]
        public async Task<string> MoveToPersist([FromUri] string uuid)
        {
            return "hello, world";
        }

        [Route("temp/{uuid}")]
        [HttpPatch]
        public async Task<string> UploadData([FromUri] string uuid)
        {
            return "nmsl";
        }

        [Route("data/exists/{hash}")]
        [HttpGet]
        public async Task<bool> CheckDataExists([FromUri] string hash)
        {
            return false;
        }

        [Route("temp/{hash}")]
        [HttpPost]
        public async Task<string> TempFileDeclear([FromUri] string hash, [FromBody] PostBody postBody)
        {
            return "nmsl";
        }

        [Route("data/{hash}")]
        [HttpGet]
        public async Task<string> GetFileWithHash([FromUri] string hash)
        {
            // TODO: change the interface frontend for this
            throw new NotImplementedException();
        }
        
    }
}