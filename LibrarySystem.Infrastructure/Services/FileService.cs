using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.DTOs;
using LibrarySystem.Domain.Exceptions.FileUploadExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace LibrarySystem.Infrastructure.Services;
public class FileService : IFileService
{
    private readonly Account _account;
    private readonly Cloudinary _cloudinary;
    public FileService(IOptions<Account> account)
    {
        _account = account.Value;
        _cloudinary = new(_account);
    }
    public async Task<string?> Delete(string publicId)
    {
        DeletionParams? deletionParams = new(publicId);
        var res = await _cloudinary.DestroyAsync(deletionParams);
        return res.Result == "ok" ? res.Result : null;
    }
    public async Task<PhotoUploadResult> Upload(IFormFile file)
    {
        var uploadResult = new ImageUploadResult();
        if (file.Length == 0)
            throw new EmptyFileException();

        using var str = file.OpenReadStream();
        var uploadParam = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, str),
            Transformation = new Transformation()
                    .Height(500)
                    .Width(500)
                    .Crop("fill")
                    .Gravity("face")
        };

        uploadResult = await _cloudinary.UploadAsync(uploadParam);

        if (uploadResult.Error != null)
            throw new FileUploadException();

        return new PhotoUploadResult(uploadResult.SecureUrl.AbsoluteUri, uploadResult.PublicId);
    }
    public async Task<PhotoUploadResult[]> Upload(List<IFormFile> files)
    {
        if (files.Count == 0)
            return [];

        var upload = files.Select(Upload);
        PhotoUploadResult[]? uploadResult = await Task.WhenAll(upload);
        return uploadResult;
    }
}