using Doc.Management.CommandHandlers;
using Doc.Management.Documents;
using Doc.Management.Documents.Commands;
using Doc.Management.Documents.DataModels;
using Doc.Management.ValueObjects;
using Journalist.Crm.Api.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Doc.Management.Api.Documents;

internal static class Endpoints
{
    public static WebApplication MapDocuments(this WebApplication app)
    {
        app.MapPost("api/documents", UploadDocument)
     .Produces(StatusCodes.Status400BadRequest)
      .Produces(StatusCodes.Status201Created)
     .DisableAntiforgery();

        app.MapGet("api/documents/{id}/infos", GetDocumentData);

        app.MapGet("api/documents/{id}", GetDocument);

        app.MapGet("api/documents", GetDocuments);

        return app;
    }

    private static async Task<DocumentResultSet> GetDocuments(
        [FromServices] IReadDocuments documentReader,
        [FromQuery] int skip = 0,
        [FromQuery] int take = 20,
        [FromQuery] string sortBy = "name",
        [FromQuery] string sortDirection = "asc"
        )
    {
        var documentResultSet = await documentReader.GetDocumentsAsync(new GetDocumentsRequest(skip, take, sortBy, sortDirection));

        return documentResultSet;
    }

    private static async Task<Results<FileStreamHttpResult, NotFound>> GetDocument(
    [FromServices] IReadDocuments documentReader,
    [FromServices] IStoreFile fileStore,
    [FromRoute] string id,
    [FromQuery] string? version,
    CancellationToken cancellationToken = default)
    {
        var document = await documentReader.GetDocumentByIdAsync(id, version != null ? Version.Parse(version) : null, cancellationToken);

        if (document == null)
        {
            return TypedResults.NotFound();
        }

        var fileStream = await fileStore.GetStreamAsync(document.Key, cancellationToken);

        return TypedResults.File(fileStream, fileDownloadName: $"{document.Name}.{document.Extension}");
    }

    private static async Task<Results<Ok<DocumentDocument>, NotFound>> GetDocumentData(
        [FromServices] IReadDocuments documentReader,
        [FromRoute] string id,
        [FromQuery] string? version,
        CancellationToken cancellationToken = default)
    {
        var document = await documentReader.GetDocumentByIdAsync(id, version != null ? Version.Parse(version) : null, cancellationToken);

        if (document == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(document);
    }

    public static async Task<Results<Created<DocumentDocument>, BadRequest<string>>> UploadDocument([FromServices] IMediator mediator,
     [FromServices] IStoreFile fileStore,
     [FromServices] IContext context,
     IFormFile uploadFile,
     CancellationToken cancellationToken = default)
    {
        if (uploadFile == null || uploadFile.Length == 0)
        {
            return TypedResults.BadRequest("No file selected");
        }

        var fileName = Path.GetFileNameWithoutExtension(uploadFile.FileName);
        var extension = Path.GetExtension(uploadFile.FileName).ToLowerInvariant();

        if (string.IsNullOrEmpty(extension))
        {
            return TypedResults.BadRequest("Invalid file type");
        }

        var key = DocumentKey.NewDocumentKey();

        using (var stream = new MemoryStream())
        {
            await uploadFile.CopyToAsync(stream, cancellationToken);

            await fileStore.UploadStreamAsync(stream, key, cancellationToken).ConfigureAwait(false);
        }

        var command = new WrappedCommand<CreateDocument, Document>(new CreateDocument(key, fileName, Path.GetFileNameWithoutExtension(uploadFile.FileName), extension), context.UserId);

        var result = await mediator.Send(command, cancellationToken);

        var document = new DocumentDocument(result.Id, result.Key, result.Name, result.FileNameWIthoutExtension, result.Extension, result.DocumentVersion);

        return TypedResults.Created($"/documents/{result.Id}", document);
    }
}
