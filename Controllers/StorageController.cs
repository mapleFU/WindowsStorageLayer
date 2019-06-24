using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WindowsStorageLayer.Models;
using WindowsStorageLayer.Tools;
using Newtonsoft.Json;

namespace WindowsStorageLayer.Controllers
{
    
    public class StorageController : ApiController
    {
        // GET
        [Route("temp/{uuid}")]
        [HttpPut]
        public async Task<string> MoveToPersist([FromUri] string uuid)
        {
            throw new NotImplementedException();
            return "hello, world";
        }

        [Route("temp/{uuid}")]
        [HttpPatch]
        public async Task<string> UploadData([FromUri] string uuid)
        {
            var dataStream = await Request.Content.ReadAsStreamAsync();
            throw new NotImplementedException();
            return "hello, world";
        }

        [Route("data/exists/{hash}")]
        [HttpGet]
        public async Task<bool> CheckDataExists([FromUri] string hash)
        {
            string dataFilePath = Path.Combine("data", hash);
            string zipDataFilePath = dataFilePath + ".zip";
            return File.Exists(dataFilePath) || File.Exists(zipDataFilePath);
        }

        [Route("temp/{hash}")]
        [HttpPost]
        public async Task<IHttpActionResult> TempFileDeclear([FromUri] string hash, [FromBody] PostBody postBody)
        {
            string dataFilePath = Path.Combine("data", hash);
            string zipDataFilePath = dataFilePath + ".zip";
            if (File.Exists(dataFilePath) || File.Exists(zipDataFilePath))
            {
                return BadRequest("Duplicate");
            }

            var message = new RedisUuidMessage();
            message.Uid = Guid.NewGuid().ToString();
            message.Size = postBody.size;
            message.FileHash = hash;
            var db = ZkConnection.Db;
            Console.WriteLine(message.Uid);
            var serData = JsonConvert.SerializeObject(message);
            
            await db.StringSetAsync(message.Uid, serData);
            return Ok(message.Uid);
        }

        [Route("data/{hash}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetFileWithHash([FromUri] string hash)
        {
            string dataFilePath = Path.Combine("data", hash);
            string zipDataFilePath = dataFilePath + ".zip";
            if (!(File.Exists(dataFilePath) || File.Exists(zipDataFilePath)))
            {
                return NotFound();
            }
            
            // TODO: change the interface frontend for this
            throw new NotImplementedException();
        }
        
    }
}