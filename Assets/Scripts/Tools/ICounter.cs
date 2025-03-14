public interface ICounter
{
    event System.Action CountersUpdated;
    int ActiveObjectsCount { get; }
    int TotalSpawned { get; }
    int TotalCreatedObjects { get; }
}