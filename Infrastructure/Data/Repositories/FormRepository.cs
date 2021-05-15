using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Contexts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly IMongoCollection<Form> _forms;

        public FormRepository(IDataContext context)
        {
            MongoClient _client = new MongoClient(context.ConnectionString);
            IMongoDatabase _database = _client.GetDatabase(context.DatabaseName);
            _forms = _database.GetCollection<Form>(context.FormCollectionName);
        }

        public void Create(Form form)
        {
            try
            {
                _forms.InsertOne(form);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ICollection<Form> GetAll()
        {
            try
            {
                return _forms.AsQueryable().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Form Get(string id)
        {
            try
            {
                return _forms.AsQueryable().ToList().Find(f => f.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Form form)
        {
            try
            {
                _forms.ReplaceOne(
                    f => f.Id == form.Id,
                    form
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
