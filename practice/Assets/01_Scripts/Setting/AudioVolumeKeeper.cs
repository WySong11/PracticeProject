using System.Collections;
using UnityEngine;

public class AudioVolumeKeeper : MonoBehaviour
{
    public string SaveKey = "";
    public VolumeController VolumeController;

    private IEnumerator Start()
    {
        yield return null;

        if (string.IsNullOrWhiteSpace(SaveKey) == true)
        {
            throw new System.Exception("��������Ű�� ����");
        }

        float volume = PlayerPrefs.GetFloat(SaveKey, 0.7f);
        Set(volume);
    }

    public void Set(float volume)
    {
        PlayerPrefs.SetFloat(SaveKey, volume);
        VolumeController.Set(volume);
    }
}

