using UnityEngine;

public class Player : Humanoid 
{
	private int spinAttackDamage = 10;

	public override int Attack()
	{
		return attackDamage + spinAttackDamage;
	}
}
