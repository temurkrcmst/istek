using istek.Actions;
using istek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace istek.Controllers
{
    public class MovieController : ApiController
    {
        [HttpPost]
        public async Task<AddUserOutput> AddMovie(Movie movie)
        {
            return await MovieManager.AddMovie(movie);
        }
        [HttpPost]
        public async Task<MovieDeleteOutput> DeleteMovie(MovieDeleteInput Parameters)
        {
            return await MovieManager.DeleteMovie(Parameters);
        }
        [HttpPost]
        public async Task<MovieUptadeOutput> UptadeMovie(MovieUptadeInput Parameters){

            return await MovieManager.UptadeMovie(Parameters);
        }
        [HttpPost]
        public async Task<List<Movie>> GetMovie(GetMovieInput Parameters)
        {
            return await MovieManager.GetMovie(Parameters);
        }



        [HttpPost]
        public async Task<ActorsAddOutput> ActorsAdd(Actors Actors)
        {
            return await ActorsManager.ActorsAdd(Actors);
        }
        [HttpPost]
        public async Task<ActorDeleteOutput> DeleteActor(ActorDeleteInput Parameters)
        {
            return await ActorsManager.DeleteActors(Parameters);
        }
        [HttpPost]
        public async Task<ActorUpdateOutput> UptadeActor(ActorUptadeInput Parameters)
        {

            return await ActorsManager.UptadeActor(Parameters);
        }
        [HttpPost]
        public async Task<List<Actors>> GetActors(GetActorInput Parameters)
        {
            return await ActorsManager.GetActors(Parameters);
        }







        
        [HttpPost]
        public async Task<ScenarioAddOutput> ScenarioAdd(Scenario Scenario)
        {
            return await ScenarioManager.ScenarioAdd(Scenario);
        }
        [HttpPost]
        public async Task<ScenarioDeleteOutput> DeleteScenario(ScenarioDeleteInput Parameters)
        {
            return await ScenarioManager.DeleteScenario(Parameters);
        }
        [HttpPost]
        public async Task<ScenarioUptadeOutput> UptadeScenario(ScenarioUptadeInput Parameters)
        {

            return await ScenarioManager.UptadeScenario(Parameters);
        }
        [HttpPost]
        public async Task<List<Scenario>> GetScenario(GetScenarioInput Parameters)
        {
            return await ScenarioManager.GetScenario(Parameters);
        }





        [HttpPost]
        public async Task<CompanyAddOutput> CompanyAdd(Company Company)
        {
            return await CompanyManager.CompanyAdd(Company);
        }
        [HttpPost]
        public async Task<CompanyDeleteOutput> DeleteCompany(CompanyDeleteInput Parameters)
        {
            return await CompanyManager.DeleteCompany(Parameters);
        }
        [HttpPost]
        public async Task<CompanyUptadeOutput> UptadeCompany(CompanyUptadeInput Parameters)
        {

            return await CompanyManager.UptadeCompany(Parameters);
        }
        [HttpPost]
        public async Task<List<Company>> GetCompany(GetCompanyInput Parameters)
        {
            return await CompanyManager.GetCompany(Parameters);
        }

    }
}