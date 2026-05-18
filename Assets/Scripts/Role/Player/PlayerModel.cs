using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerModel : RoleModel,PlayerValueCaculator,PropTimeRecorder,PropUser
{
    //接口属性实现
    //PropTimeRecorder
    public Dictionary<PropType,float> propTimeRemainder {get;set;} = new Dictionary<PropType,float>();
    //PlayerValueCaculator
    public float baseDamage {get;} = 1f;
    public float baseShootInterval {get;}= 0.33f;
    public float baseMoveForce {get;}= 1000f;
    [Header("玩家属性")]
    [SerializeField] private float shootInterval = 0.5f;//射击间隔
    [SerializeField] private Vector2 shootDirection = Vector2.zero;//射击方向
    [SerializeField] protected bool isShoot = false;//是否正在射击
    [SerializeField] public Animator animator;//动画器
    //damage就是子弹的伤害
    [Header("玩家控制系统")]
    [SerializeField] public InputActionAsset inputSystem;
    private InputActionMap playerControl;
    //移动监听
    private InputAction moveRight;
    private InputAction moveLeft;
    private InputAction moveUp;
    private InputAction moveDown;
    //射击监听
    private InputAction upShoot;
    private InputAction downShoot;
    private InputAction leftShoot;
    private InputAction rightShoot;
    //道具使用监听
    private InputAction useProp;
    //玩家的所有属性都从GameData中获取
    //操纵和保存也直接修改GameData
    private bool InputSystemInit()
    {
        if(inputSystem == null) return false;
        playerControl = inputSystem.FindActionMap("PlayerControl");
        if(playerControl == null) return false;
        playerControl.Enable();
        moveDown = playerControl.FindAction("MoveDown");
        moveLeft = playerControl.FindAction("MoveLeft");
        moveRight = playerControl.FindAction("MoveRight");
        moveUp = playerControl.FindAction("MoveUp");
        upShoot = playerControl.FindAction("UpShoot");
        downShoot = playerControl.FindAction("DownShoot");
        leftShoot = playerControl.FindAction("LeftShoot");
        rightShoot = playerControl.FindAction("RightShoot");
        useProp = playerControl.FindAction("UseProp");
        if(moveDown == null || moveLeft == null || moveRight == null || moveUp == null || upShoot == null || downShoot == null || leftShoot == null || rightShoot == null || useProp == null) return false;
        return true;
    }

    private void InputFuncLink()
    {
        //上下移动
        moveDown.performed += (context) =>
        {
            SetMoveDirection(new Vector2(moveDirection.x,-1.0f));
        };
        moveDown.canceled += (context) =>
        {
            SetMoveDirection(new Vector2(moveDirection.x,0.0f));
        };
        moveUp.performed += (context) =>
        {
            SetMoveDirection(new Vector2(moveDirection.x,1.0f));
        };
        moveUp.canceled += (context) =>
        {
            SetMoveDirection(new Vector2(moveDirection.x,0.0f));
        };
        //左右移动
        moveLeft.performed += (context) =>
        {
            SetMoveDirection(new Vector2(-1.0f,moveDirection.y));
        };
        moveLeft.canceled += (context) =>
        {
            SetMoveDirection(new Vector2(0.0f,moveDirection.y));
        };
        moveRight.performed += (context) =>
        {
            SetMoveDirection(new Vector2(1.0f,moveDirection.y));
        };
        moveRight.canceled += (context) =>
        {
            SetMoveDirection(new Vector2(0.0f,moveDirection.y));
        };
        //上下射击
        upShoot.performed += (context) =>
        {
            SetShootDirection(new Vector2(shootDirection.x,1.0f));
        };
        upShoot.canceled += (context) =>
        {
            SetShootDirection(new Vector2(shootDirection.x,0.0f));
        };
        downShoot.performed += (context) =>
        {
            SetShootDirection(new Vector2(shootDirection.x,-1.0f));
        };
        downShoot.canceled += (context) =>
        {
            SetShootDirection(new Vector2(shootDirection.x,0.0f));
        };
        //左右射击
        leftShoot.performed += (context) =>
        {
            SetShootDirection(new Vector2(-1.0f,shootDirection.y));
        };
        leftShoot.canceled += (context) =>
        {
            SetShootDirection(new Vector2(0.0f,shootDirection.y));
        };
        rightShoot.performed += (context) =>
        {
            SetShootDirection(new Vector2(1.0f,shootDirection.y));
        };
        rightShoot.canceled += (context) =>
        {
            SetShootDirection(new Vector2(0.0f,shootDirection.y));
        };
        //使用道具
        useProp.performed += (context) =>
        {
            UseProp();
        };
    }

    void Start()
    {
        bool validInit = InputSystemInit();
        Debug.Log("[Player Input System Init] => " + validInit);
        if(validInit) InputFuncLink();
    }

    // Update is called once per frame
    new void Update()
    {
        //优先检查数值
        NowDamage();
        NowMoveForce();
        NowShootInterval();
        //道具时间流动
        PropTimeFlow();
        //动画与射击检测
        AnimatorControl();
        CheckShoot();
        Shoot();
        base.Update();
    }

    //玩家移动时触发函数
    void FixedUpdate()
    {
        Move();
    }

    //玩家死亡时触发函数
    protected override void OnDeath()
    {
        openTouchDamage = false;
        animator.SetTrigger("Death");
        //断开输入系统监听
        if(playerControl!=null) playerControl.Disable();
        base.OnDeath();
    }

    //实时进行isShoot判断
    [SerializeField] public Bullet bullet;//子弹预制体
    private void CheckShoot()
    {
        if(shootDirection != Vector2.zero) 
        {
            isShoot = true;
        }
        else 
        {
            isShoot = false;
        }
    }
    //设计实现
    private float shootIntervalRecorder = 0f;
    protected virtual void Shoot()
    {
        if(!isShoot)
        {
            shootIntervalRecorder = shootInterval;
            return;
        }
        shootIntervalRecorder += Time.deltaTime;
        //射击间隔未达到时无法开火
        if(shootIntervalRecorder < shootInterval) return;
        bool isEightDir = false;
        bool isTripleBlt = false;
        if(propTimeRemainder.ContainsKey(PropType.Wheel)) isEightDir = true;
        if(propTimeRemainder.ContainsKey(PropType.ShotGun) || propTimeRemainder.ContainsKey(PropType.Star)) isTripleBlt = true;
        {
            int times = isEightDir ? 8 : 1;
            for(int i = 0; i < times; i++)
            {
                Quaternion dirRotation = Quaternion.identity;
                dirRotation = Quaternion.Euler(0, i * 45f, 0);
                if(isTripleBlt)
                {
                    for(int j = 0; j < 3; j++)
                    {
                        int idx = 0;
                        switch(j)
                        {
                            case 1:
                                idx = 1;
                                break;
                            case 2:
                                idx = -1;
                                break;
                            case 0:
                            default:
                                idx = 0;
                                break;
                        }
                        Quaternion appendDir = Quaternion.Euler(0.0f,idx * 20.0f,0.0f);
                        GenerateBullet(dirRotation * appendDir);
                    }
                }
                else GenerateBullet(dirRotation);
            }
            //重置射击间隔
            shootIntervalRecorder = 0f;
        }
    }

    //产生单个子弹
    private Bullet GenerateBullet(Quaternion rotation)
    {
        //创建子弹
        Bullet newBullet = Instantiate(bullet); 
        //设置子弹数据
        if(bullet!=null) 
        {
            newBullet.transform.position = transform.position;
            newBullet.SetBulletData(rotation * new Vector3(shootDirection.x,0.0f,shootDirection.y),true,5.0f,damage);   
        }
        return newBullet;
    }

    //控制玩家的动画器
    private void AnimatorControl()
    {
        //移动动画控制
        if(moveDirection != Vector2.zero)
        {
            animator.SetBool("IsMove",true);
        }
        else
        {
            animator.SetBool("IsMove",false);
        }
        //射击动画控制
        if(isShoot)
        {
            animator.SetBool("IsShoot",true);
        }
        else
        {
            animator.SetBool("IsShoot",false);
        }
    }

    public void SetShootDirection(Vector2 direction)
    {
        shootDirection = direction;
    }

    //接口的实现
    //PropTimeRecorder接口
    public bool AddPropTime(PropType propType,float addTime)
    {
        if(propTimeRemainder == null) return false;
        if(propTimeRemainder.ContainsKey(propType)) propTimeRemainder[propType] += addTime;
        else propTimeRemainder.Add(propType,addTime);
        return true;
    }

    public bool PropTimeFlow()
    {
        if(propTimeRemainder == null) return false;
        foreach(KeyValuePair<PropType,float> item in propTimeRemainder.ToList())
        {
            propTimeRemainder[item.Key] -= Time.deltaTime;
            if(propTimeRemainder[item.Key] <= 0) propTimeRemainder.Remove(item.Key);
        }
        return true;
    }

    public bool SetPropTime(PropType propType,float time)
    {
        if(propTimeRemainder == null) return false;
        if(propTimeRemainder.ContainsKey(propType)) propTimeRemainder[propType] = time;
        else propTimeRemainder.Add(propType,time);
        return true;
    }
    //PlayerValueCaculator接口
    public float NowDamage()
    {
        //检索强化列表
        damage = baseDamage;
        switch(GameData.bullet)
        {
            case BulletType.Bullet_1:
                damage += 1.0f;
                break;
            case BulletType.Bullet_2:
                damage += 2.0f;
                break;
            case BulletType.Bullet_3:
                damage += 5.0f;
                break;
            case BulletType.Bullet_4:
                damage += 11.0f;
                break;
            case BulletType.None:
            default:
                break;
        }
        return damage;
    }

    public float NowMoveForce()
    {
        moveForce = baseMoveForce;
        float rate = 1.0f;//提速倍率
        //检索强化列表
        switch(GameData.boots)
        {
            case BootsType.Boots_1:
                rate += 0.25f;
                break;
            case BootsType.Boots_2:
                rate += 0.5f;
                break;
            case BootsType.None:
            default:
                break;
        }
        //Coffee道具
        if(propTimeRemainder.ContainsKey(PropType.Coffee))
        {
            if(rate < 1.5f) rate = 1.5f;
        }
        //Tomb道具
        if(propTimeRemainder.ContainsKey(PropType.Tomb))
        {
            rate += 0.75f;
        }
        moveForce *= rate;
        return moveForce;
    }

    public float NowShootInterval()
    {
        shootInterval = baseShootInterval;
        float rate = 1.0f;//Timeout倍率
        //检索强化列表
        switch(GameData.weaponUp)
        {
            case WeaponUpType.WeaponUp_1:
                rate += 0.25f;
                break;
            case WeaponUpType.WeaponUp_2:
                rate += 0.5f;
                break;
            case WeaponUpType.WeaponUp_3:
                rate += 0.75f;
                break;
            case WeaponUpType.WeaponUp_4:
                rate += 1.0f;
                break;
            case WeaponUpType.None:
            default:
                break;
        }
        //MachineGun道具
        if(propTimeRemainder.ContainsKey(PropType.MachineGun))
        {
            if(rate < 3.0f) rate = 3.0f;
        }
        shootInterval /= rate;
        return shootInterval;
    }
    //PropUser接口
    public PropType GetBackPackPropType()
    {
        return GameData.prop;
    }
    //道具使用器
    public void UseProp()
    {
        if(GameData.prop == PropType.None) return;
        //创建一个空物体并触发使用效果,之后删除该游戏物体
        GameObject propTemp = new GameObject("PropTemp");
        Prop prop = propTemp.AddComponent<Prop>();
        prop.propType = GetBackPackPropType();
        prop.EffectOnPlayer(this);
        //输出使用效果
        Debug.Log("[PlayerModel PropUser]:"+"Use Prop:"+prop.propType);
        Destroy(propTemp);
        //重置道具
        GameData.prop = PropType.None;
    }
}
