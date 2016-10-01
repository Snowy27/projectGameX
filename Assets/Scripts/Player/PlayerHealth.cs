using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {FullHealth, Wounded, CriticalyWounded, Dead};

[System.Serializable]
public class Health 
{
	public int maxHealth = 100;
	public int criticalHealth = 25;
}

[System.Serializable]
public class Regen 
{
	public float regenDelay = 2.0f;
	public int regenSpeed = 2;
	public float regenFrequency = 1.0f;
}

public class PlayerHealth : MonoBehaviour 
{
	public PlayerHealthController controller;

	void Start ()
	{
		controller.Init ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (controller.IsWounded ()) {
			controller.RegenHealth ();
		}
		if(Input.GetButtonDown("Fire1"))
		{
			controller.TakeDamage (5);
		}
	}
}
	
[System.Serializable]
public class PlayerHealthController
{
	public Health healthDefaults;
	public Regen regenDefaults;


	[SerializeField]
	private PlayerState _state;
	public PlayerState state { get {return _state;}}
	[SerializeField]
	private int _currentHealth;
	public int currentHealth { get {return _currentHealth;}}
	private float _lastDamageTime;
	private float _lastHealTime;

	public void Init() {
		healthDefaults = new Health ();
		regenDefaults = new Regen ();
		_currentHealth = healthDefaults.maxHealth;
		SetState ();
	}

	public void TakeDamage (int damage)
	{
		_currentHealth -= damage;
		_lastDamageTime = Time.time;
		SetState ();
	}

	public void HealDamage (int heal)
	{
		_currentHealth += heal;
		_lastHealTime = Time.time;
		SetState ();
	}

	public bool IsWounded ()
	{
		return _state == PlayerState.Wounded || _state == PlayerState.CriticalyWounded;
	}

	public void RegenHealth ()
	{
		if (CanRegen ())
		{
			HealDamage (regenDefaults.regenSpeed);
		}
	}

	virtual public bool CanRegen ()
	{
		return (Time.time - _lastDamageTime) >= regenDefaults.regenDelay && 
			(Time.time - _lastHealTime) >= regenDefaults.regenFrequency;
	}

	private void SetState()
	{
		if (_currentHealth <= 0) 
		{
			_state = PlayerState.Dead;
			return;
		}
		if (_currentHealth <= healthDefaults.criticalHealth)
		{
			_state = PlayerState.CriticalyWounded;
			return;
		}
		if (_currentHealth < healthDefaults.maxHealth)
		{
			_state = PlayerState.Wounded;
			return;
		}
		_state = PlayerState.FullHealth;
	}
}
