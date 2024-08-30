using UnityEngine;

public class PlayerAnimations : MonoBehaviour {

    private Animator _anim;
    private float _lockedUntil;
    public int _currentState;
    public AnimState _animState;
    public enum AnimState {
        idle,
        running,
        attack,
        reload,
    }
    public float _attackDuration;

    private static readonly int IDLE = Animator.StringToHash("idle");
    private static readonly int RUNNING = Animator.StringToHash("running");
    private static readonly int ATTACK = Animator.StringToHash("attack");
    private void Awake() {
        _anim = GetComponentInChildren<Animator>();
    }

    private void Update() {
        var state = GetState();
        if (state == _currentState) return;
        _anim.CrossFade(state, 0, 0);
        _currentState = state;
    }

    public int GetState() {
        if (Time.time < _lockedUntil) return _currentState;

        if (_animState == AnimState.attack) return LockState(ATTACK, _attackDuration);
        if (_animState == AnimState.running) return RUNNING;
        if (_animState == AnimState.idle) return IDLE;
        return IDLE;
        int LockState(int s, float t) {
            _lockedUntil = Time.time + t;
            return s;
        }

    }

    public void ChangeState(AnimState state) {
        _animState = state;
    }


}
