﻿namespace Discount.Grpc.Models;

public class Coupon
{
    public int Id { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}
