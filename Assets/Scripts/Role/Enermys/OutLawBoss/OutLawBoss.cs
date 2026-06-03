using System;
using UnityEngine;

public class OutLawBoss : EnermyModel
{
    [SerializeField] public float moveToRestChangeTime;//改变移动状态的时间
    [SerializeField] public float restToMoveChangeTime;//改变移动状态的时间
    [SerializeField] private bool canMove = true;//是否可以移动
    [SerializeField] private float moveTimeRecorder = 0;//时间记录器
    [SerializeField] public Animator animator;
    [SerializeField] public float shootInterval;//Boss攻击间隔
    [SerializeField] private float shootTimeRecorder = 0;
    [SerializeField] private bool canShoot = true;//是否可以攻击
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private BossDrop bossDropPrefab;
    new void Start()
    {
        base.Start();
        animator?.SetBool("IsMove",canMove);//设置动画
        animator?.SetBool("IsShoot",canShoot);
    }
    
    new void Update()
    {
        //移动时间流动变化
        moveTimeRecorder += Time.deltaTime;
        float timeSet = 0.0f; 
        if(canMove)
        {
            timeSet = moveToRestChangeTime;
        }
        else
        {
            timeSet = restToMoveChangeTime;
        }
        if(moveTimeRecorder > timeSet)
        {
            moveTimeRecorder = 0;
            canMove = !canMove;
            canShoot = !canShoot;
            animator?.SetBool("IsMove",canMove);//设置动画
        }
        //射击时间间隔流动
        shootTimeRecorder += Time.deltaTime;
        animator?.SetBool("IsShoot",canShoot);
        if(shootInterval != 0) animator?.SetFloat("ShootSpeed",1.0f/shootInterval);
        if(shootTimeRecorder > shootInterval)
        {
            shootTimeRecorder = 0;
            if(canShoot && !IsDeath()) GenerateBullet();
        }
        base.Update();
        //FollowPlayer();
    }

    public void GenerateBullet()
    {
        //生成子弹
        //Vector3 pos = transform.position;
        //pos.y += 1.5f;
        Bullet newbt = Instantiate(bulletPrefab,null);
        newbt.transform.position = this.transform.position + new Vector3(0.0f,1.0f,0.0f);
        newbt.SetBulletData(FollowPlayer_NotChangeMoveDirection()-this.transform.position,false,5.0f,this.damage,true,bulletSpeed);
        newbt.OpenAllCollider();
        AudioManager.instance?.TriggerShootEffect();
    }

    void FixedUpdate()
    {
        //状态切换检测
        DirectionChange();
        if(!IsDeath()) Move();
    }

    public void DirectionChange()
    {
        if(canMove)
        {
            //改变移动方向
            Vector3 tar = FollowPlayer_NotChangeMoveDirection();
            if(tar.x > rb.position.x) moveDirection = new Vector3(1.0f,0.0f,0.0f);
            else if(tar.x < rb.position.x) moveDirection = new Vector3(-1.0f,0.0f,0.0f);
            else moveDirection = Vector2.zero;
        }
        else
        {
            moveDirection = Vector2.zero;//停止移动
        }
    }

    protected override void OnDeath()
    {
        openTouchDamage = false;//死亡后不能造成接触伤害
        animator?.SetTrigger("Death");
        canMove = false;
        canShoot = false;
        //产生BossDrop可拾取道具,用于触发下一章节的加载
        BossDrop drop = Instantiate(bossDropPrefab, transform.position, Quaternion.identity);
        base.OnDeath();
    }
}
