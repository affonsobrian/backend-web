using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend_Web.Utils
{
    public class MongoDAO<T> where T : BaseMongoModel
    {
        private MongoClient _client;
        private readonly string _connString;
        private IMongoDatabase _database;
        private readonly string _databaseName;
        private IMongoCollection<T> _collection;
        private readonly string _defaultCollectionName;

        private MongoClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new MongoClient(_connString);
                }

                return _client;
            }
        }

        public IMongoDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    _database = Client.GetDatabase(_databaseName);
                }

                return _database;
            }
        }

        protected IMongoCollection<T> Collection
        {
            get
            {
                if (_collection == null)
                {
                    _collection = Database.GetCollection<T>(_defaultCollectionName);
                }
                return _collection;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDAO"/> class.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <param name="connString">The mongo connection string</param>
        public MongoDAO(string databaseName, string connString, string defaultCollectionName)
        {
            _connString = connString;
            _databaseName = databaseName;
            _defaultCollectionName = defaultCollectionName;
        }

        /// <summary>
        /// Gets all items from a colletions.
        /// </summary>
        /// <typeparam name="T">Model Type</typeparam>
        /// <param name="collectionName">Name of the collection.</param>
        /// <returns>List of the especified collection</returns>
        public List<T> GetAll(string collectionName = null)
        {
            try
            {
                if (collectionName == null)
                {
                    collectionName = _defaultCollectionName;
                }

                IMongoCollection<T> collection = Database.GetCollection<T>(collectionName);
                return collection.Find(Builders<T>.Filter.Empty).ToList();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets itens form a collection thast satisfies a given filter
        /// </summary>
        /// <typeparam name="T">Model Type</typeparam>
        /// <param name="collectionName">Name of the collection.</param>
        /// <param name="filter">Filter definition</param>
        /// <returns>List of the especified collection</returns>
        public List<T> GetByFilter(FilterDefinition<T> filter, SortDefinition<T> sort = null, string collectionName = null)
        {
            try
            {
                if (collectionName == null)
                {
                    collectionName = _defaultCollectionName;
                }

                IMongoCollection<T> collection = Database.GetCollection<T>(collectionName);
                if (sort != null)
                {
                    return collection.Find(filter).Sort(sort).ToList();
                }
                else
                {
                    return collection.Find(filter).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets element by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="collectionName">Name of the collection.</param>
        /// <returns></returns>
        public T GetById(ObjectId id, string collectionName = null)
        {
            return GetByFilter(Builders<T>.Filter.Eq(e => e._id, id), null, collectionName)?.FirstOrDefault();
        }

        /// <summary>
        /// Gets element by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="collectionName">Name of the collection.</param>
        /// <returns></returns>
        public T GetById(string id, string collectionName = null)
        {
            return GetById(new ObjectId(id), collectionName);
        }

        /// <summary>
        /// Inserts in the specified collection name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName">Name of the collection.</param>
        /// <param name="model">The model.</param>
        public void Insert(T model, string collectionName = null)
        {
            if (collectionName == null)
            {
                collectionName = _defaultCollectionName;
            }

            IMongoCollection<T> collection = Database.GetCollection<T>(collectionName);
            collection.InsertOne(model);
        }

        public void Insert(List<T> model, string collectionName = null)
        {
            if (model.Count > 0)
            {
                if (collectionName == null)
                {
                    collectionName = _defaultCollectionName;
                }

                IMongoCollection<T> collection = Database.GetCollection<T>(collectionName);
                collection.InsertMany(model);
            }
        }

        /// <summary>
        /// Replace the specified item based on id.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName">Name of the collection.</param>
        /// <param name="model">The model.</param>
        public void Update(T model, string collectionName = null)
        {
            if (collectionName == null)
            {
                collectionName = _defaultCollectionName;
            }

            IMongoCollection<T> collection = Database.GetCollection<T>(collectionName);
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", model._id);
            collection.ReplaceOne(filter, model);
        }

        /// <summary>
        /// Updates the specified item based on a filter .
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName">Name of the collection.</param>
        /// <param name="model">The model.</param>
        public void Update(UpdateDefinition<T> model, FilterDefinition<T> filter, string collectionName = null)
        {

            if (collectionName == null)
            {
                collectionName = _defaultCollectionName;
            }
            IMongoCollection<T> collection = Database.GetCollection<T>(collectionName);
            collection.UpdateOne(filter, model);
        }



        /// <summary>
        /// Updates the specified collection name, if it has an empty ObjectId then creates it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName">Name of the collection.</param>
        /// <param name="model">The model.</param>
        public void InsertOrUpdate(T model, string collectionName = null)
        {
            if (collectionName == null)
            {
                collectionName = _defaultCollectionName;
            }

            if (GetByFilter(Builders<T>.Filter.Eq("_id", model._id))?.Count == 0)
            {
                Insert(model, collectionName);
            }
            else
            {
                Update(model, collectionName);
            }
        }

        /// <summary>
        /// Counts in the specified collection name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName">Name of the collection.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>The count</returns>
        public long Count(FilterDefinition<T> filter, string collectionName = null)
        {
            if (collectionName == null)
            {
                collectionName = _defaultCollectionName;
            }

            IMongoCollection<T> collection = Database.GetCollection<T>(collectionName);
            return collection.CountDocuments(filter);
        }

        /// <summary>
        /// Deletes all records of a specified collection.
        /// </summary>
        /// <typeparam name="T">Type of the collection</typeparam>
        /// <param name="collectionName">Name of the collection</param>
        public void DeleteAll(string collectionName = null)
        {
            if (collectionName == null)
            {
                collectionName = _defaultCollectionName;
            }

            IMongoCollection<T> collection = Database.GetCollection<T>(collectionName);
            collection.DeleteMany(FilterDefinition<T>.Empty);
        }

        /// <summary>
        /// Deletes in the specified collection name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName">Name of the collection.</param>
        /// <param name="filter">The filter.</param>
        public void Delete(FilterDefinition<T> filter, string collectionName = null)
        {
            if (collectionName == null)
            {
                collectionName = _defaultCollectionName;
            }

            IMongoCollection<T> collection = Database.GetCollection<T>(collectionName);
            collection.DeleteMany(filter);
        }

        public void Delete(T element, string collectionName = null)
        {
            Delete(Builders<T>.Filter.Eq("_id", element._id), collectionName);
        }
    }

}