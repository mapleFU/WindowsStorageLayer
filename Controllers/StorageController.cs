using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
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
        public async Task<IHttpActionResult> MoveToPersist([FromUri] string uuid)
        {
            var s = await ZkConnection.Db.StringGetAsync(uuid);
            
            if (s.IsNull)
            {
                return NotFound();
            }
            
            var o = JsonConvert.DeserializeObject<RedisUuidMessage>(s.ToString());

            string dataFilePath = Path.Combine("data", "temp", uuid);
            
            if (!File.Exists(dataFilePath))
            {
                return BadRequest();
            }
            var src = File.OpenRead(dataFilePath);
            string destFilePath = Path.Combine("data", o.FileHash);
            string destFilePathGz = destFilePath + ".gz";
            var dest = File.Create(destFilePath);
//            var dest = new GZipStream(File.Create(destFilePathGz), CompressionMode.Compress);
            await src.CopyToAsync(dest);
            return Ok();
        }

        [Route("temp/{uuid}")]
        [HttpPatch]
        public async Task<IHttpActionResult> UploadData([FromUri] string uuid)
        {
           
            var s = await ZkConnection.Db.StringGetAsync(uuid);
            
            if (s.IsNull)
            {
                return NotFound();
            }
            
            var o = JsonConvert.DeserializeObject<RedisUuidMessage>(s.ToString());
            var dataStream = await Request.Content.ReadAsStreamAsync();
            var new_fs = File.Create(Path.Combine("data", "temp", o.Uid));
            await dataStream.CopyToAsync(new_fs);
            // TODO: add checker
            return Ok();
        }

        [Route("data/exists/{hash}")]
        [HttpGet]
        public async Task<bool> CheckDataExists([FromUri] string hash)
        {
            string dataFilePath = Path.Combine("data", hash);
            string zipDataFilePath = dataFilePath + ".gz";
            return File.Exists(dataFilePath) || File.Exists(zipDataFilePath);
        }

        [Route("temp/{hash}")]
        [HttpPost]
        public async Task<IHttpActionResult> TempFileDeclear([FromUri] string hash, [FromBody] PostBody postBody)
        {
            string dataFilePath = Path.Combine("data", hash);
            string zipDataFilePath = dataFilePath + ".gz";
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
            string zipDataFilePath = dataFilePath + ".gz";
            if (!(File.Exists(dataFilePath) || File.Exists(zipDataFilePath)))
            {
                return NotFound();
            }

            
           
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            if (File.Exists(dataFilePath))
            {
                var stream = File.OpenRead(dataFilePath);
//            var gZipStream = new GZipStream(stream, CompressionMode.Decompress);
//            result.Content = new StreamContent(gZipStream);
                result.Content = new StreamContent(stream);
                return ResponseMessage(result);
            }
            else
            {
                throw new NotImplementedException();
            }
            

        }
        
    }
}