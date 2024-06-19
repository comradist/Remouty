using MyMind.Application.Contract.Persistence;
using MyMind.Domain.Models.Entities;

namespace MyMind.Persistence.Repositories;

public class NoteRepository : GenericRepositoryManager<Note, Guid>, INoteRepository
{
    public NoteRepository(RepositoryNoteDbContext RepositoryNoteDbContext) : base(RepositoryNoteDbContext)
    {
    }

    public async Task<Note> GetNote(Guid id, bool trackChanges)
    {
        return await Get(p => p.Id.Equals(id), trackChanges);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        var entity = await Get(p => p.Id.Equals(id), false);
        return entity != null;
    }
}