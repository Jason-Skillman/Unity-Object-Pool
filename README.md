# Unity-Object-Pool
An object pool data structure for Unity. Manages instances of game objects and activates them when needed saving performance.

## How to install
This package can be installed through the Unity `Package Manager`

Open up the package manager `Window/Package Manager` and click on `Add package from git URL...`.

![unity_package_manager_git_drop_down](Documentation~/images/unity_package_manager_git_drop_down.png)

Paste in this repository's url.

`https://github.com/Jason-Skillman/Unity-Object-Pool.git`

![unity_package_manager_git_with_url](Documentation~/images/unity_package_manager_git_with_url.png)

Click `Add` and the package will be installed in your project.

---
**NOTE:** For Unity version 2019.2 or lower

If you are using Unity 2019.2 or lower than you will not be able to install the package with the above method. Here are a few other ways to install the package.
1. You can clone this git repository into your project's `Packages` folder.
1. Another alternative would be to download this package from GitHub as a zip file. Unzip and in the `Package Manager` click on `Add package from disk...` and select the package's root folder.

---

### Git submodule
Alternatively you can also install this package as a git submodule.

```console
$ git submodule add https://github.com/Jason-Skillman/Unity-Object-Pool.git Packages/Unity-Object-Pool
```

## Object pool
The object pool class can be used to manage gameobjects and prefabs in the hirachery at runtime. The user can request to get an object from the pool. If no available object exists in the pool a new one will be instantiated. Objects can be activated and deactivated.

### Class definition
`T` must be a `MonoBehaviour` and implements `IPoolable`.

```C#
public class ObjectPool<T> where T : MonoBehaviour, IPoolable {
```

### Constructor
Each object pool needs to have a reference to a prefab. The prefab will be used when creating new objects in the object pool. Note that the prefab instance need to have the `T` component at its root. The `T` component will have to compose its children.

```C#
public ObjectPool(T prefab, Transform parent = null) {
```

Ex. A prefab called `SparkParticle` will have to have a component called `Spark`.

```C#
ObjectPool<Spark> particlePool = new ObjectPool<Spark>(sparkPrefabRef);
```

### Properties
|Return Type|Name|Description|
|---|---|---|
|`int`|`Count`|The amount of objects in the pool.|
|`T[]`|`ToArray`|Returns all of the objects in the pool as an array.|

### Methods
|Return Type|Name|Parameters|Description|
|---|---|---|---|
|`T`|`GetItem`|`Func<T, bool>` predicate|Returns an available object in the pool. A new object will be created if no available objects exist. A predicate can also be used to retreive a specific item in the pool. Ex. The first available object with the smallest child index.|
|`void`|`Remove`|`T` itemToDeactivate|Deactivates the item in the pool.|
|`void`|`Clear`||Clears all objects in the pool.|

### Example
An object pool can be used with a particle manager. The object pool will create particles when needed and reuse old particle objects to save memory. When the particle animation has ended, the particle component can deactivate itself making it available for re-use. When activated again the animation will start.
