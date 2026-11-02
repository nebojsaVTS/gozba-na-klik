using System.Text.Json;
using GozbaNaKlik.API.Data;
using GozbaNaKlik.API.DTOs;
using GozbaNaKlik.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlik.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrdersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        if (request.UserId <= 0)
        {
            return BadRequest("Korisnik nije validan.");
        }

        if (request.RestaurantId <= 0)
        {
            return BadRequest("Restoran nije validan.");
        }

        if (request.AddressId <= 0)
        {
            return BadRequest("Adresa nije validna.");
        }

        if (request.Items == null || request.Items.Count == 0)
        {
            return BadRequest("Porudžbina mora imati najmanje jednu stavku.");
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId);

        if (user == null)
        {
            return NotFound("Korisnik nije pronađen.");
        }

        var restaurant = await _context.Restaurants
            .FirstOrDefaultAsync(r => r.Id == request.RestaurantId);

        if (restaurant == null)
        {
            return NotFound("Restoran nije pronađen.");
        }

        var address = await _context.Addresses
            .FirstOrDefaultAsync(a =>
                a.Id == request.AddressId &&
                a.UserId == request.UserId);

        if (address == null)
        {
            return BadRequest("Izabrana adresa ne pripada korisniku.");
        }

        var order = new Order
        {
            UserId = request.UserId,
            RestaurantId = request.RestaurantId,
            DeliveryStreet = address.Street,
            DeliveryCity = address.City,
            DeliveryPhoneNumber = address.PhoneNumber,
            Status = OrderStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        decimal totalPrice = 0;

        foreach (var requestedItem in request.Items)
        {
            if (requestedItem.Quantity <= 0)
            {
                return BadRequest("Količina stavke mora biti veća od 0.");
            }

            var menuItem = await _context.MenuItems
                .FirstOrDefaultAsync(m =>
                    m.Id == requestedItem.MenuItemId &&
                    m.RestaurantId == request.RestaurantId);

            if (menuItem == null)
            {
                return BadRequest(
                    $"Jelo sa ID {requestedItem.MenuItemId} ne postoji u izabranom restoranu."
                );
            }

            if (!string.IsNullOrWhiteSpace(requestedItem.Note) &&
                requestedItem.Note.Length > 200)
            {
                return BadRequest("Napomena može imati najviše 200 karaktera.");
            }

            var orderItem = new OrderItem
            {
                MenuItemId = menuItem.Id,
                ItemName = menuItem.Name,
                UnitPrice = menuItem.Price,
                Quantity = requestedItem.Quantity,
                SelectedSides = JsonSerializer.Serialize(
                    requestedItem.SelectedSides ?? new List<string>()
                ),
                Note = string.IsNullOrWhiteSpace(requestedItem.Note)
                    ? null
                    : requestedItem.Note.Trim()
            };

            order.Items.Add(orderItem);

            totalPrice += menuItem.Price * requestedItem.Quantity;
        }

        order.TotalPrice = totalPrice;

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            order.Id,
            order.Status,
            order.TotalPrice,
            order.CreatedAt
        });
    }
}