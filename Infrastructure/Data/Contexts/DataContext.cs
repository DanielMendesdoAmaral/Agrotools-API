﻿namespace Infrastructure.Data.Contexts
{
    public class DataContext : IDataContext
    {
        public string AuthorCollectionName { get; set; }
        public string FormCollectionName { get; set; }
        public string QuestionCollectionName { get; set; }
        public string AnswerCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
