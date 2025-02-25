using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using The_Engneering.Authentication;
using The_Engneering.Persistence;
using The_Engneering.Services;

namespace The_Engneering;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddControllers();
        services.AddOpenApi();

        #region Generate DB
        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("this connection string is invalid");

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString)); 
        #endregion
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IBillService, BillService>();

        services.AddFluentValidationServices();
        services.AddAuthServices();
        //services.AddSignalR();
        return services;
    }
    public static IServiceCollection AddFluentValidationServices(this IServiceCollection services) 
    {
        services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }

    public static IServiceCollection AddAuthServices(this IServiceCollection services)
    {
        services.AddSingleton<IJwtProvider, JwtProvider>();
        services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

        //Add authentication configurations
        services.AddAuthentication(options =>
        {
            // Bearer عشان يعرف دايما اننا بنستخدم مع التوكين بتاعنا ال 
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("O1YjDQOryMIeS1yMtUfobV61DcH5QmoR")),
                ValidIssuer = "EngineeringApp",
                ValidAudience = "EngineeringApp Users"
            };
        });
        return services;
    }

}
