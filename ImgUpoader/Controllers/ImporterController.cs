using ImgUpoader.Application;
using Microsoft.AspNetCore.Mvc;

namespace ImgUpoader.Controllers;

[ApiController]
[Route("[controller]")]
public class ImporterController : ControllerBase
{
    IImgImporter _ImgImporter;

    public ImporterController( IImgImporter imgImporter)
    {
        _ImgImporter = imgImporter;
    }


    [HttpPost("[action]")]
    public async Task<IActionResult> Import()
    {
        await _ImgImporter.Import();
        return Ok();
    }
}