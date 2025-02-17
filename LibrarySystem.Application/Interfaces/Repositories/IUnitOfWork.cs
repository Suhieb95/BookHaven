using InventoryManagement.Application.Interfaces.Services;
using InventoryManagement.Application.Interfaces.Services.CatagoryService;
using InventoryManagement.Application.Interfaces.Services.ProductService;
using InventoryManagement.Application.Interfaces.Services.PurchaseOrderService;
using InventoryManagement.Application.Interfaces.Services.SalesOrderService;
using InventoryManagement.Application.Interfaces.Services.ShippingService;
using InventoryManagement.Application.Interfaces.Services.UnitsService;
using InventoryManagement.Application.Interfaces.Services.UserSerivce;
using InventoryManagement.Application.Interfaces.Services.WarehouseService;

namespace InventoryManagement.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    IUserSerivce UserSerivce { get; }
    IPurchaseOrderService PurchaseOrderService { get; }
    ISalesOrderService SalesOrderService { get; }
    ICategoryService CatagoryService { get; }
    IProductService ProductService { get; }
    IShippingService ShippingService { get; }
    IFileService FileService { get; }
    ICategoryService CategoryService { get; }
    IUnitsService UnitsService { get; }
    IWarehouseService WarehouseService { get; }
}
