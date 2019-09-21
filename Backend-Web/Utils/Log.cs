using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace Backend_Web.Utils
{
    public class Log : BaseMongoModel
    {
        public string Message { get; set; }

        public Exception Exception { get; set; }

        public Level Level { get; set; }
    }

    public enum Level
    {
        [Description("Debug")]
        Debug,
        [Description("Warning")]
        Warning,
        [Description("Error")]
        Error
    }

    public class BaseMongoModel : MarshalByRefObject
    {
        [BsonId]
        [NotMapped]
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId _id { get; set; }
    }

    public class ObjectIdConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return new ObjectId(serializer.Deserialize<string>(reader));
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(ObjectId).IsAssignableFrom(objectType);
        }
    }

    public class Logger : MongoDAO<Log>
    {
        public Logger(string databaseName, string mongoPath, string collectionName) : base(databaseName, mongoPath, collectionName) { }

        public void Error(string message)
        {
            Insert(new Log { Message = message, Level = Level.Error });
        }

        public void Error(Exception ex, string message)
        {
            Insert(new Log { Message = message, Level = Level.Error, Exception = ex });
        }
        public void Warn(Exception ex, string message)
        {
            Insert(new Log { Message = message, Level = Level.Warning, Exception = ex });
        }
        public void Warn(string message)
        {
            Insert(new Log { Message = message, Level = Level.Warning });
        }
        public void Debug(string message)
        {
            Insert(new Log { Message = message });
        }

    }

    public static class LoggerManager
    {
        public static Logger GetDefaultLogger(string collectionName)
        {
            return new Logger("ProjetoWeb", ConfigurationManager.AppSettings["MongoConnection"], collectionName);
        }
    }
}