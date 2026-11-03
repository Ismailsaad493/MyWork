using Library_Management_System.Data;
using Library_Management_System.Repositories;
using Library_Management_System.Service;
using Library_Management_System.NewModule.Data;
using Library_Management_System.NewModule.Repositories;
using Library_Management_System.NewModule.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

// 1. Register Old Module DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Register New Module DbContext
builder.Services.AddDbContext<NewBooksDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NewBooksDbConnection")));

// ==========================================================
// OLD MODULE REGISTRATIONS
// ==========================================================
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<Library_Management_System.Repositories.IStudentRepository, Library_Management_System.Repositories.StudentRepository>();
builder.Services.AddScoped<Library_Management_System.Service.IStudentService, Library_Management_System.Service.StudentService>();
builder.Services.AddScoped<IFineRepository, FineRepository>();
builder.Services.AddScoped<IFineService, FineService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookLoanService, BookLoanService>();

// ==========================================================
// NEW MODULE REGISTRATIONS
// ==========================================================
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

builder.Services.AddScoped<Library_Management_System.NewModule.Repositories.IReturnRepository, Library_Management_System.NewModule.Repositories.ReturnRepository>();
builder.Services.AddScoped<Library_Management_System.NewModule.Services.IReturnService, Library_Management_System.NewModule.Services.ReturnService>();
builder.Services.AddScoped<Library_Management_System.NewModule.Repositories.IReturnDetailRepository, Library_Management_System.NewModule.Repositories.ReturnDetailRepository>();
builder.Services.AddScoped<Library_Management_System.NewModule.Services.IReturnDetailService, Library_Management_System.NewModule.Services.ReturnDetailService>();

builder.Services.AddScoped<Library_Management_System.NewModule.Repositories.IStudentRepository, Library_Management_System.NewModule.Repositories.StudentRepository>();
builder.Services.AddScoped<Library_Management_System.NewModule.Services.IStudentService, Library_Management_System.NewModule.Services.StudentService>();

builder.Services.AddScoped<Library_Management_System.NewModule.Repositories.IIssueRepository, Library_Management_System.NewModule.Repositories.IssueRepository>();
builder.Services.AddScoped<Library_Management_System.NewModule.Services.IIssueService, Library_Management_System.NewModule.Services.IssueService>();
builder.Services.AddScoped<Library_Management_System.NewModule.Repositories.IIssueDetailRepository, Library_Management_System.NewModule.Repositories.IssueDetailRepository>();
builder.Services.AddScoped<Library_Management_System.NewModule.Services.IIssueDetailService, Library_Management_System.NewModule.Services.IssueDetailService>();

builder.Services.AddScoped<Library_Management_System.NewModule.Repositories.IFineMasterRepository, Library_Management_System.NewModule.Repositories.FineMasterRepository>();
builder.Services.AddScoped<Library_Management_System.NewModule.Services.IFineMasterService, Library_Management_System.NewModule.Services.FineMasterService>();
builder.Services.AddScoped<Library_Management_System.NewModule.Repositories.IFineDetailMasterRepository, Library_Management_System.NewModule.Repositories.FineDetailMasterRepository>();
builder.Services.AddScoped<Library_Management_System.NewModule.Services.IFineDetailMasterService, Library_Management_System.NewModule.Services.FineDetailMasterService>();

builder.Services.AddScoped<Library_Management_System.NewModule.Repositories.IPaymentMasterRepository, Library_Management_System.NewModule.Repositories.PaymentMasterRepository>();
builder.Services.AddScoped<Library_Management_System.NewModule.Services.IPaymentMasterService, Library_Management_System.NewModule.Services.PaymentMasterService>();
builder.Services.AddScoped<Library_Management_System.NewModule.Repositories.IPaymentDetailMasterRepository, Library_Management_System.NewModule.Repositories.PaymentDetailMasterRepository>();
builder.Services.AddScoped<Library_Management_System.NewModule.Services.IPaymentDetailMasterService, Library_Management_System.NewModule.Services.PaymentDetailMasterService>();

builder.Services.AddScoped<Library_Management_System.NewModule.Services.IBookSearchService, Library_Management_System.NewModule.Services.BookSearchService>();

// ==========================================================
// AUTHENTICATION & SWAGGER CONFIGURATION
// ==========================================================
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();