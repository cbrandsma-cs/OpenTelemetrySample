using System.Diagnostics;

namespace Sample.WebApi.Services;

public static class ProductValidation
{
    public static bool ValidateProductName(ProductInputModel product)
    {
        if (string.IsNullOrEmpty(product.ProductName))
        {
            Activity.Current?.AddEvent(new ActivityEvent(
                DiagnosticNames.ValidationFailed,
                tags: new ActivityTagsCollection(new List<KeyValuePair<string, object?>>
                {
                    new("model.type", nameof(ProductInputModel)),
                    new("model.failed_field", nameof(product.ProductName)),
                    new("model.filed_value", product.ProductName)
                })));
            return false;
        }

        return true;
    }

}

public record ProductInputModel
{
    public long ProductId {get;set; }
    public string ProductName {get;set; }
    public int ProductType {get;set; }
}