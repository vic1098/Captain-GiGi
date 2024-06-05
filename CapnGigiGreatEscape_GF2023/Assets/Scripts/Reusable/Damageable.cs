using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    //public GameObject enemyDrop;
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<int, int> healthChanged;
    Animator anim;
    [SerializeField] private bool isInvincible = false;
    public float invincibilityTime = 0.25f;
    private float timeSinceHit = 0;
    [SerializeField] private int _maxHealth = 100;
    
    private void Start()
    {

    }
    
    public int MaxHealth{
        get{
            return _maxHealth;
        } set {
            _maxHealth = value;
        }
    }

    [SerializeField] private int _health = 100;
    public int Health{
        get {
            return _health;
        } set {
            _health = value;
            // Update unity event for the health bar
            healthChanged?.Invoke(_health, MaxHealth);
            // If health is less than 0 
            if(_health <= 0 ){ 
                // Kill the character 
                IsAlive = false;
                /*
                Victor just commented out because was not assigned the object and i was having a compiler error
                GameObject g = Instantiate(enemyDrop, this.transform.position, Quaternion.identity);
                g.transform.position = this.transform.position;
                */
            }
            
        }
    }

    [SerializeField] private bool _isAlive = true;
    public bool IsAlive{
        get{
            return _isAlive;
        } set {
            _isAlive = value;
            anim.SetBool(AnimationStrings.isAlive, value);
        }
    }

    // Also here the velocity should not be changed while this is true but needs to be respected by other physics components (like player controller)
    // Just getting and setting the paramether on the animator
    public bool LockVelocity{
        get{
            return anim.GetBool(AnimationStrings.lockVelocity);
        } set {
            anim.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    void Awake(){
        anim = GetComponent<Animator>();
    }
    
    private void Update(){
        // If is in invincibilty timer 
        if(isInvincible){
            // If timer is finished
            if(timeSinceHit > invincibilityTime){
                // Remove invincibility
                isInvincible = false;
                // Reset the timeSinceHit timer 
                timeSinceHit = 0;
            }
            // Update the timeSinceHit timer 
            timeSinceHit += Time.deltaTime;
        }
    }

    public bool Hit(int damage, Vector2 knockback){
        // If player is alive and is not during the invincibilty timer  
        if(IsAlive && !isInvincible){
            // Take damage 
            Health -= damage;
            // Start the invincibility timer to not be able to be hitted again
            isInvincible = true;
            // Update animator parameter 
            anim.SetTrigger(AnimationStrings.hitTrigger);
            // Lock velocity to have a little freeze effect
            LockVelocity = true;
            // Notify other subscribed components that the damagable was hit to handle the knockback, checking first if is not null
            damageableHit?.Invoke(damage, knockback);
            // Call the unity action and pass the paramethers 
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
            // Able to be hit 
            return true;
        }
        // Unable to be hit
        return false;
    }

    public void Heal(int healthRestore){
        if(IsAlive){
            // Set the maximum heal capacity to not overflow the max health
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            // Update the healing value choosing the smaller value between the heal capacity just calculated and the value of the collectable
            int actualHeal =  Mathf.Min(maxHeal, healthRestore);
            // Update the character health 
            Health += actualHeal;
            // Invoke the unity action and pass the paramethers
            CharacterEvents.characterHealed(gameObject, actualHeal);
        }
    }
    
    public void changeIsAlive()
    {
        // kill the player
        IsAlive = false;
    }
}
