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

        if (request.Items == null || request.Items.Count == 0)
        {
            return BadRequest("Porudžbina mora imati najmanje jednu stavku.");
        }

        if (!OrderFulfillmentType.All.Contains(request.FulfillmentType))
        {
            return BadRequest(
                "Način preuzimanja mora biti Delivery ili Pickup."
            );
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

        if (
            request.FulfillmentType == OrderFulfillmentType.Delivery &&
            !restaurant.SupportsDelivery
        )
        {
            return BadRequest("Izabrani restoran ne podržava dostavu.");
        }

        if (
            request.FulfillmentType == OrderFulfillmentType.Pickup &&
            !restaurant.SupportsPickup
        )
        {
            return BadRequest(
                "Izabrani restoran ne podržava lično preuzimanje."
            );
        }

        var order = new Order
        {
            UserId = request.UserId,
            RestaurantId = request.RestaurantId,
            FulfillmentType = request.FulfillmentType,
            Status = OrderStatus.Pending,
            CreatedAt = DateTime.UtcNow,
            EstimatedReadyAt = null
        };

        if (request.FulfillmentType == OrderFulfillmentType.Delivery)
        {
            if (!request.AddressId.HasValue || request.AddressId.Value <= 0)
            {
                return BadRequest(
                    "Adresa je obavezna za dostavu."
                );
            }

            var address = await _context.Addresses
                .FirstOrDefaultAsync(a =>
                    a.Id == request.AddressId.Value &&
                    a.UserId == request.UserId);

            if (address == null)
            {
                return BadRequest(
                    "Izabrana adresa ne pripada korisniku."
                );
            }

            order.DeliveryStreet = address.Street;
            order.DeliveryCity = address.City;
            order.DeliveryPhoneNumber = address.PhoneNumber;
        }
        else
        {
            if (string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                return BadRequest(
                    "Broj telefona je obavezan za lično preuzimanje."
                );
            }

            order.DeliveryStreet = string.Empty;
            order.DeliveryCity = string.Empty;
            order.DeliveryPhoneNumber = request.PhoneNumber.Trim();
        }

        decimal totalPrice = 0;

        foreach (var requestedItem in request.Items)
        {
            if (requestedItem.Quantity <= 0)
            {
                return BadRequest(
                    "Količina stavke mora biti veća od 0."
                );
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

            if (
                !string.IsNullOrWhiteSpace(requestedItem.Note) &&
                requestedItem.Note.Length > 200
            )
            {
                return BadRequest(
                    "Napomena može imati najviše 200 karaktera."
                );
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
            order.FulfillmentType,
            order.Status,
            order.TotalPrice,
            order.EstimatedReadyAt,
            order.CreatedAt
        });
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetOrdersByUser(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest("Korisnik nije validan.");
        }

        var userExists = await _context.Users
            .AnyAsync(u => u.Id == userId);

        if (!userExists)
        {
            return NotFound("Korisnik nije pronađen.");
        }

        var ordersFromDatabase = await _context.Orders
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .Select(o => new
            {
                o.Id,
                o.RestaurantId,
                RestaurantName = o.Restaurant.Name,
                o.FulfillmentType,
                o.DeliveryStreet,
                o.DeliveryCity,
                o.DeliveryPhoneNumber,
                o.EstimatedReadyAt,
                o.Status,
                o.TotalPrice,
                o.CreatedAt,
                Items = o.Items.Select(i => new
                {
                    i.Id,
                    i.MenuItemId,
                    i.ItemName,
                    i.UnitPrice,
                    i.Quantity,
                    i.SelectedSides,
                    i.Note
                }).ToList()
            })
            .ToListAsync();

        var orders = ordersFromDatabase.Select(o => new
        {
            o.Id,
            o.RestaurantId,
            o.RestaurantName,
            o.FulfillmentType,
            o.DeliveryStreet,
            o.DeliveryCity,
            o.DeliveryPhoneNumber,
            o.EstimatedReadyAt,
            o.Status,
            o.TotalPrice,
            o.CreatedAt,
            Items = o.Items.Select(i => new
            {
                i.Id,
                i.MenuItemId,
                i.ItemName,
                i.UnitPrice,
                i.Quantity,
                SelectedSides =
                    JsonSerializer.Deserialize<List<string>>(
                        i.SelectedSides
                    ) ?? new List<string>(),
                i.Note
            })
        });

        return Ok(orders);
    }
}