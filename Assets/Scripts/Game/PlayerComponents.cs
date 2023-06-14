using UnityEngine;

public class PlayerComponents : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private const string nameVariable = "Running";
    private bool valueState;
    
    public void ChangeStateRunning()
    {
        animator.SetBool(nameVariable, !valueState);
    }
}
