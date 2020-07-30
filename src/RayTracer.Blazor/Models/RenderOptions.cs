using System.ComponentModel.DataAnnotations;

namespace RayTracer.Blazor.Models
{
    public class RenderOptions
    {
        private const int InitWidth = 32;
        private const int InitHeight = 32;

        [Range(1, 2048, ErrorMessage = "Width invalid (1-2048).")]
        public int Width
        {
            get; 
            set;
        } = InitWidth;

        [Range(1, 2048, ErrorMessage = "{Height invalid (1-2048).")]
        public int Height
        {
            get; 
            set;
        } = InitHeight;
    }
}
