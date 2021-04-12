using SomethingNew.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SomethingNew.Services
{    
    /// <summary>
    /// Options taken into consideration for creating this transaction
    ///     --->> Create temp data structure in memory, on commit add to main store on rollback remove all from temp store
    ///     --->> Could also create a copy on main, and on commit push copy into actual main and on rollback remove copy
    /// Create a transaction where we can add key value store information to a main store on commit or roll back temp if not needed (in error)
    /// Make this also a value type so it can for this task handle the keyvalue store nullable value type
    /// </summary>
    /// <typeparam name="TKeyType"></typeparam>
    /// <typeparam name="TValueType"></typeparam>
    public class TransactionService<TKeyType, TValueType>: IDisposable where TValueType : struct  
    {
        /// <summary>
        /// Current/Temp KeyValueStore of the transaction
        /// The transaction we find ourselves curently in
        /// </summary>
        public KeyValueStore<TKeyType, TValueType> CurrentStore { get; set; }

        /// <summary>
        /// Original/Main KeyValueStore that will be updated and presented
        /// </summary>
        public KeyValueStore<TKeyType, TValueType> MainStore { get; set; }

        /// <summary>
        /// Create a transaction setup for committing or rolling back on error
        /// Main store is eqaul to the mainstore of that specific instance
        /// </summary>
        public TransactionService(KeyValueStore<TKeyType, TValueType> mainStore)
        {
            MainStore = mainStore;
        }

        /// <summary>
        /// Initialise temp store for the use in the transaction
        /// Values will be saved in temp and pushed to main store on commit or removed on rollback
        /// </summary>
        public void Begin()
        {
            CurrentStore =  new KeyValueStore<TKeyType, TValueType>
            {
                KeyValueSets = new List<KeyValueSet<TKeyType, TValueType>>()
            };
        }

        /// <summary>
        /// Committed a transaction
        /// Update main store with temp store values
        /// First commit wins, nested transaction means inner most transaction wins as that wouild probably be your initial commit
        /// </summary>
        /// <returns>True on Success, false on failure</returns>
        public bool Committed() 
        {
            try
            {
                var distinctValues = CurrentStore.KeyValueSets.Where(set => MainStore.KeyValueSets.All(collection => !collection.Key.Equals(set.Key)));
                MainStore.KeyValueSets.AddRange(distinctValues);

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Rollback uncommited sets in transaction to original
        /// User initiated and on purpose
        /// </summary>
        /// <returns>True on Success, false on failure</returns>
        public bool RollBack() 
        {
            try
            {
                Dispose();

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Remove in memory sets from current transaction. 
        /// We use this seperate from rollback as this is not user requested but will automatically happen when in a using.
        /// If used correctly, this will make sure we do not use unecessary memory
        /// </summary>
        public void Dispose()
        {
            CurrentStore.KeyValueSets.Clear();
        }
    }
}