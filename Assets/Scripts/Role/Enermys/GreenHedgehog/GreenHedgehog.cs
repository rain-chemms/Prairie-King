using UnityEngine;

public class GreenHedgehog : EnermyModel
{
    [SerializeField] public float surviveTime = 10.0f;//存活时间,之后释放尖刺
    [SerializeField] private bool overlive = false;
    [SerializeField] private bool canMove = true;
    [SerializeField] private float surviveTimeRecorder = 0.0f;
    [SerializeField] public float directionChangeTime = 2.5f;//方向改变的时间
    private float dirChgTimeRecorder = 0.0f;//方向改变计时器
    [SerializeField] public EnermyModel stickPrefab;//尖刺预制体
    [SerializeField] private bool nowAgentMove = true;//当前是否为智能移动
    [SerializeField] private float moveModelShiftTime = 6.0f;//移动模式切换时间
    [SerializeField] private float moveModelShiftTimeRecorder = 0.0f;
    [SerializeField] public Animator animator;

    new void Start()
    {
        base.Start();
        canMove = true;
        SetRandomMoveDirection();
    }

    new void Update()
    {
        base.Update();
        //FollowPlayer();
        if(!overlive) surviveTimeRecorder += Time.deltaTime;
        moveModelShiftTimeRecorder += Time.deltaTime;
        dirChgTimeRecorder += Time.deltaTime;
        if(CheckDirTimeChange()) SetRandomMoveDirection();
        if(CheckSurviveTime()) AfterSurviveTime();
        if(CheckShiftMoveModelTime()) ShiftMoveModel();
    }

    private bool CheckShiftMoveModelTime()
    {
        if(moveModelShiftTimeRecorder > moveModelShiftTime)
        {
            moveModelShiftTimeRecorder = 0.0f;
            return true;
        }
        else return false;
    }

    public void ShiftMoveModel()
    {
        if(nowAgentMove)//当前为随机移动
        {
            nowAgentMove = false;
        }
        else//当前为智能移动
        {
            nowAgentMove = true;
        }
    }

    //产生尖刺示例
    public void DropStick()
    {
        //生成尖刺
        EnermyModel stick = Instantiate(stickPrefab,transform.position,transform.rotation);
    }

    //存活时间检查
    private bool CheckSurviveTime()
    {
        if(surviveTimeRecorder > surviveTime)
        {
            return true;
        }
        else return false;
    }

    private void AfterSurviveTime()
    {
        canMove = false;
        overlive = true;//死亡,防止计时器超时重复计算
        surviveTimeRecorder = 0.0f;
        canDropProp = false;//死亡后不能掉落道具
        CloseTouchDamage();//关闭接触伤害
        //将自身血量设置为零或者时激活放置尖刺的动画
        animator.SetTrigger("DropStick");//在动画事件中放置尖刺
        DropStick();
    }

    private bool CheckDirTimeChange()
    {
        if(dirChgTimeRecorder > directionChangeTime)
        {
            dirChgTimeRecorder = 0.0f;
            return true;
        }
        else return false;
    }
    //米字方向随机改变moveDirection
    private void SetRandomMoveDirection()
    {
        moveDirection = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        moveDirection.Normalize();
    }

    void FixedUpdate()
    {
        //Move_Agent();
        if(canMove) 
        {
            /*
            if(nowAgentMove)
            {
                Move_Agent();
            }
            else Move();
            */
            Move_Agent();
        }
        else 
        {
            if(rb!=null) 
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
    protected override void OnDeath()
    {
        openTouchDamage = false;//死亡后不能造成接触伤害
        animator.SetTrigger("Death");
        base.OnDeath();
    }
}
