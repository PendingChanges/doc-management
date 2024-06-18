using Doc.Management.CommandHandlers;
using Doc.Management.Documents;
using Doc.Management.Documents.Commands;
using Doc.Management.ValueObjects;
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
        if (uploadFile.File == null || uploadFile.File.Length == 0)
        {
            return BadRequest("No file selected");
        }

        var extension = Path.GetExtension(uploadFile.File.FileName).ToLowerInvariant();

        if (string.IsNullOrEmpty(extension))
        {
            return BadRequest("Invalid file type");
        }

        var key = DocumentKey.NewDocumentKey();

        using (var stream = new MemoryStream())
        {
            await uploadFile.File.CopyToAsync(stream);

            await _fileStore.UploadStreamAsync(stream, key, cancellationToken).ConfigureAwait(false);
        }

        var command = new WrappedCommand<CreateDocument, Document>(new CreateDocument(key, Path.GetFileNameWithoutExtension(uploadFile.File.FileName), extension), _context.UserId);

        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result?.Id);
    }
}
