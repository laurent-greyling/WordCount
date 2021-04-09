using SomethingNew.Models;
using System.Collections.Generic;
using System.Linq;

namespace SomethingNew.Services
{
    public class KeyValueStore<TKeyType, TValueType>
    {
        /// <summary>
        /// List of Key value sets
        /// https://docs.microsoft.com/en-us/dotnet/api/system.collections.objectmodel.collection-1.count?redirectedfrom=MSDN&view=net-5.0#System_Collections_ObjectModel_Collection_1_Count
        /// </summary>
        public List<KeyValueSet<TKeyType, TValueType>> KeyValueSets { get; set; } = new List<KeyValueSet<TKeyType, TValueType>>();


        /// <summary>
        /// Add or update a value to the collection of key value sets
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set(TKeyType key, TValueType value)
        {
            var keyValueSet = new KeyValueSet<TKeyType, TValueType>(key, value);

            if (IsKeyPresent(key))
            {
                Update(key, value);
                return;
            }

            KeyValueSets.Add(keyValueSet);
        }

        /// <summary>
        /// Remove a value from the list of key value sets based on key identifier
        /// </summary>
        /// <param name="key"></param>
        public void Delete(TKeyType key)
        {
            var keyValueToRemove = KeyValueSets.FirstOrDefault(set => set.Key.Equals(key));
            KeyValueSets.Remove(keyValueToRemove);
        }

        /// <summary>
        /// Return the value of a specified key identifier
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValueType Get(TKeyType key)
        {
            var keyValueSet = KeyValueSets.FirstOrDefault(set => set.Key.Equals(key));
            return keyValueSet == null ? default : keyValueSet.Value;
        }

        /// <summary>
        /// Return count of same values
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Count(TValueType value) => KeyValueSets.Count(set => set.Value.Equals(value));

        /// <summary>
        /// Clear key values from memory and say bye
        /// </summary>
        /// <returns></returns>
        public string End() 
        {
            KeyValueSets.Clear();
            return "Bye!";
        }

        /// <summary>
        /// Check if the key exist in the collection set
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsKeyPresent(TKeyType key) => KeyValueSets.Any(set => set.Key.Equals(key));

        /// <summary>
        /// Update the value of a specified key identifier
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void Update(TKeyType key, TValueType value)
        {
            var keyToUpdate = KeyValueSets.FirstOrDefault(set => set.Key.Equals(key));
            keyToUpdate.Value = value;
        }
    }
}
