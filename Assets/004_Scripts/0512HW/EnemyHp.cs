using UnityEngine;

public class EnemyHp : MonoBehaviour, IDamageable
{
    private int maxHp = 3;
    private int currentHp = 0;
    private void Awake()
    {
        currentHp = maxHp;
    }
    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        Debug.Log($"[ {gameObject.name} ] 데미지 입음 , 현재 체력 {currentHp}");
        if(currentHp <= 0)
        {
            Debug.Log($"[ {gameObject.name} ] 사망");
            Destroy(gameObject);
        }
    }
}
