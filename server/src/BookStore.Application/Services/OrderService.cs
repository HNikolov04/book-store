using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;

namespace BookStore.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
    {
        var orders = await _orderRepository.GetAllOrdersAsync();
        return orders.Select(order => new OrderDto
        {
            Id = order.Id,
            CustomerName = order.CustomerName,
            Address = order.Address,
            BookId = order.BookId,
            Quantity = order.Quantity
        }).ToList();
    }

    public async Task<OrderDto?> GetOrderByIdAsync(int id)
    {
        var order = await _orderRepository.GetOrderByIdAsync(id);

        if (order == null)
        {
            return null;
        }

        return new OrderDto
        {
            Id = order.Id,
            CustomerName = order.CustomerName,
            Address = order.Address,
            BookId = order.BookId,
            Quantity = order.Quantity
        };
    }

    public async Task<OrderDto> CreateOrderAsync(OrderDto orderDto)
    {
        var order = new Order
        {
            CustomerName = orderDto.CustomerName,
            Address = orderDto.Address,
            BookId = orderDto.BookId,
            Quantity = orderDto.Quantity
        };

        var newOrder = await _orderRepository.AddOrderAsync(order);

        return new OrderDto
        {
            Id = newOrder.Id,
            CustomerName = newOrder.CustomerName,
            Address = newOrder.Address,
            BookId = newOrder.BookId,
            Quantity = newOrder.Quantity
        };
    }

    public async Task<OrderDto?> UpdateOrderAsync(int id, OrderDto orderDto)
    {
        var order = await _orderRepository.GetOrderByIdAsync(id);

        if (order == null)
        {
            return null;
        }

        order.CustomerName = orderDto.CustomerName;
        order.Address = orderDto.Address;
        order.BookId = orderDto.BookId;
        order.Quantity = orderDto.Quantity;

        var updatedOrder = await _orderRepository.UpdateOrderAsync(order);

        return new OrderDto
        {
            Id = updatedOrder.Id,
            CustomerName = updatedOrder.CustomerName,
            Address = updatedOrder.Address,
            BookId = updatedOrder.BookId,
            Quantity = updatedOrder.Quantity
        };
    }

    public async Task<bool> DeleteOrderAsync(int id)
    {
        return await _orderRepository.DeleteOrderAsync(id);
    }
}
