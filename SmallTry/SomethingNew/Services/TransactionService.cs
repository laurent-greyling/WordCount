using SomethingNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SomethingNew.Services
{    
    public class TransactionService<TKeyType, TValueType> : IDisposable
    {
        /// <summary>
        /// Current KeyValueStore of the transaction
        /// </summary>
        public KeyValueStore<TKeyType, TValueType> CurrentStore { get; set; }

        /// <summary>
        /// Original/Main KeyValueStore that will be updated and presented
        /// </summary>
        public KeyValueStore<TKeyType, TValueType> MainStore { get; set; }

        /// <summary>
        /// Create a transaction setup for committing or rolling back on error
        /// </summary>
        public TransactionService(KeyValueStore<TKeyType, TValueType> mainStore)
        {
            MainStore = mainStore;
        }

        /// <summary>
        /// Initialise temp store for the use in the transaction
        /// Values will be saved in temp and pushed to main store on commit
        /// </summary>
        public void Begin()
        {
            CurrentStore =  new KeyValueStore<TKeyType, TValueType>
            {
                KeyValueSets = new List<KeyValueSet<TKeyType, TValueType>>()
            };
        }

        /// <summary>
        /// Commit a transaction
        /// Update main store with temp store values
        /// </summary>
        public void Commit() 
        {
            var distinctValues = CurrentStore.KeyValueSets.Where(set => MainStore.KeyValueSets.All(collection => !collection.Key.Equals(set.Key)));
            MainStore.KeyValueSets.AddRange(distinctValues);
        }

        /// <summary>
        /// Rollback uncommited sets in transaction to original
        /// </summary>
        public void RollBack() 
        {
            Dispose();
        }

        public void Dispose()
        {
            CurrentStore.KeyValueSets.Clear();
        }
    }
}