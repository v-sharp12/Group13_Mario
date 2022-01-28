using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerController : MonoBehaviour
{
    public characterController player;
    public GameObject fireballProjectile;
    public Color baseColor;
    public Color secColor;
    
    [Header("Powerup Variables and Constraints")]
    public float powDuration = 10;
    
    [Header("Fire Ball Variables")]
    public bool fireFlowerEquipped;
    public float fireTimer;
    public int maxFireballsfired;
    public int currFireballsFired;
    public int instancedFireballs;
    
    [Header("Mushroom Variables")]
    public bool bigMushroomEquipped;
    public float mushTimer;
    
    [Header("Starman Variables")]
    public bool starManEquipped;
    public float starTimer;
    public ParticleSystem starParticle;
    void Start()
    {
        player = GetComponent<characterController>();
        starParticle = transform.Find("Particle System").GetComponent<ParticleSystem>();
        fireFlowerEquipped = false;
        starManEquipped = false;
    }

    // Update is called once per frame
    void Update()
    {
        shootFireball();
        starMan();
    }
    public void shootFireball()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && fireFlowerEquipped && instancedFireballs < 2 || Input.GetKeyDown(KeyCode.RightShift) && fireFlowerEquipped && instancedFireballs < 2 || Input.GetKeyDown(KeyCode.Z) && fireFlowerEquipped && instancedFireballs < 2)
        {
            GameObject projectile = Instantiate(fireballProjectile, player.firePoint.position, Quaternion.identity);
            currFireballsFired = Mathf.Clamp(currFireballsFired += 1, 0, maxFireballsfired);
            instancedFireballs = Mathf.Clamp(instancedFireballs += 1, 0, 2);            
            fireball fireball = projectile.GetComponent<fireball>();
            fireball.player = GetComponent<characterController>();
            if(player.facingRight == false)
            {
                projectile.transform.Rotate(0f, 180f, 0f);
            }
            Rigidbody2D fireballRb = projectile.GetComponent<Rigidbody2D>();
            fireballRb.AddForce(transform.right * 5, ForceMode2D.Impulse);
        }
        if(fireFlowerEquipped)
        {
            fireTimer -= Time.deltaTime;
        }
        else if(!fireFlowerEquipped)
        {
            fireTimer = powDuration;
        }
        if(fireTimer<=0)
        {
            fireFlowerEquipped = false;
        }  
        else if (currFireballsFired>=maxFireballsfired)
        {
            fireFlowerEquipped = false;
            currFireballsFired = 0;
        }
    }
    public void starMan()
    {
        if(starManEquipped)
        {
            starTimer -= Time.deltaTime;
            if(!starParticle.isPlaying)
            starParticle.Play();
        }
        else if(!starManEquipped)
        {
            starTimer = powDuration;            
            starParticle.Stop();
        }        
        if(starTimer<=0)
        {
            starManEquipped = false;
        }  
    }

    public IEnumerator getPowerup()
    {
        player.sprite.color = baseColor;
        yield return new WaitForSecondsRealtime(0.1f);
        player.sprite.color = secColor;
        yield return new WaitForSecondsRealtime(0.1f);
        player.sprite.color = baseColor;
        yield return new WaitForSecondsRealtime(0.1f);
        player.sprite.color = secColor;
        yield return new WaitForSecondsRealtime(0.1f);
        player.sprite.color = baseColor;
        yield return new WaitForSecondsRealtime(0.1f);
        player.sprite.color = secColor;
        yield return new WaitForSecondsRealtime(0.1f);
    }
}
