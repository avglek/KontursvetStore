using KontursvetStore.Core.Constants;

namespace KontursvetStore.Api.Contracts;

public class OrderRequest
{
    public string Code { get; set; }
    public int Amount { get; set; }
    public string Address { get; set; }
    public PaidSystem PaymentMethod { get; set; }
    public bool IsPaid { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime DateOfOrder { get; set; }
    public string? Comment { get; set; }
    public bool Enabled { get; set; }
}