using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerModel : RoleModel
{
    [Header("玩家属性")]
    [SerializeField] public float shootInterval = 0.3f;//射击间隔
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
        AnimatorControl();
        base.Update();
    }

    //玩家死亡时触发函数
    protected override void OnDeath()
    {
        openTouchDamage = false;
        animator.SetTrigger("Death");
        base.OnDeath();
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
    }
}
