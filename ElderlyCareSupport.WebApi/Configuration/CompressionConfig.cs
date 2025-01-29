using System.IO.Compression;
using Microsoft.AspNetCore.ResponseCompression;

namespace ElderlyCareSupport.Server.Configuration;

public static class CompressionConfig
{
    public static IServiceCollection AddCompressionConfig(this IServiceCollection services)
    {
        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<GzipCompressionProvider>();
            options.Providers.Add<BrotliCompressionProvider>();
            options.MimeTypes = ResponseCompressionDefaults.MimeTypes;
        });
        services.Configure<BrotliCompressionProviderOptions>(o =>
        {
            o.Level = CompressionLevel.Optimal;
        });

        services.Configure<GzipCompressionProviderOptions>(o =>
        {
            o.Level = CompressionLevel.SmallestSize;
        });
        return services;
    }


    public static IApplicationBuilder UseCompressionConfig(this IApplicationBuilder app)
    {
        app.UseResponseCompression();
        return app;
    }
}