using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPictures : MonoBehaviour
{
    public Image[] ImageList;
    public float PlayRate;

    int _index;
    [Range(1, int.MaxValue)]
    int _sz;
    float _lastSwitchTime;

    // Start is called before the first frame update
    void Start()
    {
        _index = 0;
        _sz = ImageList.Length;
        ImageList[_index].gameObject.SetActive(true);
        _lastSwitchTime = float.NegativeInfinity;
    }

    // Update is called once per frame
    void Update()
    {
        if(_lastSwitchTime + PlayRate < Time.time)
        {
            _lastSwitchTime = Time.time;
            ImageList[_index].gameObject.SetActive(false);
            _index = (_index + 1) % _sz;
            ImageList[_index].gameObject.SetActive(true);
        }
    }
}
