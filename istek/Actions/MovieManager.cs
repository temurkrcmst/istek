using istek.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;


namespace istek.Actions
{
    public class MovieManager
    {

        public static MongoClient _mongoClient = new MongoClient();
        public static string dbName = "MovieData";
        public static IMongoDatabase db = _mongoClient.GetDatabase(dbName);

        

        public static async Task<AddUserOutput> AddMovie(Movie Movie)
        {

            try
            {
                if (Movie.MovieName == "") {
                    return new AddUserOutput() { Type = 0, Message = "Kayıt Ekleme Sırasında Bir Hata Meydana Geldi.." };
                }
                Movie.MovieID = Guid.NewGuid().ToString();
                await db.GetCollection<Movie>("Movie").InsertOneAsync(Movie);
            }
            
            catch (Exception ex)
            {
                return new AddUserOutput() { Type = 0, Message = "Kayıt Ekleme Sırasında bir hata gerçekleşti.. Hata: "+ex.Message };
                
            }
        return new AddUserOutput() { Type = 1, Message="Kayıt Başarıyla eklendi. " };
        }

        
        public static async Task<MovieDeleteOutput> DeleteMovie(MovieDeleteInput Parameters)
        {
            MovieDeleteOutput output = new MovieDeleteOutput()
            {
                Type = 1,
                Message = "Kayıt Başarıyla Silindi."
            };
            var filter = Builders<Movie>.Filter.Eq(Parameters.FilterCol, Parameters.FilterVal);
            var result = await db.GetCollection<Movie>("Movie").DeleteManyAsync(filter);

            if (result.DeletedCount == 0)
            {
                output.Type = 0;
                output.Message = "Kayıt silme sırasında hata oluştu";
            }

            return output;

        }

        public static async Task<MovieUptadeOutput> UptadeMovie(MovieUptadeInput  Parameters)
        {
            MovieUptadeOutput output = new MovieUptadeOutput() { 
            Type = 0,
            Message="Kayıt Güncellemede Hata Oluştu."
            };

            var filter = Builders<Movie>.Filter.Eq(Parameters.FilterCol, Parameters.FilterVal);

            var update = Builders<Movie>.Update
                .Set(x => x.MovieName, Parameters.MovieName)
                .Set(x => x.MovieType, Parameters.MovieType)
                .Set(x => x.Time, Parameters.Time); ;
           var projection = Builders<Movie>.Projection;
            var project = projection.Exclude("_id");

            var result = await db.GetCollection<Movie>("Movie").UpdateOneAsync(filter, update);

            if (result.ModifiedCount > 0)
            {
                output.Type = 1;
                output.Message = result.ModifiedCount + " Kayıt başarıyla güncellendi.";
            }
            return output;

        }
        public static async Task<List<Movie>> GetMovie(GetMovieInput Parameters)
        {
            var List = new List<Movie>();
            var projection = Builders<Movie>.Projection;
            var project = projection.Exclude("_id");

            var filter = FilterDefinition<Movie>.Empty;

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
                filter = Builders<Movie>.Filter.Eq(Parameters.Column, val);
            }
            else if (Parameters.Statement == "<")
            {
                filter = Builders<Movie>.Filter.Lt(Parameters.Column, val);
            }
            else if (Parameters.Statement == ">")
            {
                filter = Builders<Movie>.Filter.Gt(Parameters.Column, val);
            }

            var option = new FindOptions<Movie, BsonDocument> { Projection = project };
            using (var cursor = await db.GetCollection<Movie>("Movie").FindAsync(filter, option))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (BsonDocument s in batch)
                    {
                        var movie = new Movie();
                        if (s.Contains("MovieID"))
                        {
                            movie.MovieID = s["MovieID"].AsString;
                        }

                        if (s.Contains("MovieName"))
                        {
                            movie.MovieName = s["MovieName"].AsString;
                        }

                        if (s.Contains("Actors"))
                        {
                            movie.Actors = s["Actors"].AsString;
                        }

                        if (s.Contains("PublishedDate"))
                        {
                            movie.PublishedDate = s["PublishedDate"].AsString;
                        }
                        if (s.Contains("Country"))
                        {
                            movie.Country = s["Country"].AsString;
                        }
                        if (s.Contains("Company"))
                        {
                            movie.Company = s["Company"].AsString;
                        }
                        if (s.Contains("Scenario"))
                        {
                            movie.Scenario = s["Scenario"].AsString;
                        }
                        if (s.Contains("MovieType"))
                        {
                            movie.MovieType = s["MovieType"].AsString;
                        }
                        if (s.Contains("Time"))
                        {
                            movie.Time = s["Time"].AsDouble;
                        }
                        if (s.Contains("IMDB"))
                        {
                            movie.IMDB = s["IMDB"].AsDouble;
                        }

                        List.Add(movie);
                    }

                }

            }
            return List;
        }
    }
}