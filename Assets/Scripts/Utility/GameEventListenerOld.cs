using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class GameEventListenerOld : MonoBehaviour
{
    [FormerlySerializedAs("Event")] [Tooltip("Event to register with.")]
    public GameEventOld eventOld;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent Response;

    private void OnEnable()
    {
        eventOld.RegisterListener(this);
    }

    private void OnDisable()
    {
        eventOld.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        Response.Invoke();
    }
}