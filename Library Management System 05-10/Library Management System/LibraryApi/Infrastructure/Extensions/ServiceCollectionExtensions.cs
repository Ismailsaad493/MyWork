using LibraryApi.Infrastructure.Context;
using LibraryApi.Infrastructure.Models;
using LibraryApi.Infrastructure.Profiles;
using LibraryApi.Infrastructure.Repositories;
using LibraryApi.Infrastructure.Services;
using LibraryApi.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLibraryInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("ConnectionStrings:DefaultConnection must be configured.");
        }

        services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(connectionString));
        services.AddOptions<LoanPolicy>()
            .Bind(configuration.GetSection(LoanPolicy.SectionName))
            .Validate(policy => policy.MaximumLoanPeriodDays is >= 1 and <= 60
                && policy.DefaultLoanPeriodDays >= 1
                && policy.DefaultLoanPeriodDays <= policy.MaximumLoanPeriodDays,
                "Loan policy periods must be between 1 and 60 days, and the default cannot exceed the maximum.")
            .ValidateOnStart();

        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<ILoanRepository, LoanRepository>();
        services.AddScoped<IFineRepository, FineRepository>();

        services.AddSingleton<BookProfile>();
        services.AddSingleton<MemberProfile>();
        services.AddSingleton<LoanProfile>();
        services.AddSingleton<FineProfile>();

        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IMemberService, MemberService>();
        services.AddScoped<ILoanService, LoanService>();
        services.AddScoped<IFineService, FineService>();

        return services;
    }
}
