using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using webapi.Data.Databases;
using webapi.Helpers;
using webapi.Helpers.Constants;
using webapi.Helpers.Enums;
using webapi.Helpers.Interfaces;
using webapi.Models.Accounts;
using webapi.Services;
using webapi.Services.database;
using webapi.Services.userresolvers;
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;
using webapi.Hubs;

var builder = WebApplication.CreateBuilder(args);

string googleClientId = string.Empty;
string googleClientSecret = string.Empty;

if (builder.Environment.IsDevelopment()) {
  // When running in Development environment, this will read from the secrets
  // file
  googleClientId = builder.Configuration["Authentication:Google:ClientId"];
  googleClientSecret =
      builder.Configuration["Authentication:Google:ClientSecret"];
} else {
  // When in production, this will read from the Azure Key Vault
  // getting azure key vault url from configuration
  var kvUri = builder.Configuration["KeyVaultConfig:url"];
  var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

  // Replace "mysecret" with the name of your secret
  KeyVaultSecret googleClientIdKeyVault =
      client.GetSecret("Authentication--Google--ClientId");
  KeyVaultSecret googleClientSecretKeyVault =
      client.GetSecret("Authentication--Google--ClientSecret");
  googleClientId = googleClientIdKeyVault.Value;
  googleClientSecret = googleClientSecretKeyVault.Value;
}

// Adding database context and configuring connection string
string? disabilityGuardianConnectionString =
    builder.Configuration.GetConnectionString(
        "DisabilityUsers_Guardian_ConnectionString");
string? companyResearchConnectionString =
    builder.Configuration.GetConnectionString(
        "Company_Research_ConnectionString");
string? stichtingAccessibilityEmployeesConnectionString =
    builder.Configuration.GetConnectionString(
        "StichtingAccessibilityEmployees_ConnectionString");
string? chatConnectionString =
    builder.Configuration.GetConnectionString("Chat_ConnectionString");

builder.Services.AddDbContext<DisabilityExpertGuardiansContext>(
    options => options.UseMySql(
        disabilityGuardianConnectionString,
        ServerVersion.AutoDetect(disabilityGuardianConnectionString)));
builder.Services.AddDbContext<CompanyResearchContext>(
    options => options.UseMySql(
        companyResearchConnectionString,
        ServerVersion.AutoDetect(companyResearchConnectionString)));
builder.Services.AddDbContext<EmployeeContext>(
    options =>
        options.UseMySql(stichtingAccessibilityEmployeesConnectionString,
                         ServerVersion.AutoDetect(
                             stichtingAccessibilityEmployeesConnectionString)));

// Adding the context factory, used to get the correct context for the user type
builder.Services.AddScoped<IIdbContextFactory, IdbContextFactory>();

// adding identities
builder.Services.AddIdentityCore<DisabilityExpert>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DisabilityExpertGuardiansContext>()
    .AddDefaultTokenProviders()
    .AddUserManager<UserManager<DisabilityExpert>>();
builder.Services.AddIdentityCore<Company>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<CompanyResearchContext>()
    .AddDefaultTokenProviders()
    .AddUserManager<UserManager<Company>>();
builder.Services.AddIdentityCore<Guardian>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DisabilityExpertGuardiansContext>()
    .AddDefaultTokenProviders()
    .AddUserManager<UserManager<Guardian>>();
builder.Services.AddIdentityCore<Employee>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<EmployeeContext>()
    .AddDefaultTokenProviders()
    .AddUserManager<UserManager<Employee>>();

// Adding the identity api endpoints
builder.Services.AddIdentityApiEndpoints<UserBase>()
    .AddEntityFrameworkStores<DisabilityExpertGuardiansContext>()
    .AddEntityFrameworkStores<CompanyResearchContext>()
    .AddEntityFrameworkStores<EmployeeContext>();

// adding signalr
builder.Services.AddSignalR();

// Adding the API controllers
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at
// https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// adding the swagger documentation
builder.Services.ConfigureSwaggerGen(setup => {
  setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {
    Title = "Disability Expert API", Version = "v1"
  });
});

// authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
      // Adding the JWT bearer token authentication
      options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration["Jwt:Key"] ?? string.Empty))
      };

      // adding the cookie authentication
      options.Events = new JwtBearerEvents {
        OnMessageReceived =
            context => {
              if (context.Request.Cookies.ContainsKey("Bearer")) {
                context.Token = context.Request.Cookies["Bearer"];
              }
              return Task.CompletedTask;
            },
      };
    })
    .AddGoogle(googleOptions => {
      // getting the client id and secret from the configuration (secret
      // manager)
      googleOptions.ClientId = googleClientId;
      googleOptions.ClientSecret = googleClientSecret;
    });

