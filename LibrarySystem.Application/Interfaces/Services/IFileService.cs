using LibrarySystem.Domain.DTOs;
using Microsoft.AspNetCore.Http;
namespace LibrarySystem.Application.Interfaces.Services;

public interface IFileService
{
    Task<FileUploadResult> Upload(IFormFile file, CancellationToken? cancellationToken = default);
    Task<FileUploadResult[]> Upload(IFormFileCollection files, CancellationToken? cancellationToken = default);
    Task<bool> Delete(string publicId);
    Task<string?> GetFile(string publicId, CancellationToken? cancellationToken = default);
    Task<string?[]?> GetFiles(string[] publicIds, CancellationToken? cancellationToken = default);
}
