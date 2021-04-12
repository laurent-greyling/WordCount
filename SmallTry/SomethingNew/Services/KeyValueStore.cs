using SomethingNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SomethingNew.Services
{
    /// <summary>
    /// Key value store is the business logic for set, get, count keyvalue sets. This is what we will initialise to create a dictionary of KeyValueSets
    /// Set to a value type for TValueType, do this else we cannot make the TValueType nullable and value type will return 0 instead of null as default
    /// Do this mainly as for this task we want to see if value was null and print it
    /// 0 can be a valid input, so if we print key was 0 a user can be confused to whether it is valid value or default (null) value
    /// Generic constraints - https://www.tutorialsteacher.com/csharp/constraints-in-generic-csharp
    /// </summary>
    /// <typeparam name="TKeyType"></typeparam>
    /// <typeparam name="TValueType"></typeparam>
    public class KeyValueStore<TKeyType, TValueType> where TValueType: struct //Struct will make TValueType a value type (int, datetime, char...)
    {
        /// <summary>
        /// Initialize a List of Key value sets
        /// When called it will not through null reference exception
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
        /// If it is not found return null, to do this we set our return type to nullable
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValueType? Get(TKeyType key)
        {
            var keyValueSet = KeyValueSets.FirstOrDefault(set => set.Key.Equals(key));
            return keyValueSet?.Value;
        }

        /// <summary>
        /// Return count of same values
        /// https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.count?view=net-5.0#remarks  (search c# list count o(1))
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Count(TValueType value) => KeyValueSets.Count(set => set.Value.Equals(value));

        /// <summary>
        /// Clear key values from memory and say bye
        /// Clear from memory to not accidentaly keep around and fill up unecessaraly
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
        /// We update after we check for key present as we do not want to add duplicates
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
