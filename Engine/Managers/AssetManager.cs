using System.Threading;
using SFML.Audio;
using SFML.Graphics;

namespace Engine.Managers
{
    public class AssetManager
    {
        private static AssetManager _instance;
        private static readonly object Sync = new object();

        private Manager<Font> _font;

        private Manager<Music> _music;

        private Manager<SoundBuffer> _sound;

        private Manager<Texture> _texture;

        public Manager<Texture> Texture => _texture ?? (_texture = new Manager<Texture>());

        public Manager<Font> Font => _font ?? (_font = new Manager<Font>());

        public Manager<Music> Music => _music ?? (_music = new Manager<Music>());

        public Manager<SoundBuffer> Sound => _sound ?? (_sound = new Manager<SoundBuffer>());

        #region SINGLETON

        private AssetManager()
        {
        }

        public static AssetManager Instance
        {
            get
            {
                if (_instance == null)
                    lock (Sync)
                    {
                        if (_instance == null)
                        {
                            var instance = new AssetManager();
                            Thread.MemoryBarrier();
                            _instance = instance;
                        }
                    }

                return _instance;
            }
        }

        #endregion
    }
}