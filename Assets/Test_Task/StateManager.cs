using UnityEngine;
using UniRx;

namespace State {

    public class StateManager : MonoBehaviour {
        public enum State {
            Teaching,
            Playing
        }

        // ReactiveProperty��State���Ǘ�
        public ReactiveProperty<State> CurrentState { get; } = new (State.Teaching);
    
    }




}