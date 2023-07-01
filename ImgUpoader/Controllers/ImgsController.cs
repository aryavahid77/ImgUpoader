using ImgUpoader.Application;
using ImgUpoader.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ImgUpoader.Controllers;

[ApiController]
[Route("[controller]")]
public class ImgsController : ControllerBase
{
    IImgService _imgservice;
    IImgImporter _ImgImporter;

    public ImgsController(IImgService imgservice, IImgImporter imgImporter)
    {
        _imgservice = imgservice;
        _ImgImporter = imgImporter;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UploadAsync(IFormFile imgFile)
    {
        await _imgservice.UploadAsync(imgFile);
        return Ok();
    }


    [HttpGet("[action]")]
    public async Task<IActionResult> ListAsync(int pageSize, int pageNo)
    {
        var lst=await _imgservice.ListAsync(pageSize,pageNo);
        return Ok(lst);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Import()
    {
        await _ImgImporter.Import();
        return Ok();
    }
}