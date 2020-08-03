using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using Raytracer.Canvas;
using Raytracer.Geometry.Geometries;
using Raytracer.Geometry.Hitable;
using Raytracer.Geometry.Models;
using Raytracer.Geometry.Scenes;
using Raytracer.Geometry.Utils;

namespace RayTracer
{
    public class RayTracer
    {
        private Optional<Intersection> Intersect(in Ray ray, in MyScene scene)
        {
            var closestDist = float.MaxValue;
            var closestInter = new Optional<Intersection>();

            foreach (var thing in scene.Things)
            {
                var inter = thing.Intersect(ray);
                if (inter.HasValue && inter.Value.Distance < closestDist)
                {
                    closestDist = inter.Value.Distance;
                    closestInter = inter;
                }
            }

            return closestInter;
        }

        private Optional<float> TestRay(in Ray ray, MyScene scene)
        {
            var intersection = Intersect(ray, scene);
            return intersection.HasValue
                ? new Optional<float>(intersection.Value.Distance)
                : new Optional<float>();
        }

        private Color TraceRay(in Ray ray, MyScene scene, int depth)
        {
            var intersection = Intersect(ray, scene);
            return intersection.HasValue
                ? Shade(intersection.Value, scene, depth)
                : Color.Background;
        }

        private const int MaxDepth = 5;

        private Color Shade(in Intersection intersection, MyScene scene, int depth)
        {
            var d = intersection.Ray.Direction;
            var pos = (intersection.Distance * d) + intersection.Ray.Start;
            var normal = intersection.Thing.Normal(pos);
            var reflectDir = d - (2.0f * GeometryMath.Dot(normal, d) * normal);
            var naturalColor = Color.Background + NaturalColor(intersection.Thing, pos, normal, reflectDir, scene);
            var reflectedColor = depth >= MaxDepth
                ? Color.Gray
                : ReflectionColor(intersection.Thing, pos, reflectDir, scene, depth);
            return naturalColor + reflectedColor;
        }

        private Color ReflectionColor(IHitable thing, in Vec3 pos, in Vec3 rd, MyScene scene, int depth)
        {
            return GeometryMath.Scale(
                thing.Surface.Reflect(pos),
                TraceRay(new Ray(pos, rd), scene, depth + 1)
            );
        }

        private Color AddLight(IHitable thing, in Vec3 pos, in Vec3 normal, in Vec3 rayDir, MyScene scene, Color col,
            Light light)
        {
            var lightDir = light.Position - pos;
            var lightDirNorm = GeometryMath.Norm(lightDir);
            var nearIntersection = TestRay(new Ray(pos, lightDirNorm), scene);
            var isInShadow = nearIntersection.HasValue && nearIntersection.Value < GeometryMath.Mag(lightDir);
            if (isInShadow) return col;

            var illumination = GeometryMath.Dot(lightDirNorm, normal);
            var lightColor = illumination > 0.0f
                ? GeometryMath.Scale(illumination, light.Color)
                : Color.DefaultColor;
            var specular = GeometryMath.Dot(lightDirNorm, GeometryMath.Norm(rayDir));
            var surf = thing.Surface;
            var specularColor = specular > 0.0f
                ? GeometryMath.Scale(GeometryMath.Pow(specular, surf.Roughness), light.Color)
                : Color.DefaultColor;

            return col + (surf.Diffuse(pos) * lightColor + surf.Specular(pos) * specularColor);
        }

        private Color NaturalColor(IHitable thing, in Vec3 pos, in Vec3 norm, in Vec3 rd, MyScene scene)
        {
            var col = Color.DefaultColor;
            foreach (var light in scene.Lights)
                col = AddLight(thing, pos, norm, rd, scene, col, light);
            return col;
        }

        private Vec3 Point(int width, int height, int x, int y, in Camera cam)
        {
            var recenterX = (x - (width / 2.0f)) / 2.0f / width;
            var recenterY = -(y - (height / 2.0f)) / 2.0f / height;

            return GeometryMath.Norm(cam.Forward + (recenterX * cam.Right + recenterY * cam.Up));
        }

        public void Render(MyScene scene, Canvas canvas)
        {
            for (int y = 0; y < canvas.Height; y++)
            {
                for (int x = 0; x < canvas.Width; x++)
                {
                    var point = Point(canvas.Width, canvas.Height, x, y, scene.Camera);
                    var color = TraceRay(new Ray(scene.Camera.Position, point), scene, 0);
                    canvas[x, y] = color;
                }
            }
        }
    }

    public class RayTracerAvx
    {
        private OptionalIntersections Intersect(in Rays rays, in MyScene scene)
        {
            var closestDist = Vector256.CreateScalar(float.PositiveInfinity);
            var closestIndex = Vector256.Create(-1);

            // TODO
            for (var i = 0; i < scene.Things.Length; i++)
            {
                var thing = scene.Things[i];
                var index = Vector256.Create(i);
                var (inter, mask) = thing.Intersect(rays);
                var replaceMask = Avx.CompareLessThan(inter, closestDist);
                closestDist = Avx.BlendVariable(closestDist, inter, replaceMask);
                closestIndex = Avx.BlendVariable(closestIndex.AsSingle(), index.AsSingle(), replaceMask).AsInt32();
            }

            return new OptionalIntersections();
        }

        private (Vector256<float>, Vector256<float>) TestRay(in Rays ray, MyScene scene)
        {
            var intersection = Intersect(ray, scene);
            return (intersection.Distance, intersection.HasValue);
        }

