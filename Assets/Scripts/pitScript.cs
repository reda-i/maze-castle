﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pitScript : MonoBehaviour
{
    public GameObject canvas;

    private Animator anim;
    private AudioSource src;
    private bool isFalling = false;
    private bool soundTriggered = false;

    private float startFall;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        src = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] tmp = anim.GetCurrentAnimatorClipInfo(0);

        if (tmp[0].clip.name.Equals("walking"))
        {
            if (this.gameObject.transform.position.z >= -16)
            {
                anim.SetBool("AtPit", true);
                isFalling = true;
            }
        }
        else if (isFalling && tmp[0].clip.name.Equals("plFalling"))
        {
            anim.applyRootMotion = false;

            canvas.SetActive(true);

            if (!soundTriggered)
            {
                startFall = Time.timeSinceLevelLoad;
                soundTriggered = true;
                src.pitch = 1;
                src.PlayOneShot(src.clip);
            }

            if (isFalling && Time.timeSinceLevelLoad - startFall >= 4)
            {
                SceneManager.LoadScene("GameOverScene", LoadSceneMode.Single);
            }

        }

    }
}
