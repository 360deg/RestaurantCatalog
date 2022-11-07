namespace Common.Handlers;

public static class Base64Handler
{
    public static bool IsValueValid(string base64Value) {
        
        if(string.IsNullOrEmpty(base64Value)) {
            return false;
        }

        var buffer = new Span<byte>(new byte[base64Value.Length]);
        
        return Convert.TryFromBase64String(base64Value, buffer, out var bytesParsed);
    }
}
