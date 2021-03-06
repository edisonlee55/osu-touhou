﻿using osu.Framework.Graphics;
using osuTK;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.Containers;
using osuTK.Graphics;
using System.Collections.Generic;
using osu.Game.Rulesets.Touhou.Extensions;

namespace osu.Game.Rulesets.Touhou.Objects.Drawables
{
    public abstract class DrawableCherry : DrawableTouhouHitObject
    {
        protected override Color4 GetComboColour(IReadOnlyList<Color4> comboColours) =>
            comboColours[(HitObject.IndexInBeatmap + 1) % comboColours.Count];

        protected virtual float GetBaseSize() => 25;

        private readonly Sprite sprite;
        private readonly Sprite kiaiSprite;
        private readonly Sprite overlay;
        private readonly Sprite branch;
        protected readonly Container Content;

        private readonly bool isKiai;

        protected DrawableCherry(Cherry h)
            : base(h)
        {
            isKiai = h.IsKiai;

            Origin = Anchor.Centre;
            Size = new Vector2(GetBaseSize() * MathExtensions.Map(h.CircleSize, 0, 10, 0.2f, 1));
            Position = h.Position;
            Scale = Vector2.Zero;

            AddInternal(Content = new Container
            {
                RelativeSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Children = new Drawable[]
                {
                    kiaiSprite = new Sprite
                    {
                        Scale = new Vector2(1.8f),
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Alpha = isKiai ? 1 : 0,
                    },
                    sprite = new Sprite
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                    },
                    overlay = new Sprite
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                    },
                    branch = new Sprite
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Position = new Vector2(1, -1)
                    }
                }
            });
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            sprite.Texture = textures.Get("cherry");
            kiaiSprite.Texture = textures.Get("cherry-kiai");
            overlay.Texture = textures.Get("cherry-overlay");
            branch.Texture = textures.Get("cherry-branch");

            AccentColour.BindValueChanged(accent => sprite.Colour = kiaiSprite.Colour = accent.NewValue, true);
        }

        protected override void UpdateInitialTransforms()
        {
            this.ScaleTo(Vector2.One, HitObject.TimePreempt);

            sprite.Delay(HitObject.TimePreempt).Then().FlashColour(Color4.White, 300);
            overlay.Delay(HitObject.TimePreempt).Then().FlashColour(Color4.White, 300);

            if (isKiai)
                kiaiSprite.Delay(HitObject.TimePreempt).Then().FlashColour(Color4.White, 300);
        }
    }
}
