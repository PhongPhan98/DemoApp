using System.Net;

namespace DemoApp.API.Middlewares
{
    public class AdminSafeListMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<AdminSafeListMiddleware> _logger;
        private readonly byte[][] _safeList;

        public AdminSafeListMiddleware(
            RequestDelegate next,
            ILogger<AdminSafeListMiddleware> logger,
            string safeList
            )
        {
            var ips = safeList.Split(';');
            _safeList = new byte[ips.Length][];
            for (var i = 0; i < ips.Length; i++)
            {
                _safeList[i] = IPAddress.Parse(ips[i]).GetAddressBytes();
            }

            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method != HttpMethod.Get.Method)
            {
                var remoteIp = context.Connection.RemoteIpAddress;
                _logger.LogDebug("Request from Remote IP address: ", remoteIp);
                if (remoteIp == null)
                {
                    _logger.LogWarning("Can not find remote ip address: ");
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return;
                }
                var bytes = remoteIp.GetAddressBytes();
                var badIp = true;

                foreach (var address in _safeList)
                {
                    if (address.SequenceEqual(bytes))
                    {
                        badIp = false;
                        break;
                    }
                }

                if (badIp)
                {
                    _logger.LogWarning("Forbiden request from remote ip address: ", remoteIp);
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return;
                }
            }

            await _next.Invoke(context);
        }
    }
}