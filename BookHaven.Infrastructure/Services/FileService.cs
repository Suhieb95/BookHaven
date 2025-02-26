using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using BookHaven.Application.Interfaces.Services;
using BookHaven.Domain.DTOs;
using BookHaven.Domain.Exceptions.FileUploadExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace BookHaven.Infrastructure.Services;
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
    public async Task<string?> GetFile(string publicId, CancellationToken? cancellationToken = default)
    {
        if (string.IsNullOrEmpty(publicId))
            return null;

        GetResourceResult? getResult = await _cloudinary.GetResourceAsync(publicId, cancellationToken: cancellationToken);
        return getResult.SecureUrl;
    }
    public async Task<string?[]?> GetFiles(string[] publicIds, CancellationToken? cancellationToken = default)
    {
        if (publicIds is null)
            return [];

        IEnumerable<Task<string?>> res = publicIds.Select(x => GetFile(x, cancellationToken));
        string?[]? result = await Task.WhenAll(res);
        return result;
    }
    public async Task<FileUploadResult> Upload(IFormFile file, CancellationToken? cancellationToken = default)
    {
        if (file.Length == 0)
            throw new EmptyFileException();

        using Stream? str = file.OpenReadStream();
        var uploadParam = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, str),
            Transformation = new Transformation()
            .Height(300)
            .Width(300)
            .Crop("fill")
            .Gravity("face")
        };
        UploadResult? uploadResult = await _cloudinary.UploadAsync(uploadParam, cancellationToken: cancellationToken);
        if (uploadResult.Error != null)
            throw new FileUploadException();

        return new FileUploadResult(uploadResult.SecureUrl.AbsoluteUri, uploadResult.PublicId);
    }
    public async Task<FileUploadResult[]> Upload(IFormFileCollection files, CancellationToken? cancellationToken = default)
    {
        if (files.Count == 0 || files is null)
            return [];

        IEnumerable<Task<FileUploadResult>>? upload = files.Select(x => Upload(x, cancellationToken));
        FileUploadResult[]? uploadResult = await Task.WhenAll(upload);
        return uploadResult;
    }
}