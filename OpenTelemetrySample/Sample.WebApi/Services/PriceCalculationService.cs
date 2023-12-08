namespace Sample.WebApi.Services;

public class PriceCalculationService
{
    public static decimal GetProductPriceWithMarkup(int categoryId, decimal productPrice)
    {
        var newPrice = productPrice;

        using var activity = DiagnosticsConfig.Source
            .StartActivity("Calculate Product Price With Markup");
        activity.SetTag(DiagnosticNames.ProductCategoryId, categoryId);
        activity.SetTag(DiagnosticNames.ProductPriceOriginal, productPrice);

        // probably some weird calculation
        newPrice =+ 30 + productPrice;

        activity.SetTag(DiagnosticNames.ProductPriceWithMarkup, newPrice);

        return newPrice;

    }
}