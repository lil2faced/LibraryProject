using LibraryProject.Applications;
using LibraryProject.Entities.BookProps;
using Microsoft.EntityFrameworkCore;
using LibraryProject.Interfaces;
using AutoMapper;
using LibraryProject.ControllerModels;

namespace LibraryProject.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly DatabaseContext _db;
        private readonly IMapper _mapper;
        public AuthorService(DatabaseContext databaseContext, IMapper mapper)
        {
            _mapper = mapper;
            _db = databaseContext;
        }
        public async Task AddAuthorAsync(AuthorModel? author, CancellationToken cancellationToken)
        {
            var mappedAuthor = _mapper.Map<BookAuthor>(author);
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            } 
            if (author == null)
            {
                throw new ArgumentNullException("На входе пришел NULL");
            }
            var Author = await _db.BookAuthors.Where(a => a.Name == author.Name).FirstOrDefaultAsync();
            if (Author != null)
            {
                throw new Exception("Автор не найден");
            }
            _db.BookAuthors.Add(mappedAuthor);            
            await _db.SaveChangesAsync();
        }
        public async Task<List<AuthorModel>> GetAllAuthorsAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            return await _db.BookAuthors
                .Select(a => _mapper.Map<AuthorModel>(a))
                .ToListAsync();
        }
        public async Task<AuthorModel> GetAuthorByIdAsync(int? id, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            if (id == null)
            {
                throw new ArgumentNullException("На входе пришел NULL");
            }

            var author = await _db.BookAuthors.FindAsync(id, cancellationToken);
            if (author == null)
            {
                throw new Exception("Автор не найден");
            }
            return _mapper.Map<AuthorModel>(author);
        }
        public async Task DeleteByIdAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("Id = null");
            }
            var author = await _db.BookAuthors.FindAsync(id);
            if (author == null)
            {
                throw new Exception("Автор не найден");
            }
            _db.BookAuthors.Remove(author);
            await _db.SaveChangesAsync();
        }
        public async Task UpdateByIDAsync(int? id, AuthorModel aut)
        {
            _mapper.Map<BookAuthor>(aut);
            if (id == null || aut == null)
            {
                throw new ArgumentNullException("На входе пришел NULL");
            }
            var author = await _db.BookAuthors.FindAsync(id);
            if (author == null)
            {
                throw new Exception("Автор не найден");
            }
            author.Name = aut.Name;
            author.Biography = aut.Biography;
            _db.BookAuthors.Update(author);
            await _db.SaveChangesAsync();
        }
    }
}
