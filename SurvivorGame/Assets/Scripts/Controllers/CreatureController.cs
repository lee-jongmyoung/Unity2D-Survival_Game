using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using static Define;

public class CreatureController : MonoBehaviour
{
	[SerializeField]
	public float _speed = 5.0f;

	protected Animator _animator;
	protected SpriteRenderer _sprite;
	public Vector3 _moveDir = Vector3.zero;

	[SerializeField]
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

	[SerializeField]
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

	public MoveDir GetDirFromVec(Vector3 dir)
    {
		if (dir.x > 0)
			return MoveDir.Right;
		else if (dir.x < 0)
			return MoveDir.Left;
		else
			return MoveDir.None;
	}

	protected virtual void UpdateAnimation()
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

	void Start()
	{
		Init();
	}

	void Update()
	{

	}

	protected virtual void Init()
	{
		_animator = GetComponent<Animator>();
		_sprite = GetComponent<SpriteRenderer>();
	}


	protected virtual void UpdateMoving()
	{
		if (_dir == MoveDir.None)
		{
			State = PlayerState.Idle;
			return;
		}

		State = PlayerState.Moving;

		float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw("Vertical");

		_moveDir = new Vector3(x, y, 0);

		transform.position += _moveDir.normalized * _speed * Time.deltaTime;
	}


	protected virtual void UpdateSkill()
	{

	}

	protected virtual void UpdateDead()
	{

	}

	public virtual void OnDamaged()
	{

	}
}
