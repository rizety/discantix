using Pantix;

namespace Discantix
{
    public class ExtraMode : CurrentMode
    {
        public ExtraMode(string name) : base(name) { }
        
        public static CurrentMode discantix = new ExtraMode("discantix");
    }
}
