# Utils
This folder contains small scripts and utilities that are drag and drop, and usually work independently.
When using [UInit](https://github.com/ErencanPelin/UInit), adding any of these utils will add the folder to your local
`/Scripts/Utils` folder.

## Documentation
### Observable\<T>
A generic Observable implementation. Let's you subscribe to value changes through `UnityEvents` .

```cs
private Observable<int> CurrentLevel = new Observable<int>(5);

private void OnEnable()
{
    // subscribe to value changes
    CurrentLevel.AddListener(OnCurrentLevelChanged);
}

private void OnDisable()
{
    // unsubscribe from value changes
    CurrentLevel.RemoveListener(OnCurrentLevelChanged);
}

private void Awake()
{
    // Setting value silently doesn't invoke the callback
    CurrentLevel.SetValueSilently(10);

    // This will invoke the callback
    CurrentLevel.Value = 20;

    // reading the value:
    if (CurrentLevel.Value > 100)
        Debug.Log("You awesome!");
}


// Invoked whenever the value of CurrentLevel is changed
private void OnCurrentLevelChanged(int newLevelValue)
{
    Debug.Log($"New Level: {newLevelValue}");
}

```

### ServiceLocator
Easy way to create global services. This is better than using Singletons and Static classes for a number of reasons - ask AI if you're interested to know why.
```cs
/* 
Sample service. You should define services using Interfaces as you can abstract the implementation with the contract provided by the ServiceLocator that way.
*/
public interface IGameSaveService
{
    public void SaveGame();
}

// Sample service implementation
public class GameSaveServiceImpl : IGameSaveService
{
    public void SaveGame()
    {
        ...
    }
}

// When you register services
public void Awake()
{
    ServiceLocator.Instance.Register<IGameSaveService>(new GameSaveServiceImpl());
}

// When you need to get the service
public void OnSaveButtonPressed()
{
    ServiceLocator.Instance.Resolve<IGameSaveService>().SaveGame();
}

// De-registering services
public void OnDestroy()
{
    ServiceLocator.Instance.Unregister<IGameSaveService>();
}
```

### Dependency Injection
A pragmatic C# way to setup dependency injection among components of a gameObject as well as its children gameObjects without 
any overly fancy, or heavy DI frameworks.
```csharp
// Provider component, lives on PlayerRoot
public class PlayerInventoryProvider : MonoBehaviour, IProvider<IPlayerInventory>
{
    private void Awake()
    {
        Provide(new PlayerInventory());
    }

    public void Provide(IPlayerInventory data)
    {
        //apply dependencies to any component that requires
        foreach (var dependency in GetComponentsInChildren<IRequire<IPlayerInventory>>(true))
        {
            dependency.SetRef(data);
        }
    }
}

// Dependent component, lives anywhere in the Player Object's hierarchy
public PlayerEquipmentController : MonoBehaviour, IRequire<IPlayerInventory>
{
    private IPlayerInventory playerInventory;
    
    public void SetRef(IPlayerInventory data) => playerInventory = data;
    
    public void Awake()
    {
        // NOTE: Since our DI occurs in Awake(), its not safe to access playerInventory here.
        // As recommened by Unity, DI should occur in Awake() and any usage should be in Start()
    }
}
```

### Bootstrapper
```csharp
public class ServicesBootstrap : MonoBehaviour, IBootstrapper
{
    // Lets you control what order bootstrapping occurs in
    // Especially useful if one bootstrapper relies on another to be strapped first
    public int Order => -1;

    public void Bootstrap()
    {
        Debug.Log("Running services bootstrap");

        //Notitications
        ServiceLocator.Instance.Register<INotificationService>(new NotificationService());
    }

    // Important to undo whatever actions were done in Bootstrap here to dispose of resources correctly
    public void Unstrap()
    {
        ServiceLocator.Instance.Unregister<INotificationService>();
    }
}
```