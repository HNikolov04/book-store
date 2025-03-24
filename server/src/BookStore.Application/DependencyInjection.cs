using BookStore.Application.Interfaces;
using BookStore.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IReviewService, ReviewService>();

        return services;
    }
}