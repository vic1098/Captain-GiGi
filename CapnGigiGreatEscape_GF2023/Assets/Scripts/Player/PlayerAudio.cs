using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource jumpAudio;
    public AudioSource landAudio;
    public AudioSource takeDamageAudio;
    public AudioSource attackAndMissAudio;
    public AudioSource attackAndHitAudio;
    //public AudioSource runningAudio;
    //public PlayerController player;
    
    public void Awake(){
      //  player = GetComponent<PlayerController>();
    }
      public void PlayjumpAudio(){
        jumpAudio.Play();
    }
      public void PlaylandAudio(){//ask Fab, where is the land?
        landAudio.Play();
    }
      public void PlaytakeDamageAudio(){
        takeDamageAudio.Play();
    }
      public void PlayattackAndHitAudio(){
        attackAndHitAudio.Play();
    }
      public void PlayattackAndMissAudio(){
        attackAndMissAudio.Play();
    }
    //public void PlayrunningAudio()
    //{
    //    if (player.IsMoving)
    //    {
    //        runningAudio.Play();
    //    }
    //    else
    //    {
    //        runningAudio.Stop();
    //    }
    //}

}
