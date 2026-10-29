using Library_Management_System.Data;
using Library_Management_System.Repositories;
using Library_Management_System.Service;

// ADDED: Using statements for the new isolated module
using Library_Management_System.NewModule.Data;
using Library_Management_System.NewModule.Repositories;
using Library_Management_System.NewModule.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

// 1. Register Existing DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ADDED: Register the separate DbContext for the new module
builder.Services.AddDbContext<NewBooksDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NewBooksDbConnection")));

// 2. Register Your Services (Both Repositories AND Services)

// Existing Registrations
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<Library_Management_System.Repositories.IStudentRepository, Library_Management_System.Repositories.StudentRepository>();
builder.Services.AddScoped<Library_Management_System.Service.IStudentService, Library_Management_System.Service.StudentService>();
builder.Services.AddScoped<IFineRepository, FineRepository>();
builder.Services.AddScoped<IFineService, FineService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookLoanService, BookLoanService>();

// ADDED: New Module Registrations 
builder.Services.AddScoped<IBookMasterRepository, BookMasterRepository>();
builder.Services.AddScoped<IBookMasterService, BookMasterService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IBookCopyRepository, BookCopyRepository>();
builder.Services.AddScoped<IBookCopyService, BookCopyService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddScoped<Library_Management_System.NewModule.Repositories.IStudentRepository, Library_Management_System.NewModule.Repositories.StudentRepository>();
builder.Services.AddScoped<Library_Management_System.NewModule.Services.IStudentService, Library_Management_System.NewModule.Services.StudentService>();

// ---> FIXED: ADDED ISSUE & ISSUE DETAIL REGISTRATIONS <---
builder.Services.AddScoped<Library_Management_System.NewModule.Repositories.IIssueRepository, Library_Management_System.NewModule.Repositories.IssueRepository>();
builder.Services.AddScoped<Library_Management_System.NewModule.Services.IIssueService, Library_Management_System.NewModule.Services.IssueService>();
builder.Services.AddScoped<Library_Management_System.NewModule.Repositories.IIssueDetailRepository, Library_Management_System.NewModule.Repositories.IssueDetailRepository>();
builder.Services.AddScoped<Library_Management_System.NewModule.Services.IIssueDetailService, Library_Management_System.NewModule.Services.IssueDetailService>();

// 3. Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

// 4. Configure Swagger to accept tokens
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Paste your JWT token below. (Do NOT type the word 'Bearer', it is added automatically)."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// 5. THESE COMMANDS ARE REQUIRED TO KEEP THE APP RUNNING
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Authentication MUST come before Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();