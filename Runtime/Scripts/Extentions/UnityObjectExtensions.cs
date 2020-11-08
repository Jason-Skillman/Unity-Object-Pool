using UnityEngine;

namespace DataStructures.Extensions {
	public static class UnityObjectExtensions {

		public static T Instantiate<T>(this Object unityObject, T t) where T : Object {
			return Object.Instantiate(t) as T;
		}

		public static T Instantiate<T>(this T unityObject) where T : Object {
			return Object.Instantiate(unityObject) as T;
		}

	}
}