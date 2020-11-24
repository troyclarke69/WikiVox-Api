using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;
using Wikivox_Api.Entities;
using Wikivox_Api.Helpers;
using Wikivox_Api.Models;
using Wikivox_Api.Services;

namespace Wikivox_Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public void ConfigureServices(IServiceCollection services)
        {
            // in memory database used for this demo...
            // user: test, pwd: test
            services.AddDbContext<DataContext>(x => x.UseInMemoryDatabase("TestDb"));

            //services.AddCors();
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder
                        .WithOrigins(
                            "http://localhost:63881",
                            "http://localhost:5000",
                            "http://localhost:3000",
                            "https://troyclarke.tk",
                            "https://ngorder-dev.netlify.app",
                            "https://wikivox-dev.netlify.app")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithExposedHeaders("X-Pagination");

                    });
            });

            services.Configure<Wikivox_ApiDatabaseSettings>(
               Configuration.GetSection(nameof(Wikivox_ApiDatabaseSettings)));

            services.AddSingleton<IWikivox_ApiDatabaseSettings>(provider =>
                provider.GetRequiredService<IOptions<Wikivox_ApiDatabaseSettings>>().Value);

            services.AddScoped<ArtistService>();
            services.AddScoped<ArtistAlbumService>();
            services.AddScoped<ArtistImageService>();
            services.AddScoped<GenreService>();
            services.AddScoped<AlbumService>();
            services.AddScoped<SongService>();
            services.AddScoped<MusicianService>();
            services.AddScoped<ImageService>();
            services.AddScoped<EntityService>();
            services.AddScoped<InstrumentService>();
            services.AddScoped<AlbumSongService>();
            services.AddScoped<ArtistGenreService>();
            services.AddScoped<ArtistMusicianService>();
            services.AddScoped<ArtistSongService>();
            services.AddScoped<MusicianInstrumentService>();


            // copd-api injection
            services.Configure<Copd_ApiDatabaseSettings>(
              Configuration.GetSection(nameof(Copd_ApiDatabaseSettings)));

            services.AddSingleton<ICopd_ApiDatabaseSettings>(provider =>
                provider.GetRequiredService<IOptions<Copd_ApiDatabaseSettings>>().Value);

            services.AddScoped<CustomerService>();
            services.AddScoped<ProductService>();
            services.AddScoped<OrderService>();
            services.AddScoped<OrderDetailService>();


            // projects-api injection
            services.Configure<Projects_ApiDatabaseSettings>(
              Configuration.GetSection(nameof(Projects_ApiDatabaseSettings)));

            services.AddSingleton<IProjects_ApiDatabaseSettings>(provider =>
                provider.GetRequiredService<IOptions<Projects_ApiDatabaseSettings>>().Value);

            services.AddScoped<ProjectsService>();
            services.AddScoped<ContactService>();



            // blog-api injection
            services.Configure<Blog_ApiDatabaseSettings>(
              Configuration.GetSection(nameof(Blog_ApiDatabaseSettings)));

            services.AddSingleton<IBlog_ApiDatabaseSettings>(provider =>
                provider.GetRequiredService<IOptions<Blog_ApiDatabaseSettings>>().Value);

            services.AddScoped<BlogService>();


            //services.AddControllers();

            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.IgnoreNullValues = true);

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                };
            });

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();



            // DTO implementation

            services.AddControllers().AddNewtonsoftJson(s => {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // add hardcoded test user to db on startup
            // plain text password is used for simplicity, hashed passwords should be used in production applications
            context.Users.Add(new User { FirstName = "Test", LastName = "User", Username = "test", Password = "test" });
            context.SaveChanges();

            app.UseHttpsRedirection();

            app.UseRouting();

            //The call to UseCors must be placed after UseRouting, but before UseAuthorization
            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
