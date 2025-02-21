using LibrarySystem.Domain.DTOs;
using Microsoft.AspNetCore.Http;
namespace LibrarySystem.Application.Interfaces.Services;

public interface IFileService
{
    Task<PhotoUploadResult> Upload(IFormFile file);
    Task<PhotoUploadResult[]> Upload(List<IFormFile> files);
    Task<string?> Delete(string publicId);
}
