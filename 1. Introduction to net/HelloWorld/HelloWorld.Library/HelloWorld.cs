using System;

namespace HelloWorld.Library
{
    public static class HelloWorld
    {
        /// <summary>
        /// The method returns formatted string {Time} Hello {name}!
        /// </summary>
        /// <param name="name">Name for the string</param>
        /// <returns>Formatted string</returns>
        public static string GetHello(string name)
        {
            var currentTime = string.Format("{0:HH:mm:ss}", DateTime.Now);
            return $"{currentTime} Hello, {name}!";
        }
    }
}
