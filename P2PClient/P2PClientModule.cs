using Microsoft.Xna.Framework;

namespace Celeste.Mod.P2PClient;

public class P2PClientModule : EverestModule
{
    static Vector2 PlayerPosition = new(0, 0);
    static Ghost Ghost = null;
    public override void Load()
    {
        Logger.Log(LogLevel.Info, "P2PClient", "P2PClientModule Loaded");
        // 注册事件
        On.Celeste.Player.Update += Player_Update;
        On.Celeste.Level.LoadLevel += Level_LoadLevel;
    }

    public override void Unload()
    {
        // 取消注册事件
        On.Celeste.Player.Update -= Player_Update;
        On.Celeste.Level.LoadLevel -= Level_LoadLevel;
    }

    private void Player_Update(On.Celeste.Player.orig_Update orig, Player self)
    {
        
        // Logger.Log(LogLevel.Debug, "P2PClient", self.Position.ToString());
        // 在玩家更新时调用
        PlayerPosition = self.Position;
        orig(self);
    }
    
    private void Level_LoadLevel(On.Celeste.Level.orig_LoadLevel orig, Level self, Player.IntroTypes playerIntro, bool isFromLoader)
    {
        // 在加载关卡时调用
        orig(self, playerIntro, isFromLoader);
        Player Player = self.Tracker.GetEntity<Player>();
        if(Player == null) return;
        Ghost = new Ghost(Player, self);
        self.Add(Ghost);
    }

}