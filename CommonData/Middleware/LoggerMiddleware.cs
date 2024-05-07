namespace CommonData.Middleware
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggerMiddleware> _logger;
        private readonly IConfiguration _configuration;

        public LoggerMiddleware(RequestDelegate next, ILogger<LoggerMiddleware> logger, IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                DateTime currentDate = DateTime.Now;
                // Get the full month name (e.g., "August")
                string fullMonthName = currentDate.ToString("MMMM");
                int day = currentDate.Day;
                // Step 1: Create NLog configuration
                var nlogConfig = new LoggingConfiguration();

                // Step 2: Define the target (file in this case)
                var fileTarget = new FileTarget("logs.txt")
                {
                    FileName = _configuration.GetSection("LogsDirectoryPath").Value + DateTime.Now.Year + "\\" + day + "_" + fullMonthName + "-" + "Logs.txt",
                    Layout = "${longdate} ${message}"
                };
                var rule = new LoggingRule("*", LogLevel.Trace, LogLevel.Fatal, fileTarget);
                // Step 3: Add the target to the configuration
                nlogConfig.AddTarget(fileTarget);

                // Step 4: Define a rule for logging (e.g., log everything to the file target)
                nlogConfig.AddRuleForAllLevels(fileTarget);

                // Step 5: Apply the configuration
                LogManager.Configuration = nlogConfig;
                await _next(context);
            }
            catch (Exception error)
            {
                var responce = context.Response;
                responce.ContentType = "application/json";

                switch (error)
                {
                    case AppException e:
                        //custom application error
                        responce.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        //not found error
                        responce.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        //unhandled error
                        _logger.LogError(error, error.Message);
                        responce.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                _logger.LogError(error, error.Message);
                var result = System.Text.Json.JsonSerializer.Serialize(new { message = error?.Message });
                await responce.WriteAsync(result);
            }
        }
    }
}
