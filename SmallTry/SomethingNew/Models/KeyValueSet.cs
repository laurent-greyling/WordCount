namespace SomethingNew.Models
{
    /// <summary>
    /// Object model that will hold the values we set as key value pairs
    /// Make this a generic type so we can specify any type we want.
    /// If it was concrete types we will always set it to the data type we want and can never reuse the code for other types. This will increase 
    /// duplicity of code as we will need keyvalue set for each type we want in future
    /// </summary>
    /// <typeparam name="TKeyType"></typeparam>
    /// <typeparam name="TValueType"></typeparam>
    public class KeyValueSet<TKeyType, TValueType>
    {
        /// <summary>
        /// Key value pair/set
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public KeyValueSet(TKeyType key, TValueType value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Identifier of the key value pair
        /// </summary>
        public TKeyType Key { get; set; }

        /// <summary>
        /// Value associated with the key identifier
        /// </summary>
        public TValueType Value { get; set; }
    }
}
