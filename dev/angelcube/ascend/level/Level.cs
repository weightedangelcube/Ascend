using System.Runtime.CompilerServices;
using Godot;

namespace dev.angelcube.ascend.level {
    
    [GlobalClass]
    public partial class Level : Node {
        [Export]
        public Rect2 Border { get; set; }
    }
}