// service for authorization
builder.Services.AddAuthorization(options => {
  // adding the policies for the different user types
  options.AddPolicy("DisabilityExpertWithoutGuardian",
                    policy => policy.RequireClaim(
                        CustomClaimTypes.UserType,
                        UserTypes.DisabilityExpertWithoutGuardian.ToString()));

  options.AddPolicy("DisabilityExpertWithGuardian",
                    policy => policy.RequireClaim(
                        CustomClaimTypes.UserType,
                        UserTypes.DisabilityExpertWithGuardian.ToString()));
  options.AddPolicy(
      "CompanyApproved",
      policy => policy.RequireClaim(CustomClaimTypes.UserType,
                                    UserTypes.CompanyApproved.ToString()));
  options.AddPolicy(
      "Guardian", policy => policy.RequireClaim(CustomClaimTypes.UserType,
                                                UserTypes.Guardian.ToString()));

  options.AddPolicy(
      "Employee", policy => policy.RequireClaim(CustomClaimTypes.UserType,
                                                UserTypes.Employee.ToString()));

  options.AddPolicy(
      "Admin", policy => policy.RequireClaim(CustomClaimTypes.EmployeeType,
                                             EmployeeTypes.Admin.ToString()));

  options.AddPolicy("Moderator", policy => policy.RequireClaim(
                                     CustomClaimTypes.EmployeeType,
                                     EmployeeTypes.Moderator.ToString()));

  // policy for all disability experts
  options.AddPolicy(
      "DisabilityExpert",
      policy => policy.RequireAssertion(
          context =>
              context.User.HasClaim(
                  CustomClaimTypes.UserType,
                  UserTypes.DisabilityExpertWithGuardian.ToString()) ||
              context.User.HasClaim(
                  CustomClaimTypes.UserType,
                  UserTypes.DisabilityExpertWithoutGuardian.ToString())));

  // policy for DisabilityExperts and Employees
  options.AddPolicy(
      "EmployeeOrDisabilityExpert",
      policy => policy.RequireAssertion(
          context =>
              context.User.HasClaim(
                  CustomClaimTypes.UserType,
                  UserTypes.DisabilityExpertWithGuardian.ToString()) ||
              context.User.HasClaim(
                  CustomClaimTypes.UserType,
                  UserTypes.DisabilityExpertWithoutGuardian.ToString()) ||
              context.User.HasClaim(CustomClaimTypes.UserType,
                                    UserTypes.Employee.ToString())));
});

// Add User Manager Wrappers
builder.Services.AddScoped<UserManagerWrapper<DisabilityExpert>>();
builder.Services.AddScoped<UserManagerWrapper<Company>>();
builder.Services.AddScoped<UserManagerWrapper<Guardian>>();
builder.Services.AddScoped<UserManagerWrapper<Employee>>();

// adding service for checking if a disability expert has a disability
builder.Services.AddScoped<ExpertDisabilityService>();

// adding the googledecodejwt service, which is used to decode the google jwt
builder.Services.AddScoped<DecodeJwt>();

// Configure and add the dictionary
builder.Services.AddScoped(
    provider => new Dictionary<UserTypes, IUserManagerWrapper> {
      { UserTypes.DisabilityExpertWithoutGuardian,
        provider.GetRequiredService<UserManagerWrapper<DisabilityExpert>>() },
      { UserTypes.DisabilityExpertWithGuardian,
        provider.GetRequiredService<UserManagerWrapper<DisabilityExpert>>() },
      { UserTypes.CompanyApproved,
        provider.GetRequiredService<UserManagerWrapper<Company>>() },
      { UserTypes.CompanyUnApproved,
        provider.GetRequiredService<UserManagerWrapper<Company>>() },
      { UserTypes.Guardian,
        provider.GetRequiredService<UserManagerWrapper<Guardian>>() },
      { UserTypes.Employee,
        provider.GetRequiredService<UserManagerWrapper<Employee>>() }
    });

// adding the user resolver service, which is used to check users across
// databases
builder.Services.AddScoped<UserResolveService>();
builder.Services.AddScoped<UserResolveServiceGoogle>();

// this service is used to generate and validate tokens
builder.Services.AddScoped<AuthTokenService>();

// this service handles the login logic
builder.Services.AddScoped<LoginLogic>();

// this service is used for common registration logic
builder.Services.AddScoped<RegistrationService>();

// Add CORS services
builder.Services.AddCors(options => {
  options.AddPolicy("PublicApiPolicy", corsPolicyBuilder => {
    // allowing all origins, headers and methods because the companies also need
    // to be able to access the API
    corsPolicyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
  });
  options.AddPolicy("FrontendPolicy", corsPolicyBuilder => {
    // specific policy for the frontend, only allowing the frontend to access
    corsPolicyBuilder
        .WithOrigins("https://sem3-5b.com", "https://www.sem3-5b.com",
                     "http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
  });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Adding the routing
app.UseRouting();

// adding cors policies
app.UseCors("PublicApiPolicy");
app.UseCors("FrontendPolicy");

// Adding api prefix to the routes
app.UsePathBase("/api");

// Adding authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

// Mapping the controllers
app.MapControllers();

app.MapHub<ChatHub>("/api/chat");

app.Run();
