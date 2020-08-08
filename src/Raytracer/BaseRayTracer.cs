using Raytracer.Geometry.Base.Geometries;
using Raytracer.Geometry.Base.Hitable;
using Raytracer.Geometry.Base.Models;
using Raytracer.Geometry.Scenes;
using Raytracer.Geometry.Utils;

namespace Raytracer
{
    public class BaseRayTracer
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

        public void Render(MyScene scene, Canvas.Canvas canvas)
        {
            for (int y = 0; y < canvas.Height; y++)
            {
                var height = canvas.Height;
                var width = canvas.Width;

                for (int x = 0; x < width; x++)
                {
                    var point = Point(width, height, x, y, scene.Camera);
                    var color = TraceRay(new Ray(scene.Camera.Position, point), scene, 0);
                    canvas[x, y] = color;
                }
            }
        }
    }
}
