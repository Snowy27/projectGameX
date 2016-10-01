using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using NSubstitute;

public class PlayerHealthTest {

	[Test]
	public void SetupTest() {
		
		PlayerHealthController playerHealth = GetControllerMock ();
		Assert.AreEqual (playerHealth.currentHealth, playerHealth.healthDefaults.maxHealth);
		Assert.AreEqual (playerHealth.state, PlayerState.FullHealth);

	}

	[Test]
	public void TakeDamageTest() {
		//Arrange
		PlayerHealthController playerHealth = GetControllerMock ();

		playerHealth.TakeDamage (5);

		Assert.AreEqual (playerHealth.currentHealth, playerHealth.healthDefaults.maxHealth - 5);
		Assert.AreEqual (playerHealth.state, PlayerState.Wounded);

	}

	[Test]
	public void TakeCriticalDamageTest() {
		//Arrange
		PlayerHealthController playerHealth = GetControllerMock ();

		playerHealth.TakeDamage (playerHealth.healthDefaults.maxHealth - playerHealth.healthDefaults.criticalHealth);

		Assert.AreEqual (playerHealth.currentHealth, playerHealth.healthDefaults.criticalHealth);
		Assert.AreEqual (playerHealth.state, PlayerState.CriticalyWounded);

	}

	[Test]
	public void TakeFatalDamageTest() {
		//Arrange
		PlayerHealthController playerHealth = GetControllerMock ();

		playerHealth.TakeDamage (playerHealth.healthDefaults.maxHealth);

		Assert.AreEqual (playerHealth.currentHealth, 0);
		Assert.AreEqual (playerHealth.state, PlayerState.Dead);

	}

	[Test]
	public void HealFullHealth() {
		PlayerHealthController playerHealth = GetControllerMock ();

		playerHealth.TakeDamage (5);
		playerHealth.HealDamage (5);

		Assert.AreEqual (playerHealth.currentHealth, playerHealth.healthDefaults.maxHealth);
		Assert.AreEqual (playerHealth.state, PlayerState.FullHealth);
	}

	[Test]
	public void RegenHealth() {
		PlayerHealthController playerHealth = GetControllerMock ();

		playerHealth.TakeDamage (5);
		playerHealth.RegenHealth (1);

		Assert.AreEqual (playerHealth.currentHealth, playerHealth.healthDefaults.maxHealth - 5 + playerHealth.regenDefaults.regenSpeed);
	}

	[Test]
	public void NotRegenHealth() {
		PlayerHealthController playerHealth = GetControllerMock ();
		playerHealth.CanRegen (1).Returns (false);

		playerHealth.TakeDamage (5);
		playerHealth.RegenHealth (1);

		Assert.AreEqual (playerHealth.currentHealth, playerHealth.healthDefaults.maxHealth - 5);
	}

	private PlayerHealthController GetControllerMock ()
	{
		var playerHealth = Substitute.For<PlayerHealthController> ();
		playerHealth.Init ();
		playerHealth.CanRegen (1).Returns (true);
		return playerHealth;
	}
		
}
