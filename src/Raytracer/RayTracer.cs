using Raytracer.Geometry.Baseline;
using Raytracer.Geometry.Baseline.Hitable;
using Raytracer.Geometry.Baseline.Scenes;
using Raytracer.Geometry.Common;

namespace Raytracer
{
    public class RayTracer
    {
        private readonly IGeometry<float, Vec3, Color> _geometry = new BaselineGeometry();

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
            var reflectDir = d - (2.0f * _geometry.Dot(normal, d) * normal);
            var naturalColor = Color.Background + NaturalColor(intersection.Thing, pos, normal, reflectDir, scene);
            var reflectedColor = depth >= MaxDepth
                ? Color.Gray
                : ReflectionColor(intersection.Thing, pos, reflectDir, scene, depth);
            return naturalColor + reflectedColor;
        }

        private Color ReflectionColor(IHitable thing, Vec3 pos, Vec3 rd, MyScene scene, int depth)
        {
            return _geometry.Scale(
                thing.Surface.Reflect(pos),
                TraceRay(new Ray(pos, rd), scene, depth + 1)
            );
        }

        private Color AddLight(IHitable thing, Vec3 pos, Vec3 normal, Vec3 rayDir, MyScene scene, Color col,
            Light light)
        {
            var lightDir = light.Position - pos;
            var lightDirNorm = _geometry.Norm(lightDir);
            var nearIntersection = TestRay(new Ray(pos, lightDirNorm), scene);
            var isInShadow = nearIntersection.HasValue && nearIntersection.Value < _geometry.Mag(lightDir);
            if (isInShadow) return col;

            var illumination = _geometry.Dot(lightDirNorm, normal);
            var lightColor = illumination > 0.0f
                ? _geometry.Scale(illumination, light.Color)
                : Color.DefaultColor;
            var specular = _geometry.Dot(lightDirNorm, _geometry.Norm(rayDir));
            var surf = thing.Surface;
            var specularColor = specular > 0.0f
                ? _geometry.Scale(_geometry.Pow(specular, surf.Roughness), light.Color)
                : Color.DefaultColor;

            return col + (surf.Diffuse(pos) * lightColor + surf.Specular(pos) * specularColor);
        }

        private Color NaturalColor(IHitable thing, Vec3 pos, Vec3 norm, Vec3 rd, MyScene scene)
        {
            var col = Color.DefaultColor;
            foreach (var light in scene.Lights)
                col = AddLight(thing, pos, norm, rd, scene, col, light);
            return col;
        }

        private Vec3 Point(int width, int height, int x, int y, Camera cam)
        {
            var recenterX = (x - (width / 2.0f)) / 2.0f / width;
            var recenterY = -(y - (height / 2.0f)) / 2.0f / height;

            return _geometry.Norm(cam.Forward + (recenterX * cam.Right + recenterY * cam.Up));
        }

        public void Render(MyScene scene, Canvas.Canvas canvas, int width, int height)
        {
            for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                var point = Point(width, height, x, y, scene.Camera);
                var color = TraceRay(new Ray(scene.Camera.Position, point), scene, 0);
                canvas[x, y] = color;
            }
        }
    }
}