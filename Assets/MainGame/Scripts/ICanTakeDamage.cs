using UnityEngine;

public interface ICanTakeDamage
{
    void TakeDamage(int damageAmount, Vector2 HitPoint, GameObject hitDirection);
}
