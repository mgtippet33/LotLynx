﻿
namespace Auction.Domain.Entities;

public abstract class BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime TimeStamp { get; set; } = DateTime.Now;
}
