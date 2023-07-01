using ImgUpoader.Application;
using ImgUpoader.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ImgUpoader.Controllers;

[ApiController]
[Route("[controller]")]
public class ImgsController : ControllerBase
{
    ImgService _imgservice;

    public ImgsController(ImgService imgservice)
    {
        _imgservice = imgservice;
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
}