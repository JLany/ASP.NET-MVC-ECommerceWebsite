using ITIECommerce.Web.Authorization.ProductAuthorizationServices;

namespace ITIECommerce.Web.Authorization;

public static class Constants
{
    public static readonly string CreateOperationName = Enum.GetName(ProductOperation.Create)!;
    public static readonly string ReadOperationName = Enum.GetName(ProductOperation.Read)!;
    public static readonly string UpdateOperationName = Enum.GetName(ProductOperation.Update)!;
    public static readonly string DeleteOperationName = Enum.GetName(ProductOperation.Delete)!;
    public static readonly string ViewProductOperationName = Enum.GetName(ProductOperation.ViewMyProducts)!;

    public static readonly string ProductSellerRoleName = "Seller";
    public static readonly string ProductCustomerRoleName = "Customer";
}
