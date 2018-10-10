using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
	// serialized for debugging
	[SerializeField] GameObject blockParticle;
	[SerializeField] int maxHits;
	[SerializeField] int timesHit;
	[SerializeField] Sprite[] damageSprite;
	// cached ref
	Level level;


	private void Start(){
		level = FindObjectOfType<Level>();
		if(tag =="Breakable"){
			level.CountBreakableBlocks();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision){
		if (tag == "Breakable"){
			doDamage();
		}
	}
	private void doDamage(){
		timesHit++;
		if(timesHit >= maxHits){
			DestoryBlock();
		}
		else {
			ShowNextDamageSprite();
		}
	}
	private void ShowNextDamageSprite(){
		int spriteIndex = timesHit - 1;
		GetComponent<SpriteRenderer>().sprite = damageSprite[spriteIndex];

	}

	private void DestoryBlock(){
		FindObjectOfType<GameStatus>().AddToScore();
		Destroy(gameObject);
		level.BlockDestroyed();
		TriggerParticle();
	}

	private void TriggerParticle(){
		GameObject particles = Instantiate(blockParticle, transform.position, transform.rotation);
		Destroy(particles, 1f);
	}
}
