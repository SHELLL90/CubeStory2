using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformIngorePlayer : MonoBehaviour
{
    [SerializeField] private PlatformEffector2D platformEffector;
    [SerializeField] private float defaultRotationalOffset = 0.0f;
    [SerializeField] private float newRotationalOffset = 0.0f;
    [SerializeField] private float timeIgnorePlayer = 0.4f;

    private IEnumerator _enumeratorIgnorePlayer;
    public void IgnorePlayer()
    {
        if (_enumeratorIgnorePlayer != null)
        {
            StopCoroutine(_enumeratorIgnorePlayer);
            _enumeratorIgnorePlayer = null;
        }

        _enumeratorIgnorePlayer = IEnumeratorIgnorePlayer();
        StartCoroutine(IEnumeratorIgnorePlayer());
    }

    private IEnumerator IEnumeratorIgnorePlayer()
    {
        platformEffector.rotationalOffset = newRotationalOffset;
        yield return new WaitForSeconds(timeIgnorePlayer);
        platformEffector.rotationalOffset = defaultRotationalOffset;
    }
}
