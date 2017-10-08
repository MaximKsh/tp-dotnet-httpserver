using System.Text;

namespace HttpServer.Middleware
{
    public class ResponseWriter: IHttpServerMiddleware
    {
        #region invoke
        
        public void Invoke(HttpRequest request, HttpResponse response)
        {
            var newLine = response.UseCrLf ? "\r\n" : "\n";
            var stringBuilder = new StringBuilder();
            
            stringBuilder.Append(
                response.HttpVersion?.GetCaption() ?? HttpVersion.Http11.GetCaption());
            stringBuilder.Append(" ");
            stringBuilder.Append(
                response.HttpStatusCode?.GetCaption() ?? HttpStatusCode.InternalServerError.GetCaption());
            stringBuilder.Append(newLine);

            foreach (var header in response.Headers)
            {
                stringBuilder.Append(header.Key);
                stringBuilder.Append(": ");
                stringBuilder.Append(header.Value);
                stringBuilder.Append(newLine);
            }
            stringBuilder.Append(newLine);
            response.RawHeadersResponse = stringBuilder.ToString();
        }
        
        #endregion
    }
}