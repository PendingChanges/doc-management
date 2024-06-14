using Doc.Management.CommandHandlers;
using Doc.Management.Documents;
using Doc.Management.Documents.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Doc.Management.Api.Controllers;

[Route("api/documents")]
[ApiController]
public class DocumentsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IContext _context;

    public class Upload
    {
        public IFormFile? File { get; set; }
    }
    public DocumentsController(IMediator mediator, IContext context)
    {
        _mediator = mediator;
        _context = context;
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromForm] Upload uploadFile, CancellationToken cancellationToken = default)
    {
        IFormFile? file = uploadFile.File;

        if (file == null || file.Length == 0)
            return BadRequest("No file selected");

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (string.IsNullOrEmpty(extension))
            return BadRequest("Invalid file type");

        // Generate a new file name with a GUID
        var newFileName = Path.GetRandomFileName() + extension;

        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        var filePath = Path.Combine(folderPath, newFileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var command = new WrappedCommand<CreateDocument, Document>(new CreateDocument(file.FileName), _context.UserId);

        var result = await _mediator.Send(command, cancellationToken);

        var fileUrl = Url.Content($"~/uploads/{newFileName}");
        return Ok(new { url = fileUrl });
    }
}
