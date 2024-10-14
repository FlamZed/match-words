using System;
using DG.Tweening;
using Infrastructure.AssetManagement.Data;
using Infrastructure.AssetManagement.Loader;
using Infrastructure.Services.Audio.Type;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Audio
{
    public class AudioService : MonoBehaviour, IAudioService
    {
        [SerializeField] private AudioSource _oneShotSource;
        [SerializeField] private AudioSource _backSource;

        [Inject] private IAssetsLoader _assetsLoader;

        public void PlayOneShot(AudioClipShot clip) =>
            PlayOneShot(ChoseShotClip(clip));

        public void PlayOneShot(AudioClip clip) =>
            _oneShotSource.PlayOneShot(clip);

        public void PlayBackground(BackgroundClip clip) =>
            PlayBackground(ChoseClip(clip));

        public void PlayBackground(AudioClip clip)
        {
            _backSource.DOFade(0, 0.5f).OnComplete(() =>
            {
                _backSource.clip = clip;
                _backSource.Play();
                _backSource.DOFade(1, 0.5f);
            });
        }

        private AudioClip ChoseShotClip(AudioClipShot clip) =>
            clip switch
            {
                AudioClipShot.Kick => _assetsLoader.Load<AudioClip>(AssetPath.KickClip),
                AudioClipShot.Tap => _assetsLoader.Load<AudioClip>(AssetPath.TapClip),
                AudioClipShot.Win => _assetsLoader.Load<AudioClip>(AssetPath.WinClip),
                AudioClipShot.Lose => _assetsLoader.Load<AudioClip>(AssetPath.LoseClip),
                _ => throw new ArgumentOutOfRangeException()
            };

        private AudioClip ChoseClip(BackgroundClip clip) =>
            clip switch
            {
                BackgroundClip.Menu => _assetsLoader.Load<AudioClip>(AssetPath.BackgroundMenuClip),
                BackgroundClip.Game => _assetsLoader.Load<AudioClip>(AssetPath.BackgroundClip),
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}
