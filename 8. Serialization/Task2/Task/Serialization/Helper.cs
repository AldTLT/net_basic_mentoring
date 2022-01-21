namespace Task.Serialization
{
    internal static class Helper
    {
        /// <summary>
        /// Set value of a properties from source to destination object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public static void SetProperty<T>(T source, T destination)
        {
            if (source == null || destination == null)
            {
                return;
            }

            foreach (var property in source.GetType().GetProperties())
            {
                var pi = destination.GetType().GetProperty(property.Name);
                pi.SetValue(destination, property.GetValue(source));
            }
        }
    }
}
