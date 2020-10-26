using istek.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace istek.Actions
{
    public class ActorsManager
    {

        public static MongoClient _mongoClient = new MongoClient();
        public static string dbName = "MovieData";
        public static IMongoDatabase db = _mongoClient.GetDatabase(dbName);

        public static async Task<ActorsAddOutput>ActorsAdd(Actors Actors)
        {
            try
            {



                if (Actors.ActorName == "")
                {
                    return new ActorsAddOutput() { Type = 0, Message = "Kayıt Ekleme Sırasında Bir Hata Meydana Geldi.." };
                }
                Actors.ActorID = Guid.NewGuid().ToString();
                await db.GetCollection<Actors>("Actors").InsertOneAsync(Actors);
            }

            catch (Exception ex)
            {
                return new ActorsAddOutput() { Type = 0, Message = "Kayıt Ekleme Sırasında bir hata gerçekleşti.. Hata: " + ex.Message };

            }
            return new ActorsAddOutput() { Type = 1, Message = "Kayıt Başarıyla eklendi. " };
        }
        public static async Task<List<Actors>> GetActors(GetActorInput Parameters)
        {
            var List = new List<Actors>();
            var projection = Builders<Actors>.Projection;
            var project = projection.Exclude("_id");

            var filter = FilterDefinition<Actors>.Empty;

            BsonValue val;
            if (Parameters.ColumnType == "int")
            {
                val = Convert.ToInt64(Parameters.Value);
            }
            else if (Parameters.ColumnType == "bool")
            {
                val = Convert.ToBoolean(Parameters.Value);
            }
            else
            {
                val = Convert.ToString(Parameters.Value);
            }

            if (Parameters.Statement == "=")
            {
                filter = Builders<Actors>.Filter.Eq(Parameters.Column, val);
            }
            else if (Parameters.Statement == "<")
            {
                filter = Builders<Actors>.Filter.Lt(Parameters.Column, val);
            }
            else if (Parameters.Statement == ">")
            {
                filter = Builders<Actors>.Filter.Gt(Parameters.Column, val);
            }


            var option = new FindOptions<Actors, BsonDocument> { Projection = project };
            using (var cursor = await db.GetCollection<Actors>("Actors").FindAsync(filter, option))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (BsonDocument s in batch)
                    {
                        var Actors = new Actors();
                        if (s.Contains("ActorID"))
                        {
                            Actors.ActorID = s["ActorID"].AsString;
                        }

                        if (s.Contains("ActorName"))
                        {
                            Actors.ActorName = s["ActorName"].AsString;
                        }

                        if (s.Contains("Actorİmage"))
                        {
                            Actors.Actorİmage = s["Actorİmage"].AsString;
                        }
                        

                        List.Add(Actors);
                    }

                }

            }
            return List;
        }
        public static async Task<ActorDeleteOutput> DeleteActors(ActorDeleteInput Parameters)
        {
            ActorDeleteOutput output = new ActorDeleteOutput()
            {
                Type = 1,
                Message = "Kayıt Başarıyla Silindi."
            };
            var filter = Builders<Actors>.Filter.Eq(Parameters.FilterCol, Parameters.FilterVal);
            var result = await db.GetCollection<Actors>("Actors").DeleteManyAsync(filter);

            if (result.DeletedCount == 0)
            {
                output.Type = 0;
                output.Message = "Kayıt silme sırasında hata oluştu";
            }

            return output;
        }
        public static async Task<ActorUpdateOutput> UptadeActor(ActorUptadeInput Parameters)
        {
            ActorUpdateOutput output = new ActorUpdateOutput()
            {
                Type = 0,
                Message = "Kayıt Güncellemede Hata Oluştu."
            };
            var filter = Builders<Actors>.Filter.Eq(Parameters.FilterCol, Parameters.FilterVal);
            var update = Builders<Actors>.Update
                .Set(x => x.ActorName, Parameters.ActorName)
                .Set(x => x.Actorİmage, Parameters.Actorİmage);
                
            var projection = Builders<Actors>.Projection;
            var project = projection.Exclude("_id");
            var result = await db.GetCollection<Actors>("Actors").UpdateOneAsync(filter, update);

            if (result.ModifiedCount > 0)
            {
                output.Type = 1;
                output.Message = result.ModifiedCount + " Kayıt başarıyla güncellendi.";
            }
           
            return output;
        }

    }
}