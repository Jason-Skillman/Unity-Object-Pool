namespace DataStructures.Pool {
	public interface IPoolable {
		/// <summary>
		/// Used by the object pool to keep track what objects are available.
		/// </summary>
		bool IsAvailable { get; set; }

		/// <summary>
		/// Called once when the object is activated.
		/// </summary>
		void OnPoolActivate();
		/// <summary>
		/// Called once when the object is deactivated.
		/// </summary>
		void OnPoolDeactivate();
	}
}