﻿using System.IO;
using System.Text;

namespace HttpServer.Middleware
{
    public class RequestParser: IHttpServerMiddleware
    {
        #region constants
        
        private const char Space = ' ';
        private const char Cr = '\r';
        private const char Plus = '+';
        private const char Colon = ':';
        private const char QuestionMark = '?';
        private const char Percentage = '%';
        private const char Zero = '0';
        private const char Nine = '9';
        // ReSharper disable once InconsistentNaming
        private const char a = 'a';
        // ReSharper disable once InconsistentNaming
        private const char f = 'f';
        private const char A = 'A';
        private const char F = 'F';
        
        #endregion
        
        #region invoke
        
        public void Invoke(HttpRequest request, HttpResponse response)
        {
            if (!response.Success)
            {
                return;
            }
            
            foreach (var ch in request.RawRequest)
            {
                if (ch == '\r')
                {
                    request.UseCrLf = true;
                    response.UseCrLf = true;
                }
                else if (ch == '\n')
                {
                    break;
                }
            }

            var lineCount = 0;
            using (var stream = new StringReader(request.RawRequest))
            {
                var firstLine = stream.ReadLine();
                if (string.IsNullOrWhiteSpace(firstLine))
                {
                    response.Success = false;
                    response.HttpStatusCode = HttpStatusCode.BadRequest;
                    return;
                }
            
                if (!ParseRequestLine(firstLine, request, response))
                {
                    return;
                }
                response.HttpVersion = request.HttpVersion;
                lineCount++;

                string line;
                var headers = request.Headers;
                while ((line = stream.ReadLine()) != null
                       && !string.IsNullOrWhiteSpace(line))
                {
                    var colonIndex = line.IndexOf(Colon);
                    if (colonIndex < 1 
                        || line.Length == colonIndex - 1)
                    {
                        response.HttpStatusCode = HttpStatusCode.BadRequest;
                        return;
                    }
                    headers[line.Substring(0, colonIndex).Trim()] = line.Substring(colonIndex + 1).Trim();
                }
            }
            
            if (lineCount == 0)
            {
                response.Success = false;
                response.HttpStatusCode = HttpStatusCode.BadRequest;
            }
        }

        private static bool ParseRequestLine(string line, HttpRequest request, HttpResponse response)
        {
            var position = 0;
            // Пропустим возможные в начале пустые символы
            for (;position < line.Length && line[position] == Space; position++)
            {}
            
            if (!ParseMethod(line, request, response, ref position))
            {
                return false;
            }
            
            position++;
            
            if (!ParseUrl(line, request, response, ref position))
            {
                return false;
            }

            position++;

            return ParseVersion(line, request, response, ref position);
        }

        private static bool ParseMethod(
            string line, 
            HttpRequest request, 
            HttpResponse response, 
            ref int position)
        {
            var startPosition = position;
            var getSimilar = true;
            var headSimilar = true;
            var getCaption = HttpMethod.Get.GetCaption();
            var headCaption = HttpMethod.Head.GetCaption();
            
            for (;position < line.Length && line[position] != Space && line[position] != Cr; position++)
            {
                if (getSimilar
                    && ((position - startPosition) >= getCaption.Length 
                        ||line[position] != getCaption[position - startPosition]))
                {
                    getSimilar = false;
                }
                if (headSimilar 
                    && ((position - startPosition) >= headCaption.Length 
                        || line[position] != headCaption[position - startPosition]))
                {
                    headSimilar = false;
                }
            }
            if (getSimilar
                && position - startPosition == getCaption.Length)
            {
                request.HttpMethod = HttpMethod.Get;
                return true;
            }
            if (headSimilar
                && position - startPosition == headCaption.Length)
            {
                request.HttpMethod = HttpMethod.Head;
                return true;
            }
            response.Success = false;
            response.HttpStatusCode = HttpStatusCode.NotAllowed;
            return false;
        }

        private static bool ParseUrl(
            string line, 
            HttpRequest request, 
            HttpResponse response, 
            ref int position)
        {
            var startPosition = position;
            var pathEncoded = false;
            for (; position < line.Length && line[position] != Space  && line[position] != Cr; position++)
            {
                var ch = line[position];
                
                if (ch == QuestionMark)
                {
                    if (position == startPosition)
                    {
                        // Начинаться с ? некорректно
                        response.Success = false;
                        response.HttpStatusCode = HttpStatusCode.BadRequest;
                        return false;
                    }

                    break;
                }
                if (ch == Percentage)
                {
                    if (position == startPosition)
                    {
                        // Начинаться с % некорректно
                        response.Success = false;
                        response.HttpStatusCode = HttpStatusCode.BadRequest;
                        return false;
                    }

                    pathEncoded = true;
                }
            }
            if (position == startPosition)
            {
                // Пустой URL
                response.Success = false;
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                return false;
            }

            request.Url = pathEncoded
                ? new Decoder(position - startPosition).Decode(line, startPosition, position)
                : line.Substring(startPosition, position - startPosition);

            // т.к. get параметры не нужны, то просто их проигнорируем
            for (; position < line.Length && line[position] != Space ; position++)
            {
            }

            return true;
        }
        
