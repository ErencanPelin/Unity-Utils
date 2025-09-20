# Unity Utils
Some basic unity utils I can re-use across projects

## Installation
1. In the Unity Package Manager Window, click 'Add Package' and 'Add Package by Git Url'. Enter the following: `https://github.com/ErencanPelin/Unity-Utils.git` and click `Add`.
2. This will add the package into your Packages folder.

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