using UnityEngine;

public class PlayerJumpScript : MonoBehaviour
{
    [SerializeField] TPController TPController;

    bool jump;

    void Start()
    {
        jump = false;
    }

    void Update()
    {
        if (jump)
        {
            TPController.velocity.y = Mathf.Sqrt(TPController.jumpHeight * -2 * TPController.gravity);
        }
    }

    public void StartJumping()
    {
        jump = true;
    }

    public void StopJumping()
    {
        jump = false;
    }
}