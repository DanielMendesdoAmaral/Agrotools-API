using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Contexts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly IMongoCollection<Answer> _answers;

        public AnswerRepository(IDataContext context)
        {
            MongoClient _client = new MongoClient(context.ConnectionString);
            IMongoDatabase _database = _client.GetDatabase(context.DatabaseName);
            _answers = _database.GetCollection<Answer>(context.AnswerCollectionName);
        }

        public void Create(Answer answer)
        {
            try
            {
                _answers.InsertOne(answer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ICollection<Answer> Get(string questionId)
        {
            try
            {
                return _answers.AsQueryable().ToList().FindAll(a => a.QuestionId == questionId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
