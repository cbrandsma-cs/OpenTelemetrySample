using System.Diagnostics;

namespace Sample.WebApi.Extensions;

public static class ActivitySourceExtensions
{
    public static Activity? StartActivityWithTags(this System.Diagnostics.ActivitySource source, string name, 
        List<KeyValuePair<string, object?>> tags)
    {
        return source.StartActivity(name, 
            ActivityKind.Internal,
            Activity.Current?.Context ?? new ActivityContext(),
            tags);
    }
}