using UnityEngine;

public class UnderwaterAudioEffect : MonoBehaviour
{
    public static UnderwaterAudioEffect Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Instance.GetComponent<AudioLowPassFilter>().enabled = false;
        Instance.GetComponent<AudioHighPassFilter>().enabled = false;
    }

    public void SetUnderwaterEffect(bool underwater)
    {
        if (underwater)
        {
            Instance.GetComponent<AudioLowPassFilter>().enabled = true;
            Instance.GetComponent<AudioHighPassFilter>().enabled = true;
        }
        else
        {
            Instance.GetComponent<AudioLowPassFilter>().enabled = false;
            Instance.GetComponent<AudioHighPassFilter>().enabled = false;
        }
    }
}
