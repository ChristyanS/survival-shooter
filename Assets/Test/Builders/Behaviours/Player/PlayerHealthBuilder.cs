using Behaviours.Player;
using Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Test.Builders.Behaviours.Player
{
    public class PlayerHealthBuilder : GameObjectBuilder<PlayerHealth>
    {
        public PlayerHealthBuilder()
        {
        }

        public PlayerHealthBuilder(GameObject gameObject) : base(gameObject)
        {
        }

        public PlayerHealthBuilder(string name) : base(name)
        {
        }


        public PlayerHealthBuilder AddPlayerMovement()
        {
            new PlayerMovementBuilder(GameObject).AddMainCamera().AddCharacterController().AddVirtualInputManager()
                .AddAnimator().Build();
            return this;
        }

        public PlayerHealthBuilder AddAudioSource()
        {
            new GameObjectBuilder<AudioSource>(GameObject).Build().Component.clip =
                AudioClip.Create("Player Hurt", 1, 1, 1000, true, null, null);
            return this;
        }

        public PlayerHealthBuilder AddSlider()
        {
            new GameObjectBuilder<Slider>().Build().WithTag(Tag.Health.ToString());
            return this;
        }

        public PlayerHealthBuilder AddDamageImage()
        {
            new GameObjectBuilder<Image>().Build().WithTag(Tag.DamageImage.ToString());
            return this;
        }


        public PlayerHealthBuilder WithAudioClip()
        {
            Component.deathClip = AudioClip.Create("Death Clip", 1, 1, 1000, true, null, null);
            return this;
        }
    }
}