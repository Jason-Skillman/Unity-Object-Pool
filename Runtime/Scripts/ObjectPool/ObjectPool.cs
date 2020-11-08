using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures.Extensions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DataStructures.Pool {
	public class ObjectPool<T> where T : MonoBehaviour, IPoolable {
		
		private readonly List<T> pool;
		private readonly T prefab;

		/// <summary>
		/// The amount of objects in the pool.
		/// </summary>
		public int Count => pool.Count;
		
		public T[] ToArray => pool.ToArray();

		/// <param name="prefab">The prefab reference to use when instantiating objects in the pool.</param>
		public ObjectPool(T prefab) {
			pool = new List<T>();
			this.prefab = prefab;
		}
		
		/// <summary>
		/// Retrieves an available object in the pool or creates a new one.
		/// </summary>
		/// <param name="predicate">Optional predicate to filter the returned object.</param>
		/// <returns></returns>
		public T GetItem(Func<T, bool> predicate = null) {
			T item;
			
			//Check the object pool for an available object
			if(predicate == null) item = pool.FirstOrDefault(i => i.IsAvailable);
			else item = pool.FirstOrDefault(i => i.IsAvailable && predicate.Invoke(i));

			//If no object in pool is free, create a new one
			if(item == null) {
				item = prefab.Instantiate();
				
				pool.Add(item);
			}

			item.IsAvailable = false;
			item.OnPoolActivate();
			
			return item;
		}

		/// <summary>
		/// Deactivates the item and makes it available again.
		/// </summary>
		/// <param name="itemToDeactivate"></param>
		public void Remove(T itemToDeactivate) {
			T item = pool.FirstOrDefault(i => i == itemToDeactivate);
			
			//Block flow if the item does not exist in the pool.
			if(item == null) return;
			
			item.IsAvailable = true;
			item.OnPoolDeactivate();
		}

		/// <summary>
		/// Clears all objects in the pool.
		/// </summary>
		public void Clear() {
			foreach(T obj in pool)
				Object.Destroy(obj);
			pool.Clear();
		}
		
	}
}