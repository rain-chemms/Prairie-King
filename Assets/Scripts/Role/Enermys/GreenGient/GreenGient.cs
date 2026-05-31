using UnityEngine;

public class GreenGient : EnermyModel
{
    [SerializeField] public Animator animator;
    [Header("开局的无敌时间的时长")]
    [SerializeField] public float invulnerableTime = 2.0f;//开始时的无敌时间
    [SerializeField] private float timeRecorder = 0.0f;
    [SerializeField] public Transform invulnerableSphere;//无敌球外观

    new void Start()
    {
        base.Start();
        timeRecorder = 0.0f;//重置时间记录器
    }

    new void Update()
    {
        base.Update();
        FollowPlayer();
        timeRecorder += Time.deltaTime;
        //实时更新无敌时间及外观的显示
        if(timeRecorder >= invulnerableTime)
        {
            invulnerableSphere?.gameObject?.SetActive(false);
            isInvulnerable = false;
        }
        else
        {
            invulnerableSphere?.gameObject?.SetActive(true);
            isInvulnerable = true;
        }
    }

    void FixedUpdate()
    {
        Move_Agent();
        //Move();
    }
    protected override void OnDeath()
    {
        openTouchDamage = false;//死亡后不能造成接触伤害
        animator.SetTrigger("Death");
        base.OnDeath();
    }
}
