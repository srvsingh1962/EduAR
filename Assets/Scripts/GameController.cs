using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Sprite Pause;
    public Sprite Camera;
    public GameObject RecordButton;

    [SerializeField]
    private AndroidUtils androidUtils;

    private void Start()
    {
        if (!AndroidUtils.IsPermitted(AndroidPermission.ACCESS_FINE_LOCATION))
            AndroidUtils.RequestPermission(AndroidPermission.ACCESS_FINE_LOCATION);
        androidUtils = FindObjectOfType<AndroidUtils>();
    }

    private IEnumerator DelayCallRecord()
    {
        yield return new WaitForSeconds(0.1f);
        androidUtils.StartRecording();
    }

    private IEnumerator DelaySaveVideo()
    {
        yield return new WaitForSeconds(1);
        //androidUtils.;
    }

    public void StopAndCapture()
    {
        if(RecordButton.transform.GetChild(0).GetComponent<Image>().sprite == Camera)
        {
            RecordButton.transform.GetChild(0).GetComponent<Image>().sprite = Pause;
            RecordButton.GetComponent<Image>().color = Color.grey;
            //androidUtils.PrepareRecorder();
            StartCoroutine(DelayCallRecord());
        }
        else
        {
            RecordButton.transform.GetChild(0).GetComponent<Image>().sprite = Camera;
            RecordButton.GetComponent<Image>().color = Color.red;
            androidUtils.StopRecording();
            StartCoroutine(DelaySaveVideo());
        }
    }
}
