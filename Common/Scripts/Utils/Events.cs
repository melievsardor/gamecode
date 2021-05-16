using UnityEngine.Events;

namespace Common
{
    public class Events
    {
        [System.Serializable] public class EventFadeComplete : UnityEvent<bool> { }
        [System.Serializable] public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> { }
    }


}

