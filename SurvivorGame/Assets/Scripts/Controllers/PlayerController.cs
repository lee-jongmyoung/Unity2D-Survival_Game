using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Define;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float lastHorizontalVector = 1;
    [SerializeField]
    public float lastVerticalVector = 0;

    public Animator _animator;
    public SpriteRenderer _sprite;
    PlayerStat _stat;
    TrailRenderer _trailRenderer;

    //Dash
    public int _dashCount = 0;
    public int dashMaxCount = 0;
    public bool isDashing = false;
    private float _dashChargeTime = 10f;
    private Vector3 _dashingDir;
    [SerializeField] private float _dashSpeed = 14f;
    [SerializeField] private float _dashingTime = 0.2f;
    [SerializeField] public List<Dash> _dashs;

    public int DashMaxCount
    {
        get { return dashMaxCount; }
        set
        {
            dashMaxCount = value;

            for (int i = 0; i < value; i++)
            {
                _dashs[i].gameObject.SetActive(true);
            }
        }
    }

    public Vector3 moveDir = Vector3.zero;

    public PlayerState _state = PlayerState.Idle;
    public PlayerState State
    {
        get { return _state; }
        set
        {
            if (_state == value)
                return;

            _state = value;
            UpdateAnimation();
        }
    }

    public MoveDir _lastDir = MoveDir.Down;
    [SerializeField]
    public MoveDir _dir = MoveDir.Down;
    public MoveDir Dir
    {
        get { return _dir; }
        set
        {
            if (_dir == value)
                return;

            _dir = value;
            if (value != MoveDir.None)
            {
                _lastDir = value;
            }

            UpdateAnimation();
        }
    }

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
        _stat = gameObject.GetComponent<PlayerStat>();
        _trailRenderer = GetComponent<TrailRenderer>();
        DashMaxCount = 5;
        _dashCount = DashMaxCount;
    }
    void Update()
    {
        GetDirInput();
        UpdateMoving();
    }
    void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    // 키보드 입력을 받아서 방향을 설정
    void GetDirInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Dir = MoveDir.Up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Dir = MoveDir.Down;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Dir = MoveDir.Left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Dir = MoveDir.Right;
        }
        else
        {
            Dir = MoveDir.None;
        }
    }

    public void UpdateAnimation()
    {
        if (_state == PlayerState.Idle)
        {
            switch (_lastDir)
            {
                case MoveDir.Up:
                    _animator.Play("idle_up");
                    _sprite.flipX = false;
                    break;
                case MoveDir.Down:
                    _animator.Play("idle_down");
                    _sprite.flipX = false;
                    break;
                case MoveDir.Left:
                    _animator.Play("idle_side");
                    _sprite.flipX = false;
                    break;
                case MoveDir.Right:
                    _animator.Play("idle_side");
                    _sprite.flipX = true;
                    break;
            }
        }
        else if (_state == PlayerState.Moving)
        {
            switch (_dir)
            {
                case MoveDir.Up:
                    _animator.Play("walk_up");
                    _sprite.flipX = false;
                    break;
                case MoveDir.Down:
                    _animator.Play("walk_down");
                    _sprite.flipX = false;
                    break;
                case MoveDir.Left:
                    _animator.Play("walk_side");
                    _sprite.flipX = false;
                    break;
                case MoveDir.Right:
                    _animator.Play("walk_side");
                    _sprite.flipX = true;
                    break;
            }
        }
        else if (_state == PlayerState.Skill)
        {

        }
        else
        {

        }
    }

    void UpdateMoving()
    {
        if (_dir == MoveDir.None)
        {
            State = PlayerState.Idle;
            return;
        }

        State = PlayerState.Moving;

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        lastHorizontalVector = x;
        lastVerticalVector = y;
        moveDir = new Vector3(x, y, 0).normalized;

        if (Input.GetKey(KeyCode.Space) && _dashCount > 0 && !isDashing)
        {
            Dash(x,y);
        }

        if (isDashing)
        {
            transform.position += _dashingDir.normalized * _dashSpeed * Time.deltaTime;
            return;
        }

        transform.position += moveDir.normalized * _stat.MoveSpeed * Time.deltaTime;

    }

    private void Dash(float x, float y)
    {
        _dashCount--;
        isDashing = true;
        _trailRenderer.emitting = true;
        _dashingDir = new Vector3(x, y);
        _dashs[_dashCount].IsAvailable = false;

        if (_dashingDir == Vector3.zero)
            _dashingDir = new Vector3(transform.localScale.x, 0);

        StartCoroutine(CoStopDashing());
        StartCoroutine(DashCount());

    }
    IEnumerator DashCount()
    {
        while (true)
        {
            yield return new WaitForSeconds(_dashChargeTime);
            if (dashMaxCount > _dashCount)
            {
                _dashs[_dashCount].IsAvailable = true;
                _dashCount++;
                yield break;
            }
        }
    }

    IEnumerator CoStopDashing()
    {
        yield return new WaitForSeconds(_dashingTime);
        _trailRenderer.emitting = false;
        isDashing = false;
    }
}
