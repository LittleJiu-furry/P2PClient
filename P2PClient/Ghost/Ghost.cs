using IL.Monocle;
using Microsoft.Xna.Framework;

namespace Celeste.Mod.P2PClient;

public class Animation {
    public int Frame;
    public String AnimationFrameID;
}


public class Ghost : Actor {
    public PlayerSprite Sprite;
    public PlayerHair Hair;
    public Player Player;
    protected Facings Facing;


    public Ghost(Player player, Level level) : base(Vector2.Zero) {
        Position = player.Position; // 设置位置
        Player = player; // 设置玩家
        Collidable = false; // 不可碰撞
        Visible = true; // 可见
        Sprite = new PlayerSprite(PlayerSpriteMode.Madeline); // 设置精灵
        Depth = 1; // 设置单独渲染层级
        AddTag(Tags.PauseUpdate); // 添加持久标签
        Hair = new PlayerHair(Sprite);
        Hair.Color = Player.NormalHairColor;
        Add(Hair);
        Add(Sprite);
        

    }

    public override void Added(Monocle.Scene scene)
    {
        base.Added(scene);
        Hair.Facing = Player.Hair.Facing;
        Hair.Start();
        
    }

    public override void Update() {
        base.Update();
        
        HisSpriteData.Enqueue(new GhostSpriteData{
            Rate = Player.Sprite.Rate,
            Position = Player.Position,
            Scale = Player.Sprite.Scale,
            Color = Player.Sprite.Color,
            Rotation = Player.Sprite.Rotation,
            Facing = Player.Facing,
            Justify = Player.Sprite.Justify,
            HairColor = Player.Hair.Color,
            HairAlpha = Player.Hair.Alpha,
            HairFacing = Player.Hair.Facing,
            HairSimulateMotion = Player.Hair.SimulateMotion
        });
        hisAnimation.Enqueue(new Animation{
            Frame = Sprite.CurrentAnimationFrame,
            AnimationFrameID = Sprite.CurrentAnimationID
        });

        if(HisSpriteData.Count > 60) {
            GhostSpriteData spriteData = HisSpriteData.Dequeue();
            Animation animation = hisAnimation.Dequeue();
            UpdateHair(spriteData, animation);
            UpdateSprite(spriteData, animation);
            UpdateOtherLogic();
        }
        
    }

    public void UpdateHair(GhostSpriteData spriteData, Animation animation) {
        Hair.Color = spriteData.HairColor;
        Hair.Alpha = spriteData.HairAlpha;
        Hair.Facing = spriteData.HairFacing;
        Hair.SimulateMotion = spriteData.HairSimulateMotion;
    }

    public void UpdateSprite(GhostSpriteData spriteData, Animation animation) {
        Sprite.Rate = spriteData.Rate;
        Position = spriteData.Position;
        Sprite.Justify = spriteData.Justify;
        Sprite.Scale = spriteData.Scale;
        Sprite.Scale.X *= (float)spriteData.Facing;
        Sprite.Color = spriteData.Color;
        Sprite.Rotation = spriteData.Rotation;
        try {
            if(Sprite.CurrentAnimationID != animation.AnimationFrameID) {
                Sprite.Play(animation.AnimationFrameID);
            }
            Sprite.SetAnimationFrame(animation.Frame);
        } catch {
            // Ignore
        }
    }

    public void UpdateOtherLogic() {
        // 其他逻辑
    }
}