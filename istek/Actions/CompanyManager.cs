using istek.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace istek.Actions
{
    public class CompanyManager
    {
        public static MongoClient _mongoClient = new MongoClient();
        public static string dbName = "MovieData";
        public static IMongoDatabase db = _mongoClient.GetDatabase(dbName);
        
        public static async Task<CompanyAddOutput> CompanyAdd(Company Company)
        {
            try
            {
                if (Company.CompanyName == "")
                {
                    return new CompanyAddOutput() { Type = 0, Message = "Ekleme Sırasında Hata oluştu" };
                }
                Company.CompanyID = Guid.NewGuid().ToString();
                await db.GetCollection<Company>("Company").InsertOneAsync(Company);
            }
            catch (Exception ex)
            {
                return new CompanyAddOutput() { Type = 0, Message = "Kayıt Ekleme Sırasında bir hata gerçekleşti.. Hata: " + ex.Message };
                throw;
            }
            return new CompanyAddOutput() { Type = 1, Message = "Kayıt Başarıyla eklendi. " };
        }
        public static async Task<CompanyDeleteOutput> DeleteCompany(CompanyDeleteInput Parameters)
        {
            CompanyDeleteOutput output = new CompanyDeleteOutput()
            {
                Type = 1,
                Message = "Kayıt Başarıyla Silindi."
            };
            var filter = Builders<Company>.Filter.Eq(Parameters.FilterCol, Parameters.FilterVal);
            var result = await db.GetCollection<Company>("Company").DeleteOneAsync(filter);

            if (result.DeletedCount == 0)
            {
                output.Type = 0;
                output.Message = "Kayıt silme sırasında hata oluştu";
            }

            
            return output;
        }
        public static async Task<List<Company>> GetCompany(GetCompanyInput Parameters)
        {
            var List = new List<Company>();
            var projection = Builders<Company>.Projection;
            var project = projection.Exclude("_id");

            var filter = FilterDefinition<Company>.Empty;

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
                filter = Builders<Company>.Filter.Eq(Parameters.Column, val);
            }
            else if (Parameters.Statement == "<")
            {
                filter = Builders<Company>.Filter.Lt(Parameters.Column, val);
            }
            else if (Parameters.Statement == ">")
            {
                filter = Builders<Company>.Filter.Gt(Parameters.Column, val);
            }
            var option = new FindOptions<Company, BsonDocument> { Projection = project };
            using (var cursor = await db.GetCollection<Company>("Company").FindAsync(filter, option))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (BsonDocument s in batch)
                    {
                        var Company = new Company();
                        if (s.Contains("CompanyID"))
                        {
                            Company.CompanyID = s["CompanyID"].AsString;
                        }

                        if (s.Contains("CompanyName"))
                        {
                            Company.CompanyName = s["CompanyName"].AsString;
                        }
                        List.Add(Company);
                    }

                }

            }
            return List;
        }
        public static async Task<CompanyUptadeOutput> UptadeCompany(CompanyUptadeInput Parameters)
        {
            CompanyUptadeOutput output = new CompanyUptadeOutput()
            {
                Type = 0,
                Message = "Kayıt Güncellemede Hata Oluştu."
            };
            var filter = Builders<Company>.Filter.Eq(Parameters.FilterCol, Parameters.FilterVal);

            var update = Builders<Company>.Update
                .Set(x => x.CompanyName, Parameters.CompanyName);
                

            var projection = Builders<Company>.Projection;
            var project = projection.Exclude("_id");
            var result = await db.GetCollection<Company>("Company").UpdateManyAsync(filter, update);

            if (result.ModifiedCount > 0)
            {
                output.Type = 1;
                output.Message = result.ModifiedCount + " Kayıt başarıyla güncellendi.";
            }

            return output;
        }


    }
}