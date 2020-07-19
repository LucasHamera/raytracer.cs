namespace Raytracer.Geometry.Baseline
{
    public readonly struct Camera
    {
        public readonly Vec3 Position;
        public readonly Vec3 Forward;
        public readonly Vec3 Right;
        public readonly Vec3 Up;

        public Camera(in Vec3 position, in Vec3 lookAt)
        {
            Position = position;

            var geometry = new BaselineGeometry();
            Forward = geometry.Norm(lookAt - position);
            Right = 1.5f * geometry.Norm(geometry.Cross(Forward, new Vec3(0.0f, -1.0f, 0.0f)));
            Up = 1.5f * geometry.Norm(geometry.Cross(Forward, Right));
        }
    }
}
