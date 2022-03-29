using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEventOld : ScriptableObject
{
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<GameEventListenerOld> eventListeners = 
        new List<GameEventListenerOld>();

    public void Raise()
    {
        for(int i = eventListeners.Count -1; i >= 0; i--)
            eventListeners[i].OnEventRaised();
    }

    public void RegisterListener(GameEventListenerOld listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(GameEventListenerOld listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}