using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Contexts;
using MongoDB.Driver;
using System;
using System.Linq;

namespace Infrastructure.Data.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IMongoCollection<Author> _authors;

        public AuthorRepository(IDataContext context)
        {
            MongoClient _client = new MongoClient(context.ConnectionString);
            IMongoDatabase _database = _client.GetDatabase(context.DatabaseName);
            _authors = _database.GetCollection<Author>(context.AuthorCollectionName);
        }

        public void Create(Author author)
        {
            try
            {
                _authors.InsertOne(author);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Author Get(string id)
        {
            try
            {
                return _authors.AsQueryable().ToList().Find(a => a.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
