using LibrarySystem.Domain.DTOs;
using Microsoft.AspNetCore.Http;
namespace LibrarySystem.Application.Interfaces.Services;

public interface IFileService
{
    Task<FileUploadResult> Upload(IFormFile file);
    Task<FileUploadResult[]> Upload(List<IFormFile> files);
    Task<bool> Delete(string publicId);
    Task<string?> GetFile(string publicId);
    Task<string?[]?> GetFiles(string[] publicIds);
}
