using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Contexts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IMongoCollection<Question> _questions;

        public QuestionRepository(IDataContext context)
        {
            MongoClient _client = new MongoClient(context.ConnectionString);
            IMongoDatabase _database = _client.GetDatabase(context.DatabaseName);
            _questions = _database.GetCollection<Question>(context.QuestionCollectionName);
        }

        public void Create(Question question)
        {
            try
            {
                _questions.InsertOne(question);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ICollection<Question> Get(string formId)
        {
            try
            {
                return _questions.AsQueryable().Where(q => q.FormId == formId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
