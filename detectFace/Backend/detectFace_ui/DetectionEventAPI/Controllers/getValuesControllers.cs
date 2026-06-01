using Microsoft.AspNetCore.Mvc;
using CommonModels;
using static CommonModels.Class1;


namespace DetectionEventAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetectionController : ControllerBase
    {
        // Python'dan gelen en son veriyi bellekte tutan statik değişken
        private static DetectionData _sonVeri;

        // Rota: POST api/detection
        [HttpPost]
        public IActionResult ReceiveData([FromBody] DetectionData data)
        {
            if (data == null)
                return BadRequest("Veri alınamadı.");

            // Gelen veriyi hafızaya kaydet
            _sonVeri = data;

            Console.WriteLine($"Kamera: {data.camera_id}, Güven: %{data.confidence}, X: {data.bbox.x_degeri}");

            return Ok(new { status = "Başarılı", receivedConfidence = data.confidence });
        }

        // Rota: GET api/detection/son-veri
        // C# UI (Form1) tarafından sürekli sorgulanacak endpoint
        [HttpGet("son-veri")]
        public IActionResult GetLatestData()
        {
            if (_sonVeri == null)
                return NotFound("Henüz veri alınmadı.");

            return Ok(_sonVeri);
        }
    }
}