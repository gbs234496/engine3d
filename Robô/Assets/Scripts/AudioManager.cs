using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    // Padronizado estritamente com a propriedade do seu GameManager
    public static AudioManager Instance { get; private set; }
    
    private AudioSource _systemSource;

    private void Awake()
    {
        // Estrutura de Singleton idêntica à do GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        // Inicialização do componente de áudio principal (2D)
        _systemSource = GetComponent<AudioSource>();
        if (_systemSource == null)
        {
            _systemSource = gameObject.AddComponent<AudioSource>();
            _systemSource.playOnAwake = false;
            _systemSource.spatialBlend = 0f; // Força o modo 2D
        }
    }

    #region 2D Sound Controls (systemSource)

    // Toca um clip em loop (ou não, dependendo do parâmetro)
    public void Play2D(AudioClip clip, float volume = 1f, bool loop = true)
    {
        if (clip == null) return;
        _systemSource.Stop();
        _systemSource.clip = clip;
        _systemSource.volume = volume;
        _systemSource.loop = loop;
        _systemSource.spatialBlend = 0f;
        _systemSource.Play();
    }

    public void Pause2D()
    {
        if (_systemSource.isPlaying)
            _systemSource.Pause();
    }

    public void Resume2D()
    {
        if (_systemSource.clip != null && !_systemSource.isPlaying)
            _systemSource.UnPause();
    }

    public void Stop2D()
    {
        _systemSource.Stop();
        _systemSource.clip = null;
    }

    // Versão one-shot para 2D (não altera o clip principal, ideal para efeitos sonoros de UI)
    public void Play2DOneShot(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        _systemSource.spatialBlend = 0f;
        _systemSource.PlayOneShot(clip, volume);
    }

    #endregion
}