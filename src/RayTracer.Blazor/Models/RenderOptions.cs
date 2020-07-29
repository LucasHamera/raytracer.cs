namespace RayTracer.Blazor.Models
{
    public class RenderOptions
    {
        private const int InitWidth = 512;
        private const int InitHeight = 512;

        public int Width
        {
            get; 
            set;
        } = InitWidth;

        public int Height
        {
            get; 
            set;
        } = InitHeight;
    }
}
