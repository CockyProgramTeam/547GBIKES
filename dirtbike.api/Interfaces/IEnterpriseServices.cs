using dirtbike.api.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using weathertest;

namespace Enterpriseservices
{
    // Existing ones
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
        IEnumerable<Parks> GetAll();
        IEnumerable<Parks> GetById(int id);
        Task<IResult> Create(Parks input);
        Task<IResult> Update(int id, Parks input);
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

    // Additional ones based on your file list
    public interface IAdminLogsService
    {
        IEnumerable<AdminLogs> GetAll();
        IEnumerable<AdminLogs> GetById(int id);
        Task<IResult> Create(AdminLogs input);
        Task<IResult> Update(int id, AdminLogs input);
        Task<IResult> Delete(int id);
    }

    public interface IBatchService
    {
        IEnumerable<Batch> GetAll();
        IEnumerable<Batch> GetById(int id);
        Task<IResult> Create(Batch input);
        Task<IResult> Update(int id, Batch input);
        Task<IResult> Delete(int id);
    }

    public interface IBatchtypeService
    {
        IEnumerable<Batchtype> GetAll();
        IEnumerable<Batchtype> GetById(int id);
        Task<IResult> Create(Batchtype input);
        Task<IResult> Update(int id, Batchtype input);
        Task<IResult> Delete(int id);
    }

    public interface IBookingService
    {
        IEnumerable<Booking> GetAll();
        IEnumerable<Booking> GetById(int id);
        Task<IResult> Create(Booking input);
        Task<IResult> Update(int id, Booking input);
        Task<IResult> Delete(int id);
    }

    public interface ICardsService
    {
        IEnumerable<Cards> GetAll();
        IEnumerable<Cards> GetById(int id);
        Task<IResult> Create(Cards input);
        Task<IResult> Update(int id, Cards input);
        Task<IResult> Delete(int id);
    }

    public interface ICartService
    {
        IEnumerable<Cart> GetAll();
        IEnumerable<Cart> GetById(int id);
        Task<IResult> Create(Cart input);
        Task<IResult> Update(int id, Cart input);
        Task<IResult> Delete(int id);
    }

    public interface ICartItemService
    {
        IEnumerable<CartItem> GetAll();
        IEnumerable<CartItem> GetById(int id);
        Task<IResult> Create(CartItem input);
        Task<IResult> Update(int id, CartItem input);
        Task<IResult> Delete(int id);
    }

    public interface ICartMasterService
    {
        IEnumerable<CartMaster> GetAll();
        IEnumerable<CartMaster> GetById(int id);
        Task<IResult> Create(CartMaster input);
        Task<IResult> Update(int id, CartMaster input);
        Task<IResult> Delete(int id);
    }

    public interface ICompanyService
    {
        IEnumerable<Company> GetAll();
        IEnumerable<Company> GetById(int id);
        Task<IResult> Create(Company input);
        Task<IResult> Update(int id, Company input);
        Task<IResult> Delete(int id);
    }

    public interface ICustomerService
    {
        IEnumerable<Customer> GetAll();
        IEnumerable<Customer> GetById(int id);
        Task<IResult> Create(Customer input);
        Task<IResult> Update(int id, Customer input);
        Task<IResult> Delete(int id);
    }

    public interface IEmailgatewayService
    {
        IEnumerable<Emailgateway> GetAll();
        IEnumerable<Emailgateway> GetById(int id);
        Task<IResult> Create(Emailgateway input);
        Task<IResult> Update(int id, Emailgateway input);
        Task<IResult> Delete(int id);
    }

    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAll();
        IEnumerable<Employee> GetById(int id);
        Task<IResult> Create(Employee input);
        Task<IResult> Update(int id, Employee input);
        Task<IResult> Delete(int id);
    }

    public interface IFileService
    {
        IEnumerable<File> GetAll();
        IEnumerable<File> GetById(int id);
        Task<IResult> Create(File input);
        Task<IResult> Update(int id, File input);
        Task<IResult> Delete(int id);
    }

    public interface ILearnDetailService
    {
        IEnumerable<LearnDetail> GetAll();
        IEnumerable<LearnDetail> GetById(int id);
        Task<IResult> Create(LearnDetail input);
        Task<IResult> Update(int id, LearnDetail input);
        Task<IResult> Delete(int id);
    }

    public interface IParkreviewsService
    {
        IEnumerable<Parkreviews> GetAll();
        IEnumerable<Parkreviews> GetById(int id);
        Task<IResult> Create(Parkreviews input);
        Task<IResult> Update(int id, Parkreviews input);
        Task<IResult> Delete(int id);
    }

    public interface IPaymentsService
    {
        IEnumerable<Payments> GetAll();
        IEnumerable<Payments> GetById(int id);
        Task<IResult> Create(Payments input);
        Task<IResult> Update(int id, Payments input);
        Task<IResult> Delete(int id);
    }

    public interface ISalesCatalogueService
    {
        IEnumerable<SalesCatalogue> GetAll();
        IEnumerable<SalesCatalogue> GetById(int id);
        Task<IResult> Create(SalesCatalogue input);
        Task<IResult> Update(int id, SalesCatalogue input);
        Task<IResult> Delete(int id);
    }

    public interface ISiteService
    {
        IEnumerable<Site> GetAll();
        IEnumerable<Site> GetById(int id);
        Task<IResult> Create(Site input);
        Task<IResult> Update(int id, Site input);
        Task<IResult> Delete(int id);
    }

    public interface ISuperuserLogsService
    {
        IEnumerable<SuperuserLogs> GetAll();
        IEnumerable<SuperuserLogs> GetById(int id);
        Task<IResult> Create(SuperuserLogs input);
        Task<IResult> Update(int id, SuperuserLogs input);
        Task<IResult> Delete(int id);
    }

    public interface ITechsService
    {
        IEnumerable<Techs> GetAll();
        IEnumerable<Techs> GetById(int id);
        Task<IResult> Create(Techs input);
        Task<IResult> Update(int id, Techs input);
        Task<IResult> Delete(int id);
    }

    public interface IUserService
    {
        IEnumerable<User> GetAll();
        IEnumerable<User> GetById(int id);
        Task<IResult> Create(User input);
        Task<IResult> Update(int id, User input);
        Task<IResult> Delete(int id);
    }

    public interface IUsergroupsService
    {
        IEnumerable<Usergroups> GetAll();
        IEnumerable<Usergroups> GetById(int id);
        Task<IResult> Create(Usergroups input);
        Task<IResult> Update(int id, Usergroups input);
        Task<IResult> Delete(int id);
    }

    public interface IUserhelpService
    {
        IEnumerable<Userhelp> GetAll();
        IEnumerable<Userhelp> GetById(int id);
        Task<IResult> Create(Userhelp input);
        Task<IResult> Update(int id, Userhelp input);
        Task<IResult> Delete(int id);
    }

    public interface IUserprofileService
    {
        IEnumerable<Userprofile> GetAll();
        IEnumerable<Userprofile> GetById(int id);
        Task<IResult> Create(Userprofile input);
        Task<IResult> Update(int id, Userprofile input);
        Task<IResult> Delete(int id);
    }

    public interface IUsersessionService
    {
        IEnumerable<Usersession> GetAll();
        IEnumerable<Usersession> GetById(int id);
        Task<IResult> Create(Usersession input);
        Task<IResult> Update(int id, Usersession input);
        Task<IResult> Delete(int id);
    }
}
