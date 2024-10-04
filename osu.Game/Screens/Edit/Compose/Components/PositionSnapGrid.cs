﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Layout;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Screens.Edit.Compose.Components
{
    public abstract partial class PositionSnapGrid : BufferedContainer
    {
        /// <summary>
        /// The position of the origin of this <see cref="PositionSnapGrid"/> in local coordinates.
        /// </summary>
        public Bindable<Vector2> StartPosition { get; } = new Bindable<Vector2>(Vector2.Zero);

        protected readonly LayoutValue GridCache = new LayoutValue(Invalidation.RequiredParentSizeToFit);

        protected PositionSnapGrid()
            : base(cachedFrameBuffer: true)
        {
            BackgroundColour = Color4.White.Opacity(0);

            StartPosition.BindValueChanged(_ => GridCache.Invalidate());

            AddLayout(GridCache);
        }

        protected override void Update()
        {
            base.Update();

            if (GridCache.IsValid)
                return;

            ClearInternal();

            if (DrawWidth > 0 && DrawHeight > 0)
                CreateContent();

            GridCache.Validate();
            ForceRedraw();
        }

        protected abstract void CreateContent();

        /// <summary>
        /// Computes the line thickness of this <see cref="PositionSnapGrid"/> which will result in 1px screen-space thickness independent of display resolution.
        /// </summary>
        /// <remarks>
        /// Unless resolution is high enough, in which case thickness will be 2px.
        /// </remarks>
        protected float GetLineWidth()
        {
            float fixedWidth = DrawWidth / ScreenSpaceDrawQuad.Width; // the width at which screen-space thickness will be 1px

            // Resolution is low enough
            if (fixedWidth > 0.4f)
                return fixedWidth;

            return fixedWidth * 2f;
        }

        protected void GenerateOutline(Vector2 drawSize)
        {
            float lineWidth = GetLineWidth();

            AddRangeInternal(new[]
            {
                new Box
                {
                    Colour = Colour4.White,
                    Alpha = 0.3f,
                    RelativeSizeAxes = Axes.X,
                    Height = lineWidth,
                    Y = 0,
                },
                new Box
                {
                    Colour = Colour4.White,
                    Alpha = 0.3f,
                    Origin = Anchor.BottomLeft,
                    Anchor = Anchor.BottomLeft,
                    RelativeSizeAxes = Axes.X,
                    Height = lineWidth
                },
                new Box
                {
                    Colour = Colour4.White,
                    Alpha = 0.3f,
                    RelativeSizeAxes = Axes.Y,
                    Width = lineWidth
                },
                new Box
                {
                    Colour = Colour4.White,
                    Alpha = 0.3f,
                    Origin = Anchor.TopRight,
                    Anchor = Anchor.TopRight,
                    RelativeSizeAxes = Axes.Y,
                    Width = lineWidth
                },
            });
        }

        public abstract Vector2 GetSnappedPosition(Vector2 original);
    }
}
