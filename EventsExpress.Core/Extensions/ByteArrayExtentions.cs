using System;

namespace EventsExpress.Core.Extensions
{
    public static class ByteArrayExtentions
    {
        public static string ToRenderablePictureString(this byte[] array)
        {
            if (array == null)
            {
                return string.Empty;
            }

            return "data:image; base64," + Convert.ToBase64String(array);
        }
    }
}
