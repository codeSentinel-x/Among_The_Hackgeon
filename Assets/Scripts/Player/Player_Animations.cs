using UnityEngine;

public class Player_Animations : MonoBehaviour
{

    private Animator anim;
    private float lockedUntil;
    public int currentState;
    public AnimState animState;
    public enum AnimState{
        idle,
        running,
        jumping, 
        atack
    }
    public float atackDuration;
    public float jumpDuration;

    private Ground ground;
    private static readonly int IDLE = Animator.StringToHash("idle");
    private static readonly int RUNNING = Animator.StringToHash("running");
    private static readonly int ATACK = Animator.StringToHash("atack");
    private static readonly int JUMPING = Animator.StringToHash("jumping");
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        ground = GetComponent<Ground>();
    }
    
    private void Update()
    {
        var state = GetState();
        if (state == currentState) return;
        anim.CrossFade(state, 0, 0);
        currentState = state;
    }

    public int GetState(){
        if (Time.time < lockedUntil) return currentState;

        if (animState == AnimState.atack) return LockState(ATACK, atackDuration);
        if (animState == AnimState.jumping) return JUMPING;
        if (animState == AnimState.running && ground.OnGround) return RUNNING;
        if(animState == AnimState.idle && ground.OnGround) return IDLE;
        return JUMPING;
        int LockState(int s, float t){
            lockedUntil = Time.time + t;
            return s;
        } 

    }
    
    public void ChangeState(AnimState state){
        animState = state;
    }   

 
}
