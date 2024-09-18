using System.Reflection;

namespace Orders.Borders.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static bool TryUpdate<TOriginal, TUpdate>(this TOriginal original, TUpdate update, IEnumerable<string> properties, bool ignoreNullsOnUpdate = false)
        {
            var hasChanges = false;

            foreach (var property in properties)
            {
                var originalProperty = original!.GetType().GetProperty(property);
                var updateProperty = update!.GetType().GetProperty(property);

                if (originalProperty is null || updateProperty is null)
                    continue;

                var updateValue = updateProperty.GetValue(update);

                var originalPropertyIsNotNullable = Nullable.GetUnderlyingType(originalProperty.PropertyType) == null;

                if (updateValue is null && (originalPropertyIsNotNullable || ignoreNullsOnUpdate))
                    continue;

                var originalValue = originalProperty.GetValue(original);

                if (!Equals(originalValue, updateValue))
                {
                    originalProperty.SetValue(original, updateValue);

                    hasChanges = true;
                }
            }

            return hasChanges;
        }

        public static TOriginal ApplyMatchingProperties<TOriginal, TUpdate>(this TOriginal original, TUpdate update)
        {
            if (Equals(original, default(TOriginal)))
                throw new ArgumentNullException(nameof(original));
            if (Equals(update, default(TUpdate)))
                throw new ArgumentNullException(nameof(update));

            PropertyInfo[] originalProperties = typeof(TOriginal).GetProperties();
            PropertyInfo[] updateProperties = typeof(TUpdate).GetProperties();

            foreach (PropertyInfo originalProp in originalProperties)
            {
                PropertyInfo? updateProp = Array.Find(updateProperties, p => p.Name == originalProp.Name && p.PropertyType == originalProp.PropertyType);

                if (updateProp != null)
                {
                    originalProp.SetValue(original, updateProp.GetValue(update));
                }
            }

            return original;
        }
    }
}
