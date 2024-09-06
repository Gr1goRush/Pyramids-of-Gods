using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioSource _source;

    private void Update()
    {
        bool sound = Saver.GetBool("Sounds", true);
        if (sound)
        {
            if (!_source.isPlaying) _source.Play();
        }
        else
        {
            _source.Pause();
        }
    }
}
