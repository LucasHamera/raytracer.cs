using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Raytracer.Geometry.Baseline.Hitable;
using Raytracer.Geometry.Baseline.Surfaces;

namespace Raytracer.Geometry.Baseline.Scenes
{
    public class MyScene
    {
        private readonly Light[] _lights;

        public MyScene()
        {
            Camera = new Camera(new Vec3(3.0f, 2.0f, 4.0f), new Vec3(-1.0f, 0.5f, 0.0f));

            Things = ImmutableArray.Create(new IHitable[]
            {
                new Plane(new Vec3(0.0f, 1.0f, 0.0f), 0.0f, Checkerboard.Create()),
                new Sphere(new Vec3(0.0f, 1.0f, -0.25f), 1.0f, Shiny.Create()),
                new Sphere(new Vec3(-1.0f, 0.5f, 1.5f), 0.5f, Shiny.Create()),
            });

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