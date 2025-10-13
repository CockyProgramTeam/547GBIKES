using dirtbike.api.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using weathertest;

namespace Enterpriseservices
{
    public interface IApilogService
    {
        IEnumerable<Apilog> GetAll();
        IEnumerable<Apilog> GetById(int id);
        Task<IResult> Create(Apilog input);
        Task<IResult> Update(int id, Apilog input);
        Task<IResult> Delete(int id);
    }

    public interface IParkService
    {
        IEnumerable<Park> GetAll();
        IEnumerable<Park> GetById(int id);
        Task<IResult> Create(Park input);
        Task<IResult> Update(int id, Park input);
        Task<IResult> Delete(int id);
    }

    public interface ITemplateService
    {
        IEnumerable<Template> GetAll();
        IEnumerable<Template> GetById(int id);
        Task<IResult> Create(Template input);
        Task<IResult> Update(int id, Template input);
        Task<IResult> Delete(int id);
    }

    public interface IUserlogService
    {
        IEnumerable<Userlog> GetAll();
        IEnumerable<Userlog> GetById(int id);
        Task<IResult> Create(Userlog input);
        Task<IResult> Update(int id, Userlog input);
        Task<IResult> Delete(int id);
    }

    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> GetForecast();
    }
}
