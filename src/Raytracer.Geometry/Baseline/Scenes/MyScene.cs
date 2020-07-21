using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Raytracer.Geometry.Baseline.Hitable;

namespace Raytracer.Geometry.Baseline.Scenes
{
    public class MyScene
    {
        private readonly Light[] _lights;

        public MyScene()
        {
            Camera = new Camera(new Vec3(3.0f, 2.0f, 4.0f), new Vec3(-1.0f, 0.5f, 0.0f));

            Things = new ImmutableArray<IHitable>
            {
                new Plane<Checkerboard>(new Vec3(0.0f, 1.0f, 0.0f), 0.0f, new Checkerboard()),
                new Sphere<Shiny>(new Vec3(0.0f, 1.0f, -0.25f), 1.0f, new Shiny()),
                new Sphere<Shiny>(new Vec3(-1.0f, 0.5f, 1.5f), 0.5f, new Shiny()),
            };

            _lights = new[]
            {
                new Light(new Vec3(-2.0f, 2.5f, 0.0f), new Color(0.49f, 0.07f, 0.07f)),
                new Light(new Vec3(1.5f, 2.5f, 1.5f), new Color(0.07f, 0.07f, 0.49f)),
                new Light(new Vec3(1.5f, 2.5f, -1.5f), new Color(0.07f, 0.49f, 0.071f)),
                new Light(new Vec3(0.0f, 3.5f, 0.0f), new Color(0.21f, 0.21f, 0.35f))
            };
        }

        public ImmutableArray<IHitable> Things
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        public ReadOnlySpan<Light> Lights
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _lights;
        }

        public Camera Camera
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }
    }
}
