﻿using System.Data;
using BookHaven.Domain.Specification;
namespace BookHaven.Application.Interfaces.Database;
public interface ISqlDataAccess
{
    Task<T?> LoadFirstOrDefault<T>(string sql, object? parameters = default, CommandType? commandType = null, CancellationToken? cancellationToken = null);
    Task<List<T>> LoadData<T>(string sql, object? parameters = default, CommandType? commandType = null, CancellationToken? cancellationToken = null);
    Task<T?> SaveData<T>(string sql, object? parameters = default, CommandType? commandType = null, CancellationToken? cancellationToken = null);
    Task SaveData<P>(string sql, P? parameters = default, CommandType? commandType = null, CancellationToken? cancellationToken = null);
    Task<(List<T1>, List<T2>)> FetchTwoListsAsync<T1, T2>(string sql, CancellationToken? cancellationToken = null, object? param = default, CommandType? commandType = null);
    Task<(T1?, List<T2>)> FetchSingleAndListAsync<T1, T2>(string sql, CancellationToken? cancellationToken = null, object? param = default, CommandType? commandType = null);
    Task<(List<T1>, T2?)> FetchListAndSingleAsync<T1, T2>(string sql, CancellationToken? cancellationToken = null, object? param = default, CommandType? commandType = null);
    Task<List<T>> LoadData<T>(Specification<T> specification, CancellationToken? cancellationToken = null);
    Task<T?> LoadFirstOrDefault<T>(Specification<T> specification, CancellationToken? cancellationToken = null);
}