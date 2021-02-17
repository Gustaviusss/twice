using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce = 20f;
    public Rigidbody2D rigidBody;
    public Transform feet;
    public LayerMask groundLayers;
    private int _jumpCount = 0;
    private int _stageJumpCount = 0;

    private float _moveX;

    private void Update()
    {
        _moveX = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && (_isGrounded() || _jumpCount <1))
        {
            Jump();
            JumpStage();
        }

        if (_moveX > 0)
        {
            transform.localScale = new Vector3(1f,1f,1f);
        }else if (_moveX < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(_moveX * moveSpeed, rigidBody.velocity.y);
        rigidBody.velocity = movement;
    }

    void Jump()
    {
        Vector2 jump = new Vector2(rigidBody.velocity.x, jumpForce);
        rigidBody.velocity = jump;
        _jumpCount++;
        print(_stageJumpCount);
        
    }

    private void JumpStage()
    {
        if (_isGrounded() && _stageJumpCount <2)
        {
            _stageJumpCount++;
        } 
        else if (_isGrounded() && _stageJumpCount == 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
    }

     private bool _isGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayers);
        if (groundCheck != null)
        {
            _jumpCount = 0;
            return true;
        }

        return false;
    } 
}
