using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFade : MonoBehaviour
{

    [SerializeField]
    Transform player;

    [SerializeField]
    Transform startCenter;

    [SerializeField]
    TextMeshPro mText;

    [SerializeField]
    public float maxRange;

    [SerializeField]
    public float minRange;

    // Update is called once per frame
    void Update()
    {
        mText.alpha = 1-(Mathf.Max((Vector3.Distance(player.position, startCenter.position) - minRange),0)/(minRange-maxRange));
    }
}
