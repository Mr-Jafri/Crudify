
namespace Crudify.Api.Extensions
{
    public static class Injections
    {
        public static void AddDependencyInjections(this IServiceCollection services, IConfiguration config)
        {
            var jwtSettings = config.GetSection("JwtConfig").Get<JwtSettings>();
            services.Configure<JwtSettings>(config.GetSection("JwtConfig"));

            var key = Encoding.ASCII.GetBytes(config["JwtConfig:SecretKey"]);

            TokenValidationParameters? tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                RequireExpirationTime = false
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {

                jwt.SaveToken = true;
                jwt.TokenValidationParameters = tokenValidationParams;
            });

            services.AddSingleton(tokenValidationParams);

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

            services.AddTransient<IApplicationContext, ApplicationContext>();
            services.AddTransient<ILoggingService, LoggingService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IStudentsService, StudentsService>();
        }
    }
}