        private static bool ParseVersion(
            string line,
            HttpRequest request,
            HttpResponse response,
            ref int position)
        {
            var startPosition = position;
            var http10Similar = true;
            var http11Similar = true;
            var http10Caption = HttpVersion.Http10.GetCaption();
            var http11Caption = HttpVersion.Http11.GetCaption();
            
            for (;position < line.Length  && line[position] != Space && line[position] != Cr; position++)
            {
                if (http10Similar 
                    && ((position - startPosition) >= http10Caption.Length 
                        || line[position] != http10Caption[position - startPosition]))
                {
                    http10Similar = false;
                }
                if (http11Similar 
                    && ((position - startPosition) >= http11Caption.Length 
                        || line[position] != http11Caption[position - startPosition]))
                {
                    http11Similar = false;
                }
            }
            if (startPosition == position)
            {
                // Default значение, когда нет ничего
                request.HttpVersion = HttpVersion.Http11;
                return true;
            }
            if (http10Similar
                && position - startPosition == http10Caption.Length)
            {
                request.HttpVersion = HttpVersion.Http10;
                return true;
            }
            if (http11Similar
                && position - startPosition == http11Caption.Length)
            {
                request.HttpVersion = HttpVersion.Http11;
                return true;
            }
            response.Success = false;
            response.HttpStatusCode = HttpStatusCode.BadRequest;
            return false;
        }
        
        #endregion
        
        #region decoder
        
        private class Decoder
        {
            private readonly int bufferSize;
            private int numChars;
            private readonly char[] charBuffer;
            private int numBytes;
            private byte[] byteBuffer;
            private readonly Encoding encoding = Encoding.UTF8;
 
            private void FlushBytes()
            {
                if (this.numBytes > 0)
                {
                    this.numChars += this.encoding.GetChars(
                        this.byteBuffer, 
                        0, 
                        this.numBytes,
                        this.charBuffer, 
                        this.numChars);
                    this.numBytes = 0;
                }
            }
 
            public Decoder(int bufferSize)
            {
                this.bufferSize = bufferSize;
                this.charBuffer = new char[bufferSize];
            }
            
            public string Decode(string value, int from, int to)
            {
                if (value == null)
                {
                    return null;
                }
 
                var count = value.Length;
                for (var pos = from; pos < to; pos++)
                {
                    var ch = value[pos];
 
                    if (ch == Plus)
                    {
                        ch = Space;
                    }
                    else if (ch == Percentage && pos < count - 2)
                    {
                        var h1 = HexToInt(value[pos + 1]);
                        var h2 = HexToInt(value[pos + 2]);
 
                        if (h1 >= 0 && h2 >= 0)
                        {     
                            var b = (byte)((h1 << 4) | h2);
                            pos += 2;
 
                            this.AddByte(b);
                            continue;
                        }
                    }

                    if ((ch & 0xFF80) == 0)
                    {
                        this.AddByte((byte) ch); 
                    }
                    else
                    {
                        this.AddChar(ch);
                    }
                }
 
                return this.GetString();
            }

            private void AddChar(char ch)
            {
                if (this.numBytes > 0)
                {
                    this.FlushBytes();
                }
 
                this.charBuffer[this.numChars++] = ch;
            }

            private void AddByte(byte b)
            {
                if (this.byteBuffer == null)
                {
                    this.byteBuffer = new byte[this.bufferSize];
                }
                this.byteBuffer[this.numBytes++] = b;
            }

            private string GetString()
            {
                if (this.numBytes == 0)
                {
                    return string.Empty;
                }
                this.FlushBytes();
                return new string(this.charBuffer, 0, this.numChars);
            }
        
            private static int HexToInt(char h)
            {
                if (h >= Zero && h <= Nine)
                {
                    return h - Zero;
                }
                if (h >= a && h <= f)
                {
                    return h - a + 10;
                }
                if (h >= A && h <= F)
                {
                    return h - A + 10;
                }
                return -1;
            }
        }
        
        #endregion
        
    }
}