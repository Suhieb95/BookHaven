using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Genres
{
    public interface IGenreApplicationService
    {
       Task<Result<int>> Add(Genre entity, CancellationToken? cancellationToken = null);
    }
}