        private Colors TraceRay(in Rays ray, MyScene scene, int depth)
        {
            var intersection = Intersect(ray, scene);

            var shade = Shade(intersection, scene, depth);
            return new Colors(
                Avx.BlendVariable(Colors.Background.R, shade.R, intersection.HasValue),
                Avx.BlendVariable(Colors.Background.G, shade.G, intersection.HasValue),
                Avx.BlendVariable(Colors.Background.B, shade.B, intersection.HasValue)
            );
        }

        private const int MaxDepth = 5;

        private Colors Shade(in OptionalIntersections intersection, MyScene scene, int depth)
        {
            var d = intersection.Ray.Direction;
            var pos = (intersection.Distance * d) + intersection.Ray.Start;


            Span<Vec3> normals = stackalloc Vec3[8];
            for (int i = 0; i < 8; ++i)
            {
                normals[i] = scene.Things[intersection.Thing.GetElement(i)]
                    .Normal(
                        new Vec3(
                            pos.X.GetElement(i),
                            pos.Y.GetElement(i),
                            pos.Z.GetElement(i)
                        )
                    );
            }


            var normal = new Vec3s(
            );

            var reflectDir = d - (Avx.Multiply(Vector256.Create(2.0f), GeometryMath.Dot(normal, d)) * normal);
            var naturalColor = Colors.Background + NaturalColor(intersection.Thing, pos, normal, reflectDir, scene);
            var reflectedColor = depth >= MaxDepth
                ? Colors.Gray
                : ReflectionColor(intersection.Thing, pos, reflectDir, scene, depth);
            return naturalColor + reflectedColor;
        }

        private Colors ReflectionColor(IHitable thing, in Vec3s pos, in Vec3s rd, MyScene scene, int depth)
        {
            return GeometryMath.Scale(
                thing.Surface.Reflect(pos),
                TraceRay(new Rays(pos, rd), scene, depth + 1)
            );
        }

        private Colors AddLight(IHitable thing, in Vec3s pos, in Vec3s normal, in Vec3s rayDir, MyScene scene,
            Colors col,
            Light light)
        {
            var lightDir = light.Position.Widen() - pos;
            var lightDirNorm = GeometryMath.Norm(lightDir);
            var (distances, mask) = TestRay(new Rays(pos, lightDirNorm), scene);
            if (Avx.MoveMask(mask) == 0)
                return col;

            var mask2 = Avx.CompareLessThan(distances, GeometryMath.Mag(lightDir));
            var msk = Avx.And(mask, mask2);
            if (Avx.MoveMask(msk) == 0)
                return col;

            var surf = thing.Surface;

            var illumination = GeometryMath.Dot(lightDirNorm, normal);
            var illumMask = Avx.CompareGreaterThan(illumination, Vector256<float>.Zero);
            var illumColor = GeometryMath.Scale(illumination, light.Color.Widen());
            var defColor = Colors.DefaultColor;
            var lightColor = new Colors(
                Avx.BlendVariable(illumColor.R, defColor.R, illumMask),
                Avx.BlendVariable(illumColor.G, defColor.G, illumMask),
                Avx.BlendVariable(illumColor.B, defColor.B, illumMask)
            );

            var specular = GeometryMath.Dot(lightDirNorm, GeometryMath.Norm(rayDir));
            var specMask = Avx.CompareGreaterThan(specular, Vector256<float>.Zero);
            var specColor = GeometryMath.Scale(GeometryMath.Pow(specular, surf.Roughness), light.Color.Widen());
            var specularColor = new Colors(
                Avx.BlendVariable(specColor.R, defColor.R, specMask),
                Avx.BlendVariable(specColor.G, defColor.G, specMask),
                Avx.BlendVariable(specColor.B, defColor.B, specMask)
            );

            var newCol = col + (surf.Diffuse(pos) * lightColor + surf.Specular(pos) * specularColor);

            return new Colors(
                Avx.BlendVariable(col.R, newCol.R, msk),
                Avx.BlendVariable(col.G, newCol.G, msk),
                Avx.BlendVariable(col.B, newCol.B, msk)
            );
        }

        private Colors NaturalColor(IHitable thing, in Vec3s pos, in Vec3s norm, in Vec3s rd, MyScene scene)
        {
            var col = Colors.DefaultColor;
            foreach (var light in scene.Lights)
                col = AddLight(thing, pos, norm, rd, scene, col, light);
            return col;
        }

        private Vec3s Point(int width, int height, int x, int y, in Camera cam)
        {
            var recenterX = (x - (width / 2.0f)) / 2.0f / width;
            var recenterY = -(y - (height / 2.0f)) / 2.0f / height;

            return GeometryMath.Norm(cam.Forward.Widen() + (recenterX * cam.Right + recenterY * cam.Up).Widen());
        }

        public void Render(MyScene scene, Canvas canvas)
        {
            for (int y = 0; y < canvas.Height; y++)
            {
                for (int x = 0; x < canvas.Width; x++)
                {
                    var point = Point(canvas.Width, canvas.Height, x, y, scene.Camera);
                    var color = TraceRay(new Rays(scene.Camera.Position.Widen(), point), scene, 0);
                    for (byte i = 0; i < 8; i++)
                    {
                        // canvas[x, y] = new Color(Avx2.Extract(color.R, i), color.G[i], color.B[i]);
                    }
                }
            }
        }
    }
}