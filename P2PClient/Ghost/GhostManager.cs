
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.P2PClient;

public class GhostManager: Entity {
    public List<Ghost> Ghosts = new List<Ghost>();
    public Player Player;
    public readonly static Color ColorGold = new Color(1f, 1f, 0f, 1f);
    public readonly static Color ColorNeutral = new Color(1f, 1f, 1f, 1f);

    public GhostManager(Player player, Level level): base(Vector2.Zero) {
        Player = player;
        Tag = Tags.HUD;

        
    }
}