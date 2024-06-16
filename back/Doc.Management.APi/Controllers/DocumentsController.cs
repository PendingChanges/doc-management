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
    private readonly IStoreFile _fileStore;

    public class Upload
    {
        public IFormFile? File { get; set; }
    }
    public DocumentsController(IMediator mediator, IContext context, IStoreFile fileStore)
    {
        _mediator = mediator;
        _context = context;
        _fileStore = fileStore;
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

        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);

            await _fileStore.UploadStreamAsync(stream, newFileName, cancellationToken).ConfigureAwait(false);
        }

        var command = new WrappedCommand<CreateDocument, Document>(new CreateDocument(file.FileName), _context.UserId);

        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result?.Id);
    }
}
