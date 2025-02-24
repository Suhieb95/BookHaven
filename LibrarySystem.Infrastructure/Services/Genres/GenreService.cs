using LibrarySystem.Application.Interfaces.Database;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Infrastructure.Services.Genres
{
    public class GenreService(ISqlDataAccess sqlDataAccess) : IGenreService
    {
        public async Task<int> Add(Genre entity, CancellationToken? cancellationToken = null)
            => await sqlDataAccess.SaveData<int>("SpCreateGenre", entity.Name, StoredProcedure, cancellationToken);
        

        public Task Delete(int id, CancellationToken? cancellationToken = null)
        {
            throw new NotImplementedException();
        }

        public Task Update(Genre entity, CancellationToken? cancellationToken = null)
        {
            throw new NotImplementedException();
        }
    }
}
