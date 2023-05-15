using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public int EnemiesRemaining = 0;
    [SerializeField] Transform _holder, _playerStart;
    int levelCount = 0;
    int currentLevel = 0;
    [SerializeField] CanvasGroup _fader;
    private void Awake()
    {
        levelCount = _holder.childCount;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            _holder.GetChild(PlayerPrefs.GetInt("Level")).gameObject.SetActive(true);
            currentLevel = PlayerPrefs.GetInt("Level");
        }
        else
        {
            _holder.GetChild(0).gameObject.SetActive(true);
        }

        StartCoroutine(Fade(true));
    }

    public void OnEnemyDead()
    {
        EnemiesRemaining--;

        if (EnemiesRemaining == 0)
        {
            StartCoroutine(ChangeLevel());
        }

    }
    IEnumerator ChangeLevel()
    {
        yield return StartCoroutine(Fade(false));

        currentLevel++;

        for (int i = 0; i < levelCount; i++)
        {
            _holder.GetChild(i).gameObject.SetActive(false);
        }

        _holder.GetChild(currentLevel).gameObject.SetActive(true);
        PlayerPrefs.SetInt("Level", currentLevel);
        GameObject.FindWithTag("Player").transform.position = _playerStart.position;
        FindObjectOfType<PlayerHealth>().ResetHealth();
        yield return StartCoroutine(Fade(true));
    }

    IEnumerator Fade(bool fadein)
    {
        if (fadein)
        {
            while (_fader.alpha > 0)
            {
                yield return new WaitForSeconds(.05f);
                _fader.alpha -= 0.1f;
            }
        }
        else
        {
            while (_fader.alpha < 0.99)
            {
                yield return new WaitForSeconds(.05f);
                _fader.alpha += 0.1f;
            }
        }
    }
}
