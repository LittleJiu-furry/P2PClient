using IL.Monocle;
using Microsoft.Xna.Framework;
namespace Celeste.Mod.P2PClient;

public class GhostSpriteData {
    public float Rate;
    public Vector2 Scale;
    public Color Color;
    public float Rotation;
    public Vector2? Justify;
    public Color HairColor;
    public float HairAlpha;
    public Facings HairFacing;
    public bool HairSimulateMotion;
}

public class GhostHairData {
    public Color Color;
    public float Alpha;
    public Facings Facing;
    public bool SimulateMotion;
}



public class GhostFrameData {
    public Vector2 Position;
    public Facings Facing;
    public GhostSpriteData Sprite;
    public GhostHairData Hair;
}