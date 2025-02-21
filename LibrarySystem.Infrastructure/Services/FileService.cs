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
        _cloudinary = new Cloudinary(_account);
        _cloudinary.Api.Secure = true;
    }
    public async Task<bool> Delete(string publicId)
    {
        if (string.IsNullOrEmpty(publicId))
            return false;

        DeletionParams? deletionParams = new(publicId);
        DeletionResult? res = await _cloudinary.DestroyAsync(deletionParams);
        return res.Result == "ok";
    }
    public async Task<string?> GetFile(string publicId)
    {
        if (string.IsNullOrEmpty(publicId))
            return null;

        GetResourceResult? getResult = await _cloudinary.GetResourceAsync(publicId);
        return getResult.SecureUrl;
    }
    public async Task<string?[]> GetFiles(string?[] publicIds)
    {
        if (publicIds.Length == 0 || publicIds is null)
            return [];

        IEnumerable<Task<string?>>? res = publicIds.Select(GetFile!);
        string?[]? result = await Task.WhenAll(res);
        return result;
    }
    public async Task<FileUploadResult> Upload(IFormFile file)
    {
        if (file.Length == 0)
            throw new EmptyFileException();

        using var str = file.OpenReadStream();
        var uploadParam = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, str),
            Transformation = new Transformation()
            .Crop("fill")
            .Gravity("face")
        };
        UploadResult? uploadResult = await _cloudinary.UploadAsync(uploadParam);
        if (uploadResult.Error != null)
            throw new FileUploadException();

        return new FileUploadResult(uploadResult.SecureUrl.AbsoluteUri, uploadResult.PublicId);
    }
    public async Task<FileUploadResult[]> Upload(List<IFormFile> files)
    {
        if (files.Count == 0 || files is null)
            return [];

        IEnumerable<Task<FileUploadResult>>? upload = files.Select(Upload);
        FileUploadResult[]? uploadResult = await Task.WhenAll(upload);
        return uploadResult;
    }
}