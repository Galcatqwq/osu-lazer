using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Textures;
using osu.Game.Configuration;
using osuTK;
using osuTK.Graphics;
//using osu.Game.Skinning;

namespace osu.Game.Graphics.Cursor
{
    public partial class HybridMenuCursor : CompositeDrawable, IGameCursor
    {
        private readonly Drawable cursorBody;
        private readonly MenuCursorContainer.Cursor cursorTrail;
        private Bindable<float> menuCursorSize;
        private const float base_scale = 0.15f;

        // 添加 OsuCursor.SIZE 的替代值
        private const float cursor_size = 28;

        public HybridMenuCursor(Bindable<float> menuCursorSize, Drawable cursorBody, Texture texture)
        {
            this.menuCursorSize = menuCursorSize;
            this.cursorBody = cursorBody;
            Texture = texture;
            RelativeSizeAxes = Axes.None;
            AutoSizeAxes = Axes.Both;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;

            InternalChildren = new Drawable[]
            {
                cursorTrail = new MenuCursorContainer.Cursor
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
                cursorBody = new Container
                {
                    Size = new Vector2(cursor_size),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Children = new Drawable[]
                    {
                        new CircularContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            Masking = true,
                            BorderThickness = cursor_size / 6,
                            BorderColour = Color4.White,
                            EdgeEffect = new EdgeEffectParameters
                            {
                                Type = EdgeEffectType.Shadow,
                                Colour = Color4.Pink.Opacity(0.5f),
                                Radius = 5,
                            },
                            Child = new Box { RelativeSizeAxes = Axes.Both, Alpha = 0 }
                        },
                        new Circle
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(cursor_size * 0.14f),
                            Colour = new Color4(34, 93, 204, 255),
                            EdgeEffect = new EdgeEffectParameters
                            {
                                Type = EdgeEffectType.Glow,
                                Radius = 8,
                                Colour = Color4.White,
                            }
                        }
                    }
                }
            };
        }

        public HybridMenuCursor()
        {
            AutoSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures, OsuConfigManager config)
        {
            Texture = textures.Get(@"Cursor/cursortrail");
            Scale = new Vector2(1 / Texture.ScaleAdjust);
            menuCursorSize = config.GetBindable<float>(OsuSetting.MenuCursorSize);
            menuCursorSize.BindValueChanged(size => UpdateScale(size.NewValue), true);
        }

        public Texture Texture { get; set; }

        public void UpdateScale(float size)
        {
            float scale = size * base_scale;
            cursorBody.Scale = new Vector2(scale);
            cursorTrail.Scale = new Vector2(scale);
        }

        protected override void Update()
        {
            base.Update();
            cursorTrail.Position = cursorBody.Position;
        }
    }
}