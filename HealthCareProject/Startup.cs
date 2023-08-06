using HealthCareProject.Models;
using HealthCareProject.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
         o.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = true,
             ValidateAudience = false,
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,
             ValidIssuer = Configuration["JWT:issuer"],
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecretKey"]))
         });


            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("HCMconnection")));

            services.AddScoped<IRepositories<DoctorModelCLass>, DoctorRepository>();
            services.AddScoped<IGetRepositories<DoctorModelCLass>, DoctorRepository>();
            services.AddScoped<IRepositories<PatientDetails>, PatientRepositories>();
            services.AddScoped<IGetRepositories<AppointmentsModelClass>, AdminRepositories>();

            services.AddScoped<IRepositories<AppointmentsModelClass>, PatientRepositories>();
            services.AddScoped<IAdminRepository<AppointmentsModelClass>, AdminRepositories>();

            services.AddScoped<IDoctorGetRepository<AppointmentsModelClass>, DoctorRepository>();
            services.AddScoped<IDoctorRepository<PatientReport>, DoctorRepository>();
            services.AddScoped<IGetDoctorUsingRole<UserModelClass>, DoctorRepository>();
            services.AddScoped<IGetUserDEtailsUsingEmail<UserModelClass>, PatientRepositories>();
            services.AddScoped<IGetUserdetailsUsingId<UserModelClass>, DoctorRepository>();
            services.AddScoped<IPatientGetRepository<PatientReport>, PatientRepositories>();
            services.AddScoped<IGetRepositories<UserModelClass>, AdminRepositories>();
            services.AddScoped<IGetDoctorDetailsUsingUserId<DoctorModelCLass>, DoctorRepository>();
            services.AddScoped<IGetRepositories<PatientDetails>, PatientRepositories>();
            services.AddScoped<IGetDoctorDetailsUsingUserId<DoctorModelCLass>, DoctorRepository>();
            services.AddScoped<IGetPatientsAllReports<PatientReport>, PatientRepositories>();
            services.AddScoped<IGetMyAppointments<AppointmentsModelClass>, PatientRepositories>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HealthCareProject", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HealthCareProject v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
