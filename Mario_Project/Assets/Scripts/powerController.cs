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
    public AudioClip lilBeep;
    
    [Header("Fire Ball Variables")]
    public bool fireFlowerEquipped;
    public float fireTimer;
    public int maxFireballsfired;
    public int currFireballsFired;
    public int instancedFireballs;
    
    [Header("Mushroom Variables")]
    public bool bigMushroomEquipped;
    public float mushTimer;
    public BoxCollider2D bigBox;
    public BoxCollider2D smallBox;
    
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
        bigShroom();
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
            player.anim.SetFloat("fireFloat", .5f);
        }
        else if(!fireFlowerEquipped)
        {
            fireTimer = powDuration;
        }
        if(fireTimer<=0)
        {
            fireFlowerEquipped = false;
            player.anim.SetFloat("fireFloat", 0f);
        }  
        else if (currFireballsFired>=maxFireballsfired)
        {
            fireFlowerEquipped = false;
            player.anim.SetFloat("fireFloat", 0f);
            currFireballsFired = 0;
        }
    }
    public void starMan()
    {
        if(starManEquipped)
        {
            starTimer -= Time.deltaTime;
            player.anim.SetFloat("fireFloat", 1f);
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
            player.anim.SetFloat("fireFloat", 0f);
        }  
    }
    public void bigShroom()
    {
        if(bigMushroomEquipped)
        {
            mushTimer -= Time.deltaTime;
            //if(!starParticle.isPlaying)
            //starParticle.Play();
        }
        else if(!bigMushroomEquipped)
        {
            mushTimer = powDuration;
            bigBox.enabled = false;          
            //starParticle.Stop();
        }        
        if(mushTimer<=0)
        {
            //bigMushroomEquipped = false;
            if(bigMushroomEquipped)
            {
                StartCoroutine("loseShroom");              
            }

            //bigBox.enabled = false;
            //player.anim.SetBool("bigShroom", false);
        }
        
        RaycastHit2D brickRay = Physics2D.BoxCast(player.headPoint.position, new Vector2(.75f,.2f), 0f, transform.up, 1f, player.brickLayer);
        if (brickRay.collider != null)
        {
            if(bigMushroomEquipped)
            {
                player.rb.AddForce(-transform.up * (player.jumpPower/5), ForceMode2D.Impulse);
                player.manager.addScore(50);
                Destroy(brickRay.collider.gameObject);
            }
        }
        RaycastHit2D item = Physics2D.BoxCast(player.headPoint.position, new Vector2(.75f,.2f), 0f, transform.up, 1f, player.itemBlockLayer);
        if (item.collider != null )
        {
            itemBlock block = item.collider.gameObject.GetComponent<itemBlock>();
            block.spawnItem();
        }
    }

    public IEnumerator getPowerup()
    {
        Time.timeScale = 0f;
        player.sprite.color = baseColor;
        yield return new WaitForSecondsRealtime(0.2f);
        player.sprite.color = secColor;
        yield return new WaitForSecondsRealtime(0.2f);
        player.sprite.color = baseColor;
        yield return new WaitForSecondsRealtime(0.2f);
        player.sprite.color = secColor;
        yield return new WaitForSecondsRealtime(0.2f);
        player.sprite.color = baseColor;
        yield return new WaitForSecondsRealtime(0.2f);
        player.sprite.color = secColor;
        yield return new WaitForSecondsRealtime(0.2f);
        player.sprite.color = baseColor;
        Time.timeScale = 1f;
    }
    public IEnumerator getShroom()
    {
        Time.timeScale = 0f;
        
        player.anim.Play("Big_Idle");
        //AudioSource.PlayClipAtPoint(lilBeep, transform.position, 1f);
        yield return new WaitForSecondsRealtime(0.2f);
        player.anim.Play("Idle_Animation_Blend");
        //AudioSource.PlayClipAtPoint(lilBeep, transform.position, 1f);
        yield return new WaitForSecondsRealtime(0.2f);
        player.anim.Play("Big_Idle");
        //AudioSource.PlayClipAtPoint(lilBeep, transform.position, 1f);
        yield return new WaitForSecondsRealtime(0.2f);
        player.anim.Play("Idle_Animation_Blend");
        //AudioSource.PlayClipAtPoint(lilBeep, transform.position, 1f);
        yield return new WaitForSecondsRealtime(0.2f);
        player.anim.Play("Big_Idle");
        //AudioSource.PlayClipAtPoint(lilBeep, transform.position, 1f);
        yield return new WaitForSecondsRealtime(0.2f);
        player.anim.Play("Idle_Animation_Blend");
        //AudioSource.PlayClipAtPoint(lilBeep, transform.position, 1f);
        yield return new WaitForSecondsRealtime(0.2f);  
        player.anim.Play("Big_Idle");
        //AudioSource.PlayClipAtPoint(lilBeep, transform.position, 1f);
        yield return new WaitForSecondsRealtime(0.2f);
        
        bigBox.enabled = true;
        smallBox.enabled = false;
        player.anim.SetBool("bigShroom", true);
        player.anim.Play("Big Idle Blend");       
        Time.timeScale = 1f;
    }
    public IEnumerator loseShroom()
    {
        bigMushroomEquipped = false;
        Time.timeScale = 0f;
        player.anim.Play("Big_Idle");
        //AudioSource.PlayClipAtPoint(lilBeep, transform.position, 1f);
        yield return new WaitForSecondsRealtime(0.2f);
        player.anim.Play("Idle_Animation_Blend");
        //AudioSource.PlayClipAtPoint(lilBeep, transform.position, 1f);
        yield return new WaitForSecondsRealtime(0.2f);
        player.anim.Play("Big_Idle");
        //AudioSource.PlayClipAtPoint(lilBeep, transform.position, 1f);
        yield return new WaitForSecondsRealtime(0.2f);
        player.anim.Play("Idle_Animation_Blend");
        //AudioSource.PlayClipAtPoint(lilBeep, transform.position, 1f);
        yield return new WaitForSecondsRealtime(0.2f);
        player.anim.Play("Big_Idle");
        //AudioSource.PlayClipAtPoint(lilBeep, transform.position, 1f);
        yield return new WaitForSecondsRealtime(0.2f);
        player.anim.Play("Idle_Animation_Blend");
        //AudioSource.PlayClipAtPoint(lilBeep, transform.position, 1f);
        yield return new WaitForSecondsRealtime(0.2f);  
        player.anim.Play("Big_Idle");
        //AudioSource.PlayClipAtPoint(lilBeep, transform.position, 1f);
        yield return new WaitForSecondsRealtime(0.2f);
        
        bigBox.enabled = false;
        smallBox.enabled = true;
        player.anim.SetBool("bigShroom", false);
        player.anim.Play("Idle_Animation_Blend");        
        Time.timeScale = 1f;
    }

        public IEnumerator loseShroomFast()
    {
        bigMushroomEquipped = false;
        Time.timeScale = 0f;
        player.anim.Play("Big_Idle");
        //AudioSource.PlayClipAtPoint(lilBeep, transform.position, 1f);
        yield return new WaitForSecondsRealtime(0.2f);
        player.anim.Play("Idle_Animation_Blend");
        //AudioSource.PlayClipAtPoint(lilBeep, transform.position, 1f);
        yield return new WaitForSecondsRealtime(0.2f);  
        player.anim.Play("Big_Idle");
        //AudioSource.PlayClipAtPoint(lilBeep, transform.position, 1f);
        yield return new WaitForSecondsRealtime(0.2f);
        
        bigBox.enabled = false;
        smallBox.enabled = true;
        player.anim.SetBool("bigShroom", false);
        player.anim.Play("Idle_Animation_Blend");        
        Time.timeScale = 1f;
    }
}
