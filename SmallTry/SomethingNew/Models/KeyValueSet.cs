namespace SomethingNew.Models
{
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
