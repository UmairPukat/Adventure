// Step 1: Create a new application builder.
var builder = WebApplication.CreateBuilder(args);


var Configuration = builder.Configuration;

// Step 2: Retrieve the services container from the builder.
var Services = builder.Services;

// Step 3: Retrieve secret value from configuration and convert it to bytes.
var secret = builder.Configuration.GetSection("Secret").Value;
var key = Encoding.ASCII.GetBytes(secret);

// Step 4: Retrieve the connection string in appsetting.json for the database.
string mySqlConnectionStr = builder.Configuration.GetConnectionString("DefaultConnection");

// Step 5: Register required services.

// Add controllers.
Services.AddControllers();

// Add API Explorer for endpoints.
Services.AddEndpointsApiExplorer();

// Add database context with SQL Server provider.
Services.AddDbContext<AdventureDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

// Register custom services and configurations.
ServiceConfiguration.Register(Services);

// Add AutoMapper for model to Dto and Dto to model mapping.
Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Step 6: Configure Authentication.

// Adding Authentication  
Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
});
// Jwt token validation and authentication
Services.AddAuthentication(
    options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            if ((context.Request.Path.Value!.StartsWith("/signalrhome")
                || context.Request.Path.Value!.StartsWith("/looney")
                || context.Request.Path.Value!.StartsWith("/usersdm")
               )
                && context.Request.Query.TryGetValue("token", out StringValues token)
            )
            {
                context.Token = token;
            }

            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            var te = context.Exception;
            return Task.CompletedTask;
        }
    };
});
#region Bearer Auhtentication
Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
});
#endregion
Services.AddLogging(builder => builder.AddNLog());
// Step 8: Build the application.
var app = builder.Build();

// Step 9: Configure middleware pipeline based on environment.

if (app.Environment.IsDevelopment())
{
    // Enable Swagger UI and Swagger endpoint in development environment.
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use authentication and authorization middleware.
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<LoggerMiddleware>();
// Map controllers to handle incoming HTTP requests.
app.MapControllers();

// Run the application.
app.Run();
