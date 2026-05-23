using UnityEngine;

public class GameMachineAnimatorTrigger : MonoBehaviour
{
    [SerializeField] public Animator animator;//控制的动画器
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //尝试自动获取动画器
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }
    public void ButtonCLick()
    {
        animator?.SetTrigger("ButtonClick");
    }
    public void LeverMove()
    {
        animator?.SetTrigger("LeverMove");
    }
}
