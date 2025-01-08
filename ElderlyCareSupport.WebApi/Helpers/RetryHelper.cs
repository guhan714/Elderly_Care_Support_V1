namespace ElderlyCareSupport.Server.Helpers
{
    public static class RetryHelper
    {
        public static async Task<TResult?> RetryAsync<TResult>(Func<Task<TResult>> action, int maxRetries, ILogger logger)
        {
            var attempt = 0;
            while (attempt < maxRetries)
            {
                try
                {
                    return await action();
                }
                catch (Exception ex)
                {
                    logger.LogError("Error: {Message}", ex.Message);
                    attempt++;
                    throw;
                }
            }
            return default;
        }
    }
}
