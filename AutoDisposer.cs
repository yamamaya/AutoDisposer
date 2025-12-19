namespace OaktreeLab.Utils.Common {
    /// <summary>
    /// A utility class for automatically disposing properties and fields marked with the <see cref="OwnedAttribute"/> attribute.
    /// </summary>
    public static class AutoDisposer {
        /// <summary>
        /// Searches for properties and fields of the specified object marked with the <see cref="OwnedAttribute"/> attribute, and calls their Dispose method if they implement <see cref="IDisposable"/>. Additionally, sets reference types or Nullable types to null.
        /// </summary>
        /// <param name="obj"></param>
        public static void Dispose( object obj ) {
            if ( obj == null ) {
                return;
            }
            var type = obj.GetType();
            // Search for properties
            var props = type.GetProperties().Where( p => Attribute.IsDefined( p, typeof( OwnedAttribute ) ) );
            foreach ( var prop in props ) {
                var value = prop.GetValue( obj );
                // Dispose if it implements IDisposable
                if ( value is IDisposable disposable ) {
                    try {
                        disposable.Dispose();
                    } catch {
                        // ignore
                    }
                    // Set to null if it's a reference type or Nullable type
                    if ( !prop.PropertyType.IsValueType || Nullable.GetUnderlyingType( prop.PropertyType ) != null ) {
                        try {
                            prop.SetValue( obj, null );
                        } catch {
                            // ignore
                        }
                    }
                }
            }
            // Search for fields
            var fields = type.GetFields().Where( f => Attribute.IsDefined( f, typeof( OwnedAttribute ) ) );
            foreach ( var field in fields ) {
                var value = field.GetValue( obj );
                // Dispose if it implements IDisposable
                if ( value is IDisposable disposable ) {
                    try {
                        disposable.Dispose();
                    } catch {
                        // ignore
                    }
                    // Set to null if it's a reference type or Nullable type
                    if ( !field.FieldType.IsValueType || Nullable.GetUnderlyingType( field.FieldType ) != null ) {
                        try {
                            field.SetValue( obj, null );
                        } catch {
                            // ignore
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Properties or fields marked with this attribute will be disposed by AutoDisposer.Dispose().
    /// </summary>
    [AttributeUsage( AttributeTargets.Property | AttributeTargets.Field )]
    public sealed class OwnedAttribute : Attribute {
    }
}
