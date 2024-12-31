using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public float maxHealth=100;
    public float health;
    public bool canTakeDamage= true;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        health= maxHealth;
    }

    
    public void TakeDamage(float damage)
    {
        if (canTakeDamage)
        {
            health -= damage;
            //play hurt anim

            anim.SetBool("Damage", true);
            if (health <= 0)
            {
                GetComponent<PolygonCollider2D>().enabled = false;
                GetComponentInParent<GatherInput>().DisableControls();
                Debug.Log("Player is dead");
            }

            StartCoroutine(DamagePrevention());
        }
    }
    private IEnumerator DamagePrevention()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(0.15f);
        if (health > 0)
        {
            canTakeDamage= true;
            anim.SetBool("Damage",false);
        }
        else
        {
            //play death animation
            anim.SetBool("Death", true );
        }
    }
}
