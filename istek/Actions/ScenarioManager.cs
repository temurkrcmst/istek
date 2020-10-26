using MongoDB.Bson;
using MongoDB.Driver;
using istek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace istek.Actions
{
    public class ScenarioManager
    {
        public static MongoClient _mongoClient = new MongoClient();
        public static string dbName = "MovieData";
        public static IMongoDatabase db = _mongoClient.GetDatabase(dbName);
        
        public static async Task<ScenarioAddOutput> ScenarioAdd(Scenario Scenario)
        {
            try
            {
                if (Scenario.ScenarioName == "")
                {
                    return new ScenarioAddOutput() { Type = 0, Message = "Ekleme Sırasında Hata oluştu" };
                }
                Scenario.ScenarioID = Guid.NewGuid().ToString();
                await db.GetCollection<Scenario>("Scenario").InsertOneAsync(Scenario);
            }
            catch (Exception ex)
            {
                return new ScenarioAddOutput() { Type = 0, Message = "Kayıt Ekleme Sırasında bir hata gerçekleşti.. Hata: " + ex.Message };
                throw;
            }
            return new ScenarioAddOutput() { Type = 1, Message = "Kayıt Başarıyla eklendi. " };
        }
        public static async Task<ScenarioDeleteOutput> DeleteScenario(ScenarioDeleteInput Parameters)
        {
            ScenarioDeleteOutput output = new ScenarioDeleteOutput()
            {
                Type = 1,
                Message = "Kayıt Başarıyla Silindi."
            };
            var filter = Builders<Scenario>.Filter.Eq(Parameters.FilterCol, Parameters.FilterVal);
            var result = await db.GetCollection<Scenario>("Scenario").DeleteManyAsync(filter);

            if (result.DeletedCount == 0)
            {
                output.Type = 0;
                output.Message = "Kayıt silme sırasında hata oluştu";
            }

            return output;
        }
        public static async Task<List<Scenario>> GetScenario(GetScenarioInput Parameters)
        {
            var List = new List<Scenario>();
            var projection = Builders<Scenario>.Projection;
            var project = projection.Exclude("_id");

            var filter = FilterDefinition<Scenario>.Empty;

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
                filter = Builders<Scenario>.Filter.Eq(Parameters.Column, val);
            }
            else if (Parameters.Statement == "<")
            {
                filter = Builders<Scenario>.Filter.Lt(Parameters.Column, val);
            }
            else if (Parameters.Statement == ">")
            {
                filter = Builders<Scenario>.Filter.Gt(Parameters.Column, val);
            }
            var option = new FindOptions<Scenario, BsonDocument> { Projection = project };
            using (var cursor = await db.GetCollection<Scenario>("Scenario").FindAsync(filter, option))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (BsonDocument s in batch)
                    {
                        var Scenario = new Scenario();
                        if (s.Contains("ScenarioID"))
                        {
                            Scenario.ScenarioID = s["ScenarioID"].AsString;
                        }

                        if (s.Contains("ScenarioName"))
                        {
                            Scenario.ScenarioName = s["ScenarioName"].AsString;
                        }

                        if (s.Contains("Scenarioİmage"))
                        {
                            Scenario.Scenarioİmage = s["Scenarioİmage"].AsString;
                        }

                       

                        List.Add(Scenario);
                    }

                }

            }
            return List;
        }
        public static async Task<ScenarioUptadeOutput> UptadeScenario(ScenarioUptadeInput Parameters)
        {
            ScenarioUptadeOutput output = new ScenarioUptadeOutput()
            {
                Type = 0,
                Message = "Kayıt Güncellemede Hata Oluştu."
            };
            var filter = Builders<Scenario>.Filter.Eq(Parameters.FilterCol, Parameters.FilterVal);

            var update = Builders<Scenario>.Update
                .Set(x => x.ScenarioName, Parameters.ScenarioName)
                .Set(x => x.Scenarioİmage, Parameters.Scenarioİmage);

            var projection = Builders<Scenario>.Projection;
            var project = projection.Exclude("_id");
            var result = await db.GetCollection<Scenario>("Scenario").UpdateOneAsync(filter, update);

            if (result.ModifiedCount > 0)
            {
                output.Type = 1;
                output.Message = result.ModifiedCount + " Kayıt başarıyla güncellendi.";
            }

            return output;
        }

    }
}