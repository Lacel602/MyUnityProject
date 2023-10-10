using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostFadeEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject _ghost;
    [SerializeField]
    private float ghostDelay;
    private float currentDelay;
    private bool makeGhost = false;
    // Start is called before the first frame update
    void Start()
    {
        currentDelay = ghostDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (makeGhost)
        {
            if (currentDelay > 0)
            {
                currentDelay -= Time.deltaTime;
            }
            else
            {
                GameObject currentGhost = Instantiate(_ghost, transform.position, transform.rotation);
                currentGhost.transform.localScale = this.transform.localScale;
                currentDelay = ghostDelay;
                Destroy(currentGhost, 1f);
            }
        }
    }

    public void SetMakeGhost(bool makeGhost)
    {
        this.makeGhost = makeGhost;
    }
}
