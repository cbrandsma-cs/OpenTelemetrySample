using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Sample.WebApi;

public static class DiagnosticsConfig
{
    public const string ServiceName = "Sample.WebApi";
    public static Meter Meter = new Meter(ServiceName);
    public static ActivitySource Source = new ActivitySource(ServiceName);

    public static Histogram<double> SalesValue = Meter.CreateHistogram<double>("sales.value");
    public static Histogram<double> SalesMarkupValue = Meter.CreateHistogram<double>("sales.markup");
    public static Counter<long> SalesCount = Meter.CreateCounter<long>("sales.count");
   


    public static void AddSalesMetrics(int productId, decimal price, decimal priceWithMarkup)
    {
        var labels = new KeyValuePair<string, object?> (DiagnosticNames.ProductId, productId);

        DiagnosticsConfig.SalesValue.Record((double)priceWithMarkup, labels);
        DiagnosticsConfig.SalesMarkupValue.Record((double)(priceWithMarkup - price), labels);
        DiagnosticsConfig.SalesCount.Add(1, labels);
    }
}


